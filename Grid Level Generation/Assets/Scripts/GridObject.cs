using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    public int type = 0;
    public List<int> possibilities;
    public NeighbourMatrix neighbourMatrix = new NeighbourMatrix ();
    public GameObject model;

    public NeighbourMatrix FindMatrix(int T){
        NeighbourMatrix nm = new NeighbourMatrix ();

        if (T == 0){
            nm.m = new List<int>[3,3] {
                {new List<int>{0,8,13}, new List<int>{0,8}, new List<int>{0,8,12}},
                {new List<int>{0,6}, new List<int>{0}, new List<int>{0,4}},
                {new List<int>{0,2,11}, new List<int>{0,2}, new List<int>{0,2,10}}
            }; 
        }
        //repeat for all 14 types (T == 0-13)

        return nm;
    }
}
