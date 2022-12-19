using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    [SerializeField]
    private int gridLength = 10;
    [SerializeField]
    private int gridWidth = 10;
    [SerializeField]
    private int minWidth = 5;
    [SerializeField]
    private GameObject[] gridObject;
    [SerializeField]
    private float elementSize = 1f;

    GridObject[,] gridElements;

    void Start () {
        Generate(gridLength, gridWidth);
    }

    void Update() {
        //changes elements repeatedly until no more chnages need to be made
        //must keep a list of all changes and when that list is empty its over
    }

    public void Generate(int l, int w) {
        //destroy all game objects in scene

        if (w < minWidth)
            w = minWidth;
        if (l < minWidth)
            l = minWidth;

        gridElements = new GridObject[l,w];

        for (int i = 0; i < l; i++){
            for (int j = 0; j < w; j++){
                gridElements[i,j] = GenerateElement(i,j,l,w);
                Instantiate(gridElements[i,j].model, new Vector3(i * elementSize, 0f, j * elementSize), Quaternion.identity);
            }
        }
    }

    GameObject FindModel(int T, int[,] neighbourMatrix){
        GameObject toReturn = new GameObject();

        if (T == 0){
            toReturn = gridObject[0];
        } else{
            toReturn = gridObject[1];
        }
        
        //figure out how to assign a bunch of different options for how the water is arranged (probably using neighbour matrix)

        return toReturn;
    }

    GridObject GenerateElement(int i, int j, int l, int w) {
        GridObject element = new GridObject();

        element.name = "" + i + j;

        //work out efficient way to define neighbour matrix here
        //find alternative to neighbour matrix

        if (i == 0 || j == 0 || i == l-1 || j == w-1) {
            element.objType = 0;
        } 
        else {
            if (gridElements[i-1,j].objType == 0 || gridElements[i, j-1].objType == 0 || gridElements[i+1,j].objType == 0 || gridElements[i, j+1].objType == 0) {
                element.objType = 2;
            }
        }

        element.model = FindModel(element.objType, element.neighbourMatrix);

        return element;
    }
}
