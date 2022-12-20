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
    List<GridObject> gridElementsToCollapse = new List<GridObject>();
    List<GridObject> gridElementsCollapsed = new List<GridObject>();
    List<GridObject> gridElementsUpdated = new List<GridObject>();

    void Start () {
        Generate(gridLength, gridWidth);
    }


    public void Generate(int l, int w) {

        gridElementsCollapsed.Clear();
        gridElementsToCollapse.Clear();
        gridElementsUpdated.Clear();
        //destroy all game objects in scene

        if (w < minWidth)
            w = minWidth;
        if (l < minWidth)
            l = minWidth;

        gridElements = new GridObject[l,w];

        for (int i = 0; i < l; i++){
            for (int j = 0; j < w; j++){
                gridElements.SetValue(GenerateElement(i,j,l,w),i,j);

                //add element just generated into the list of elements to collapse
                gridElementsToCollapse.Add(gridElements[i,j]);
            }
        }

        //collapse one element and make it grass
        int randX = Mathf.FloorToInt(Random.Range(0 + 0.1f, gridWidth-1f + 0.1f));
        int randY = Mathf.FloorToInt(Random.Range(0 + 0.1f, gridLength-1f + 0.1f));
        CollapseElement(gridElements[randX, randY], 5); 

    	ObserveGrid(); //see whats next to collapse
    }

    void ObserveGrid() {
        //update possibilities of elements neighbouring collapsed elements
        List<GridObject> collapsedCopy = new List<GridObject>();
        collapsedCopy = gridElementsCollapsed;

        foreach (GridObject element in gridElementsCollapsed){
            UpdateNeighbourPossibilities(element);
        }

        //find element (to collapse) that has lowest entropy
        GridObject smallestEntropy = null;
        foreach (GridObject go in gridElementsToCollapse){
            if (smallestEntropy != null){
                if (go.possibilities.Count <= smallestEntropy.possibilities.Count){
                    smallestEntropy = go;
                }
            } else {
                smallestEntropy = go;
            }
        }

        Debug.Log("Possibilities("+ smallestEntropy.number.x + ", " + smallestEntropy.number.y + ")");
        foreach (int i in smallestEntropy.possibilities){
            Debug.Log(i);
        }

        //collapse smallest entropy to a random type within its possibilities
        int typeChosen = Mathf.FloorToInt(Random.Range (0.1f, smallestEntropy.possibilities.Count-1));
        CollapseElement(smallestEntropy, smallestEntropy.possibilities[typeChosen]);

        //repeat observation process until there are no elements to collapse
        Debug.Log ("Elements Left: " + gridElementsToCollapse.Count);
        if (gridElementsToCollapse.Count > 0){
            ObserveGrid ();
        }

    }

    void CollapseElement (GridObject element, int T){
        //collapse type
        element.type = T;
        element.model = objPrefab[T];
        //update neighbour matrix now that type is collapsed
        element.neighbourMatrix = element.FindMatrix(T);
        Instantiate(element.model, new Vector3(element.number.x * elementSize, 0f, element.number.y * elementSize), element.model.transform.rotation);
        //update lists
        gridElementsToCollapse.Remove(element);
        gridElementsCollapsed.Add(element);
    }

    void UpdateNeighbourPossibilities(GridObject element) {
        List<int> topPos = element.neighbourMatrix.m[0,1];
            List<int> botPos = element.neighbourMatrix.m[2,1];
            List<int> lefPos = element.neighbourMatrix.m[1,0];
            List<int> rigPos = element.neighbourMatrix.m[1,2];

            int x = Mathf.FloorToInt(element.number.x + 0.1f);
            int y = Mathf.FloorToInt(element.number.y + 0.1f);

            /*Debug.Log("El("+ x + ", " + y + "): ");
            foreach (int i in topPos){
                Debug.Log("T: " + i);
            }
            foreach (int i in botPos){
                Debug.Log("B: " + i);
            }
            foreach (int i in lefPos){
                Debug.Log("L: " + i);
            }
            foreach (int i in rigPos){
                Debug.Log("R: " + i);
            }*/
            
            if (y < gridElements.GetLength(1)-1 && x < gridElements.GetLength(0)-1 && y > 0 && x > 0){
                
                //top
                List<int> GECopy = new List<int>();
                foreach (int i in gridElements[x, y+1].possibilities){
                    GECopy.Add(i);
                }
                foreach (int i in GECopy){
                    if (!topPos.Contains(i)){
                        gridElements[x, y+1].possibilities.Remove(i);
                    }
                }
                GECopy.Clear();
            
                //bottom
                foreach (int i in gridElements[x, y-1].possibilities){
                    GECopy.Add(i);
                }
                foreach (int i in GECopy){
                    if (!botPos.Contains(i)){
                        gridElements[x, y-1].possibilities.Remove(i);
                    }
                }
                GECopy.Clear();
            
                //left
                foreach (int i in gridElements[x-1, y].possibilities){
                    GECopy.Add(i);
                }
                foreach (int i in GECopy){
                    if (!lefPos.Contains(i)){
                        gridElements[x-1, y].possibilities.Remove(i);
                    }
                }
                GECopy.Clear();
            
                //right
                foreach (int i in gridElements[x+1, y].possibilities){
                    GECopy.Add(i);
                }
                foreach (int i in GECopy){
                    if (!rigPos.Contains(i)){
                        gridElements[x+1, y].possibilities.Remove(i);
                    }
                }
                GECopy.Clear();
            }

            gridElementsUpdated.Add(element);
    }

    GridObject GenerateElement(int i, int j, int l, int w) {
        GridObject element = new GridObject();

        element.number = new Vector2 (i,j);
        element.possibilities = new List<int> {0,1,2,3,4,5,6,7,8,9,10,11,12,13};

        return element;
    }

}
