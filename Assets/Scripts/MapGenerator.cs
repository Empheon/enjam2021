using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public TextAsset CsvFileMap;
    public TextAsset CsvFilePigeons;

    public GameObject StraightTrackPrefab;
    public GameObject CurvedTrackPrefab;
    public GameObject NonMovableCurvedTrackPrefab;
    public Train TrainPrefab;
    
    public Pigeon PigeonPrefab;

    public int DeathCountNeeded;
    public float CountdownDuration;

    [HideInInspector] public float CountdownTimer;
    
    public int MaxKillablePigeons;
    
    private char lineSeperater = '\n';
    private char fieldSeperator = ',';

    private float trackUnits = 2f;

    [HideInInspector]
    public Train TrainInstance;
    private int trainX, trainY;
    private int destX, destY;
    
    // Start is called before the first frame update
    void Start()
    {
        CountdownTimer = CountdownDuration;
        
        var csvMapData = ReadData(CsvFileMap, true);
        var csvPigeonData = ReadData(CsvFilePigeons);

        float hOffset = csvMapData[0].Count * trackUnits / 2f - trackUnits / 2f;
        float vOffset = csvMapData.Count * trackUnits / 2f - trackUnits / 2f;

        for (int i = 0; i < csvMapData.Count; i++)
        {
            for (int j = 0; j < csvMapData[i].Count; j++)
            {
                string data = csvMapData[i][j];
                GameObject go = null;

                switch (data)
                {
                    case "":
                        continue;
                    case "h":
                        go = Instantiate(StraightTrackPrefab);
                        go.transform.position = new Vector3(i * trackUnits - vOffset, 0, j * trackUnits - hOffset);
                        break;
                    case "v":
                        go = Instantiate(StraightTrackPrefab);
                        go.transform.position = new Vector3(i * trackUnits - vOffset, 0, j * trackUnits - hOffset);
                        
                        go.transform.Rotate(0, 90,0);
                        break;
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        go = Instantiate(CurvedTrackPrefab);
                        go.transform.position = new Vector3(i * trackUnits - vOffset, 0, j * trackUnits - hOffset);
                        
                        go.transform.Rotate(0, (Int32.Parse(data) - 1) * 90,0);
                        break;
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        go = Instantiate(NonMovableCurvedTrackPrefab);
                        go.transform.position = new Vector3(i * trackUnits - vOffset, 0, j * trackUnits - hOffset);
                        
                        go.transform.Rotate(0, (Int32.Parse(data) - 1 - 5) * 90,0);
                        break;
                        
                }

                if (go != null)
                {
                    go.transform.parent = transform;
                }

                if (i == trainX && j == trainY)
                {
                    TrainInstance.transform.position = go.transform.position + new Vector3(5, 0, 0);
                    TrainInstance.AttachToNewTrack(go.GetComponent<Track>());
                    TrainInstance.transform.parent = transform;
                }

                if (i == destX && j == destY)
                {
                    go.GetComponent<Track>().IsDestination = true;
                }

                if (csvPigeonData[i][j] != "" && csvPigeonData[i][j] != "\r")
                {
                    int nb = Int32.Parse(csvPigeonData[i][j]);
                    float extent = nb * 0.2f;
                    
                    for (int k = 0; k < nb; k++)
                    {
                        float z = 0;
                        if (nb > 1)
                        {
                            z = Mathf.Lerp(-extent, extent, (float) k / (nb - 1));
                        }
                        
                        Vector3 pos = new Vector3(0, 0, z);
                        Pigeon pigeon = Instantiate(PigeonPrefab, Vector3.zero, Quaternion.identity, go.transform);
                        pigeon.transform.localPosition = pos;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!RoundManager.Instance.IsTrainDeparted)
        {
            CountdownTimer -= Time.deltaTime;
            if (CountdownTimer < 0f)
            {
                RoundManager.Instance.StartGame();
            }
        }
    }
    
    private List<List<string>> ReadData(TextAsset file, bool isMap = false)
    {
        string[] records = file.text.Split (lineSeperater);
        List<List<string>> strMap = new List<List<string>>();

        int i = 0, j = 0;
        foreach (string record in records)
        {
            string[] fields = record.Split(fieldSeperator);
            strMap.Add(new List<string>());
            foreach(string field in fields)
            {
                if (field.Length > 1)
                {
                    strMap[i].Add(field.Substring(0, 1));
                    if (isMap)
                    {
                        if (field.Substring(1, 1).Equals("t"))
                        {
                            TrainInstance = Instantiate(TrainPrefab);
                            trainX = i;
                            trainY = strMap[i].Count - 1;
                        } else if (field.Substring(1, 1).Equals("d"))
                        {
                            destX = i;
                            destY = strMap[i].Count - 1;
                        }
                    }
                }
                else
                {
                    strMap[i].Add(field);
                }

                j++;
            }

            i++;
        }
        
        // strMap.RemoveAt(strMap.Count - 1);

        return strMap;
    }
}
