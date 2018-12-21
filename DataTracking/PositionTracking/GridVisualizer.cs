/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
* Attach this to an object that will render the cubes
*/

using System.Collections.Generic;
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    // Scriptable Objects
	public TransformVariable gridPosition;
    public GridDataContainer dataContainer;
    public DataGridProperties gridProperties;
    public BooleanVariable dataTrackerEnabled;
    // public StringVariable fileToLoad

    [Header("Load Telemetry Data")]
    public string[] previousFiles;
    [Space]
    public string fileToLoad;

    private void Awake() {
        gridPosition.Value = transform;
    }

    private void OnDrawGizmos() {
        // Prevents Gizmos to be drawn
        if (!Application.isPlaying || dataContainer.dataTrackingGrids.Length == 0) return;

        foreach(var dataGrid in dataContainer.dataTrackingGrids) {
            if (dataGrid.showGrid) {
                for (int i = 0; i < dataGrid.gridProperties.gridSizeZ; i++) {
                    for (int j = 0; j < dataGrid.gridProperties.gridSizeX; j++) {
                        Vector3 cubePosition = new Vector3();
                        cubePosition.z = transform.position.z  + i * dataGrid.gridProperties.gridGranularity;
                        cubePosition.x = transform.position.x  + j * dataGrid.gridProperties.gridGranularity;

                        float cubeHeight = 0;

                        if (dataGrid.gridData[i,j] > 0) {
                            float normalizedVal = dataGrid.NormalizeBasedOnData(dataGrid.gridData[i,j]);
                            if(dataGrid.gridProperties.showNormalizedData) {
                                cubeHeight = normalizedVal * dataGrid.gridProperties.maxCubeHeight;
                            } else {
                                cubeHeight = dataGrid.gridData[i,j];
                            }
                            cubePosition.y = cubeHeight * 0.5f;

                            Gizmos.color = dataGrid.GetNormalizedColor(normalizedVal);
                            Gizmos.DrawCube(cubePosition, new Vector3(dataGrid.gridProperties.gridGranularity, cubeHeight, dataGrid.gridProperties.gridGranularity));
                        }
                    }
                }
            }
        }
    }
}