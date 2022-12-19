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
    List<GridObject> gridElementsToCollapse;

    void Start () {
        Generate(gridLength, gridWidth);
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

                //add element just generated into the list of elements to collapse

                //collapse all surrounding elements of the matrix to water
                if (i == 0 || j == 0 || j == w-1 || i == l-1) {
                    gridElements [i,j].type = 0;
                    //remove element from list of elements to collapse
                }
            }
        }

    	ObserveGrid(); //see whats next to collapse
    }

    void ObserveGrid() {
        //read elements to collapse lists
        //collapse all possibilities to suit neighbour matrices
        GridObject smallestEntropy = new GridObject ();
        foreach (GridObject go in gridElementsToCollapse){
            if (smallestEntropy.possibilities.Count != 0){
                if (go.possibilities.Count <= smallestEntropy.possibilities.Count){
                    smallestEntropy = go;
                }
            } else {
                smallestEntropy = go;
            }
        }

        //collapse smallest entropy to a random type within its possibilities (if any, otherwise leave blank)
        //remove smallest entropy from the grid elements to collapse list

        //repeat observation process until there are no elements to collapse
        if (gridElementsToCollapse.Count > 0){
            ObserveGrid ();
        }
    }

    GridObject GenerateElement(int i, int j, int l, int w) {
        GridObject element = new GridObject();

        element.possibilities = new List<int> {0,1,2,3,4,5,6,7,8,9,10,11,12,13};

        return element;
    }

}
