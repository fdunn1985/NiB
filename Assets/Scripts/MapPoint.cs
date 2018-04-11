using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapPoint {
    public int numVal { get; set; }
    public int sectionId { get; set; }
    public int rCoord { get; set; }
    public int cCoord { get; set; }

    public MapPoint(int numVal, int sectionId)
    {
        this.numVal = numVal;
        this.sectionId = sectionId;
    }
}
