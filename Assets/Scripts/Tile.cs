using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tile {

    public GameObject gameObj { get; set; }
    public GameObject highlightObj { get; set; }
    public int numVal { get; set; }
    public GameObject numObj { get; set; }
    public bool isSelected { get; set; }
    public MapPoint mapPoint { get; set; }
    public bool isDisplayed { get; set; }

    public Tile(GameObject gameObj, GameObject highlightObj)
    {
        numVal = 0;
        isSelected = false;
        this.gameObj = gameObj;
        this.highlightObj = highlightObj;
    }
}
