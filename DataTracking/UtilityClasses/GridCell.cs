/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
* This class is used in conjuction with the Serializer Command script
* Since the JSON unity tool does not support array serialization, a wrapper object was needed to represent a point in the data grid
*/

using System;

[Serializable]
public struct GridCell
{
    public int z;
    public int x;
    public float value;
} 