/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewGridDataContainer", menuName="SunsOutGunsOut/DataTracking/GridDataContainer")]
public class GridDataContainer : ScriptableObject
{
    public DataTrackingGrid[] dataTrackingGrids;

    private Dictionary<TelemetryDataType, DataTrackingGrid> dataSets;

    private void OnEnable()
    {
        if(dataTrackingGrids.Length > 0) {
            MapMessages();
        }
    }

    private void MapMessages()
    {
        dataSets = new Dictionary<TelemetryDataType, DataTrackingGrid>();
        foreach(var item in dataTrackingGrids)
        {
            dataSets[item.dataType] = item;
        }
    }

    public DataTrackingGrid GetDataGrid(TelemetryDataType type) {
        if(dataSets.ContainsKey(type)) {
            return dataSets[type];
        } else {
            return null;
        }
    }
}