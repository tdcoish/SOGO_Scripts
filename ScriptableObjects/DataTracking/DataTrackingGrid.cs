/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewDataTrackingGrid", menuName="SunsOutGunsOut/DataTracking/DataTrackingGrid")]
public class DataTrackingGrid : ScriptableObject 
{
    [Header("Telemetry Data Type")]
    public TelemetryDataType dataType;

    [Header("Grid Properties")]
    public DataGridProperties gridProperties;
    public BooleanVariable dataTrackerEnabled;    

    [Header("Gathered Data")]
    public bool showGrid = true;
    public float[,] gridData;
    public List<GridCell> dataCells;
    public float maxValStored = 0f;

    public enum InitType {
        NO_DATA,
        SAVED_DATA
    };

    public void OnEnable() {
        if (dataTrackerEnabled.Value) {
            InitializeData(InitType.NO_DATA);
        } else {
            InitializeData(InitType.SAVED_DATA);   
        }
    }

    public void InitializeData(InitType initType) {
        gridData = new float[gridProperties.gridSizeZ, gridProperties.gridSizeX];

        if(initType == InitType.NO_DATA) {
            maxValStored = 0f;
            for (int i = 0; i < gridProperties.gridSizeZ; i++)		
            {
                for (int j = 0; j < gridProperties.gridSizeX; j++)
                {
                    gridData[i,j] = 0f;
                }
            }
        } else if (initType == InitType.SAVED_DATA) {
            foreach(GridCell cell in dataCells) {
                gridData[cell.z, cell.x] = cell.value;
            }
            dataCells.Clear();
        }
    }

    public bool InsideBounds(int xData, int zData) {
        return xData < gridProperties.gridSizeX && xData >= 0 && zData < gridProperties.gridSizeZ && zData >= 0;
    }

    public float NormalizeBasedOnData(float value) {
        return value / maxValStored;
    }

    public Color GetNormalizedColor(float normalizedValue) {
        float minH, minS, minV, maxH, maxS, maxV;
        Color.RGBToHSV(gridProperties.minColor, out minH, out minS, out minV);
        Color.RGBToHSV(gridProperties.maxColor, out maxH, out maxS, out maxV);
        
        float hue = Mathf.Lerp(minH, maxH, normalizedValue);
        float saturation = Mathf.Lerp(minS, maxS, normalizedValue);
        float value = Mathf.Lerp(minV, maxV, normalizedValue);
        
        
        Color normalizedColor = Color.HSVToRGB(hue, saturation, value);
        normalizedColor.a = gridProperties.gizmosOpacity;
        return normalizedColor;
    }
}