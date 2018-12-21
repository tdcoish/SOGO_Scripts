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
using System.IO;

public class LoadDataCommand : EditorWindow
{
    
    [MenuItem("CONTEXT/GridVisualizer/LoadData")]
    static void SerializeData(MenuCommand command) {
        
        string telemetryFolder = "/TelemetryData";
        GridVisualizer visualizer = (GridVisualizer)command.context;
        
        string file = visualizer.fileToLoad.Trim();

        if(string.IsNullOrEmpty(file)) {
            Debug.LogWarning("File field is empty");
        } else {
            string json = File.ReadAllText(Application.dataPath + telemetryFolder + "/" + file);
            Payload payload = JsonUtility.FromJson<Payload>(json);

            // Loads the grid properties
            visualizer.gridProperties.gridSizeX = payload.gridOptions.gridSizeX;
            visualizer.gridProperties.gridSizeZ = payload.gridOptions.gridSizeZ;
            visualizer.gridProperties.maxCubeHeight = payload.gridOptions.maxCubeHeight;
            visualizer.gridProperties.cubeYPosition = payload.gridOptions.cubeYPosition;
            visualizer.gridProperties.gridGranularity = payload.gridOptions.gridGranularity;
            visualizer.gridProperties.dataAddStep = payload.gridOptions.dataAddStep;
            visualizer.dataTrackerEnabled.Value = false;
            
            foreach (GCPPositionDataEntity entity in payload.gridData) {
                for(int i = 0; i < visualizer.dataContainer.dataTrackingGrids.Length; i++) {
                    if (visualizer.dataContainer.dataTrackingGrids[i].dataType.ToString() == entity.name) {
                        // Insert the data here!
                        DataTrackingGrid grid = visualizer.dataContainer.dataTrackingGrids[i];
                        grid.maxValStored = entity.data.maxValStored;
                        grid.dataCells = entity.data.gridData;
                    }
                }
            }
        }
    }
}
#endif