/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    // Scriptable Objects
    [SerializeField]
    private TransformVariable gridPosition;
    [SerializeField]
    private GridDataContainer dataContainer;
    [SerializeField]
    private BooleanVariable dataTrackerEnabled;

    public void TrackObjectPosition(TelemetryDataType type) {
        if(dataTrackerEnabled.Value) {
            DataTrackingGrid dataSet = dataContainer.GetDataGrid(type);
            Vector3 position = transform.position;
            int xData = (int)((transform.position.x - gridPosition.Value.transform.position.x) + dataSet.gridProperties.gridGranularity * 0.5f / dataSet.gridProperties.gridGranularity);
            int zData = (int)((transform.position.z - gridPosition.Value.transform.position.z)  + dataSet.gridProperties.gridGranularity * 0.5f / dataSet.gridProperties.gridGranularity);	

            if(dataSet.InsideBounds(xData, zData)) {
                dataSet.gridData[zData, xData] += dataSet.gridProperties.dataAddStep;
                if (dataSet.gridData[zData, xData] > dataSet.maxValStored) {
                    dataSet.maxValStored = dataSet.gridData[zData, xData];
                }
            }	
        }
    }
}