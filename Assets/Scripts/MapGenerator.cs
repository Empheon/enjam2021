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
    
    private char lineSeperater = '\n';
    private char fieldSeperator = ';';

    private Train train;
    private int trainX, trainY;
    
    // Start is called before the first frame update
    void Start()
    {
        var csvMapData = ReadData(CsvFileMap, true);
        var csvPigeonData = ReadData(CsvFilePigeons);

        float hOffset = csvMapData[0].Count / 2f - 0.5f;
        float vOffset = csvMapData.Count / 2f - 0.5f;

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
                        go.transform.position = new Vector3(i * 1 - vOffset, 0, j * 1 - hOffset);
                        break;
                    case "v":
                        go = Instantiate(StraightTrackPrefab);
                        go.transform.position = new Vector3(i * 1 - vOffset, 0, j * 1 - hOffset);
                        
                        go.transform.Rotate(0, 90,0);
                        break;
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        go = Instantiate(CurvedTrackPrefab);
                        go.transform.position = new Vector3(i * 1 - vOffset, 0, j * 1 - hOffset);
                        
                        go.transform.Rotate(0, (Int32.Parse(data) - 1) * 90,0);
                        break;
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        go = Instantiate(NonMovableCurvedTrackPrefab);
                        go.transform.position = new Vector3(i * 1 - vOffset, 0, j * 1 - hOffset);
                        
                        go.transform.Rotate(0, (Int32.Parse(data) - 1 - 5) * 90,0);
                        break;
                        
                }

                if (i == trainX && j == trainY)
                {
                    train.transform.position = go.transform.position + new Vector3(0, 0, -1);
                    train.AttachToNewTrack(go.GetComponent<Track>());
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    if (isMap && field.Substring(1, 1).Equals("t"))
                    {
                        train = Instantiate(TrainPrefab);
                        trainX = i;
                        trainY = strMap[i].Count - 1;
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
        
        strMap.RemoveAt(strMap.Count - 1);

        return strMap;
    }
}
