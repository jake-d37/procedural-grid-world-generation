using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    public int type = 0;
    public List<int> possibilities;
    public NeighbourMatrix neighbourMatrix = new NeighbourMatrix ();
    public GameObject model;
    public Vector2 number = Vector2.zero;

    public NeighbourMatrix FindMatrix(int T){
        NeighbourMatrix nm = new NeighbourMatrix ();

        /*if (T == 0){
            nm.m = new List<int>[3,3] {
                {new List<int>{0,8,13}, new List<int>{0,8}, new List<int>{0,8,12}},
                {new List<int>{0,6}, new List<int>{0}, new List<int>{0,4}},
                {new List<int>{0,2,11}, new List<int>{0,2}, new List<int>{0,2,10}}
            }; 
        }*/
        
        switch (T)
        {
            case 0:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{0,8}, new List<int>{0}},
                    {new List<int>{0,6}, new List<int>{0}, new List<int>{0,4}},
                    {new List<int>{0}, new List<int>{0,2}, new List<int>{0}}
                }; 
                break;
            case 1:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{0}, new List<int>{0}},
                    {new List<int>{0}, new List<int>{0}, new List<int>{2,3,10}},
                    {new List<int>{0}, new List<int>{4,10,7}, new List<int>{0}}
                }; 
                break;
            case 2:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{0}, new List<int>{0}},
                    {new List<int>{2,1,11}, new List<int>{0}, new List<int>{2,3,10}},
                    {new List<int>{0}, new List<int>{5,12,13}, new List<int>{0}}
                }; 
                break;
            case 3:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{0}, new List<int>{0}},
                    {new List<int>{1,2,11}, new List<int>{0}, new List<int>{0}},
                    {new List<int>{0}, new List<int>{6,9,11}, new List<int>{0}}
                }; 
                break;
            case 4:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{1,4,12}, new List<int>{0}},
                    {new List<int>{0}, new List<int>{0}, new List<int>{5,6,11,13}},
                    {new List<int>{0}, new List<int>{4,7,10}, new List<int>{0}}
                }; 
                break;
            case 5:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{2,5,10,11}, new List<int>{0}},
                    {new List<int>{4,5,10,12}, new List<int>{0}, new List<int>{5,6,11,13}},
                    {new List<int>{0}, new List<int>{5,8,12,13}, new List<int>{0}}
                }; 
                break;
            case 6:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{3,6,13}, new List<int>{0}},
                    {new List<int>{4,5,10,12}, new List<int>{0}, new List<int>{0}},
                    {new List<int>{0}, new List<int>{6,9,11}, new List<int>{0}}
                }; 
                break;
            case 7:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{1,4,12}, new List<int>{0}},
                    {new List<int>{0}, new List<int>{0}, new List<int>{8,9,12}},
                    {new List<int>{0}, new List<int>{0}, new List<int>{0}}
                }; 
                break;
            case 8:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{2,5,10,11}, new List<int>{0}},
                    {new List<int>{7,8,11}, new List<int>{0}, new List<int>{8,9,10}},
                    {new List<int>{0}, new List<int>{0}, new List<int>{0}}
                }; 
                break;
            case 9:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{3,6,13}, new List<int>{0}},
                    {new List<int>{7,8,13}, new List<int>{0}, new List<int>{0}},
                    {new List<int>{0}, new List<int>{0}, new List<int>{0}}
                }; 
                break;
            case 10:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{1,4,12}, new List<int>{0}},
                    {new List<int>{1,2,11}, new List<int>{0}, new List<int>{5,6,11,13}},
                    {new List<int>{0}, new List<int>{5,8,12,13}, new List<int>{0}}
                }; 
                break;
            case 11:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{3,6,13}, new List<int>{0}},
                    {new List<int>{4,5,10,12}, new List<int>{0}, new List<int>{2,3,10}},
                    {new List<int>{0}, new List<int>{5,8,12,13}, new List<int>{0}}
                }; 
                break;
            case 12:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{2,5,10,11}, new List<int>{0}},
                    {new List<int>{7,8,13}, new List<int>{0}, new List<int>{5,6,11,13}},
                    {new List<int>{0}, new List<int>{4,7,10}, new List<int>{0}}
                }; 
                break;
            case 13:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{2,5,10,11}, new List<int>{0}},
                    {new List<int>{4,5,10,12}, new List<int>{0}, new List<int>{8,9,12}},
                    {new List<int>{0}, new List<int>{6,9,11}, new List<int>{0}}
                }; 
                break;
            default:
                nm.m = new List<int>[3,3] {
                    {new List<int>{0}, new List<int>{0,8}, new List<int>{0}},
                    {new List<int>{0,6}, new List<int>{0}, new List<int>{0,4}},
                    {new List<int>{0}, new List<int>{0,2}, new List<int>{0}}
                }; 
                break;
        }

        //repeat for all 14 types (T == 0-13)

        return nm;
    }
}
