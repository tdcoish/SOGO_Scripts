/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using System;
using System.Collections.Generic;

[Serializable]
public struct Payload
{
    public List<GCPPositionDataEntity> gridData;
    public GridOptions gridOptions;
}