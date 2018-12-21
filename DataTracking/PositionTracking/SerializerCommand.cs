/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class SerializerCommand : EditorWindow
{
    
    [MenuItem("CONTEXT/GridVisualizer/SerializeJSON")]
    static void SerializeData(MenuCommand command) {
        
        string telemetryFolder = "/TelemetryData";

        GridVisualizer visualizer = (GridVisualizer)command.context;

        List<GCPPositionDataEntity> entities = new List<GCPPositionDataEntity>();

        foreach(var tracker in visualizer.dataContainer.dataTrackingGrids) {

            Grid grid = new Grid { 
                gridData = new List<GridCell>(),
                maxValStored = tracker.maxValStored
            };

            for (int i = 0; i < tracker.gridProperties.gridSizeZ; i++) {
                for (int j = 0; j < tracker.gridProperties.gridSizeX; j++)
                {
                    if(tracker.gridData[i,j] > 0f) {
                        GridCell cell = new GridCell {
                            z = i,
                            x = j,
                            value = tracker.gridData[i,j]
                        };
                        grid.gridData.Add(cell);
                    }
                }
            }

            GCPPositionDataEntity newEntity = new GCPPositionDataEntity {
                name = tracker.dataType.ToString(),
                data = grid
            };
            
            entities.Add(newEntity);
        }

        GridOptions gridOptions = new GridOptions {
            gridSizeX = visualizer.gridProperties.gridSizeX,
            gridSizeZ = visualizer.gridProperties.gridSizeZ,
            maxCubeHeight = visualizer.gridProperties.maxCubeHeight,
            cubeYPosition = visualizer.gridProperties.cubeYPosition,
            gridGranularity = visualizer.gridProperties.gridGranularity,
            dataAddStep = visualizer.gridProperties.dataAddStep,
        };

        Payload payload = new Payload { gridData = entities, gridOptions = gridOptions };
        string json = JsonUtility.ToJson(payload, true);
        DateTime date = DateTime.UtcNow.ToLocalTime();
        string newFile = 
            Application.dataPath + 
            telemetryFolder + "/" + 
            date.Day + "-" + 
            date.Month + "-" + 
            date.Year + "at" + 
            date.Hour + "-" + 
            date.Minute + "-" + 
            date.Second + "-" + 
            "Telemetry.json"
        ;
        File.WriteAllText(newFile, json); // Data Path is the Assets forlder

        // Fetch Telemetry Data
        string[] filePaths = Directory.GetFiles(Application.dataPath + telemetryFolder, "*.json");
        
        if (filePaths.Length > 0) {
            string[] fileNames = new string[filePaths.Length];
            for (int i = 0; i < filePaths.Length; i++) {
                fileNames[i] = Path.GetFileName(filePaths[i]);
            }
            visualizer.previousFiles = fileNames;
        }
    }
}
#endif