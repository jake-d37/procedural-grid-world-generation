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
    private GameObject[] objPrefab;
    [SerializeField]
    private float elementSize = 1f;

    GridObject[,] gridElements;
    List<GridObject> gridElementsToCollapse;
    List<GridObject> gridElementsCollapsed;

    void Start () {
        Generate(gridLength, gridWidth);
    }


    public void Generate(int l, int w) {
        foreach (GridObject i in gridElementsCollapsed){
            gridElementsCollapsed.Remove(i);
        }
        foreach (GridObject i in gridElementsToCollapse){
            gridElementsToCollapse.Remove(i);
        }
        //destroy all game objects in scene

        if (w < minWidth)
            w = minWidth;
        if (l < minWidth)
            l = minWidth;

        gridElements = new GridObject[l,w];

        for (int i = 0; i < l; i++){
            for (int j = 0; j < w; j++){
                gridElements[i,j] = GenerateElement(i,j,l,w);
                Instantiate(gridElements[i,j].model, new Vector3(i * elementSize, 0f, j * elementSize), gridElements[i,j].model.transform.rotation);

                //add element just generated into the list of elements to collapse
                gridElementsToCollapse.Add(gridElements[i,j]);

                //collapse all surrounding elements of the matrix to water
                if (i == 0 || j == 0 || j == w-1 || i == l-1) {
                    CollapseElement(gridElements[i,j], 0);
                }
            }
        }

    	ObserveGrid(); //see whats next to collapse
    }

    void ObserveGrid() {
        //update possibilities of elements neighbouring collapsed elements
        foreach (GridObject element in gridElementsCollapsed){
            List<int> topPos = element.neighbourMatrix.m[1,2];
            List<int> botPos = element.neighbourMatrix.m[3,2];
            List<int> lefPos = element.neighbourMatrix.m[2,1];
            List<int> rigPos = element.neighbourMatrix.m[2,3];

            int x = Mathf.FloorToInt(element.number.x);
            int y = Mathf.FloorToInt(element.number.y);
            //top
            foreach (int i in gridElements[x, y+1].possibilities){
                if (!topPos.Contains(i)){
                    gridElements[x, y+1].possibilities.Remove(i);
                }
            }
            //bottom
            foreach (int i in gridElements[x, y-1].possibilities){
                if (!botPos.Contains(i)){
                    gridElements[x, y-1].possibilities.Remove(i);
                }
            }
            //left
            foreach (int i in gridElements[x-1, y].possibilities){
                if (!lefPos.Contains(i)){
                    gridElements[x-1, y].possibilities.Remove(i);
                }
            }
            //right
            foreach (int i in gridElements[x+1, y].possibilities){
                if (!rigPos.Contains(i)){
                    gridElements[x+1, y].possibilities.Remove(i);
                }
            }
        }

        //find element (to collapse) that has lowest entropy
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

        //collapse smallest entropy to a random type within its possibilities
        CollapseElement(smallestEntropy, Mathf.FloorToInt(Random.Range (0, smallestEntropy.possibilities.Count)));

        //repeat observation process until there are no elements to collapse
        if (gridElementsToCollapse.Count > 0){
            ObserveGrid ();
        } else {
            return;
        }
    }

    void CollapseElement (GridObject element, int T){
        //collapse type
        element.type = T;
        element.model = objPrefab[T];
        //update neighbour matrix now that type is collapsed
        element.neighbourMatrix = element.FindMatrix(T);
        //update lists
        gridElementsToCollapse.Remove(element);
        gridElementsCollapsed.Add(element);
    }

    GridObject GenerateElement(int i, int j, int l, int w) {
        GridObject element = new GridObject();

        element.number = new Vector2 (i,j);
        element.possibilities = new List<int> {0,1,2,3,4,5,6,7,8,9,10,11,12,13};

        return element;
    }

}
