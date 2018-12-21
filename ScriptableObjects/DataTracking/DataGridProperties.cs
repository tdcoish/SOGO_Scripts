/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using UnityEngine;

[CreateAssetMenu(fileName="NewDataGridProperties", menuName="SunsOutGunsOut/DataTracking/DataGridProperties")]
public class DataGridProperties : ScriptableObject
{
    [Header("UI Tracking Properties")]
    public Color maxColor = Color.red;
    public Color minColor = Color.blue;
    public bool showNormalizedData = true;
    [Range(0f, 1f)]
    public float cubesTransparency = 0.5f;
    [Range(0f, 1f)]
	public float gizmosOpacity = 0.5f;

    [Header("Data Grid Properties")]
    public int gridSizeX = 100;
    public int gridSizeZ = 100;
    public float maxCubeHeight = 5f;
    public float cubeYPosition = 5f;
    public int gridGranularity = 1;
    public float dataAddStep = 0.1f;
}