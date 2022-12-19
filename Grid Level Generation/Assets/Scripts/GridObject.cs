using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    public string name = "Not yet set up";
    public int objType = 0;
    public int[,] neighbourMatrix = {{0,0,0},{0,-1,0},{0,0,0}}; //0 is water, 1 is not water, -1 is the object 
    public Quaternion orientation = Quaternion.identity;
    public GameObject model;
}
