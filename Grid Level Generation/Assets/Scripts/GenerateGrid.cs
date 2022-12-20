using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    //goofy editor stuff
    [SerializeField] private CameraMove camScript;

    public int gridLength = 10;
    public int gridWidth = 10;
    
    [SerializeField] private int minWidth = 5;
    [SerializeField] private GameObject[] objPrefab;
    [SerializeField] private float elementSize = 1f;
    public GenerateScenery genSceneScript;

    GridObject[,] gridElements;
    List<GridObject> gridElementsToCollapse = new List<GridObject>();
    List<GridObject> gridElementsCollapsed = new List<GridObject>();
    public List<GameObject> elementsInScene = new List<GameObject>();

    void Awake() {
        genSceneScript = GetComponent<GenerateScenery>();
    }

    public void GenerateOnClick() {
        Generate(gridLength, gridWidth);
    }

    void Generate(int l, int w) {

        gridElementsCollapsed.Clear();
        gridElementsToCollapse.Clear();

        foreach (GameObject g in elementsInScene){
            Destroy(g);
        }

        if (w < minWidth)
            w = minWidth;
        if (l < minWidth)
            l = minWidth;

        gridElements = new GridObject[l,w];

        for (int i = 0; i < l; i++){
            for (int j = 0; j < w; j++){
                gridElements.SetValue(GenerateElement(i,j,l,w),i,j);
                gridElementsToCollapse.Add(gridElements[i,j]);
            }
        }

        float height;
        if (gridLength > gridWidth){
            height = gridLength * elementSize;
        } else {
            height = gridWidth * elementSize;
        }

        camScript.UpdateDesPos(new Vector3 ((l/2f) * elementSize, height + 3f, (w/2f)* elementSize - (height/10f)*2f)); 

        //collapse one element and make it grass
        int randX = Mathf.FloorToInt(Random.Range(0 + 0.1f, gridLength-1f + 0.1f));
        int randY = Mathf.FloorToInt(Random.Range(0 + 0.1f, gridWidth-1f + 0.1f));
        CollapseElement(gridElements[randX, randY], 5); 

    	ObserveGrid(); //see whats next to collapse
    }

    void ObserveGrid() {
        //update possibilities of elements neighbouring collapsed elements
        foreach (GridObject element in gridElementsCollapsed){
            if (!element.updatedNeighbours)
                UpdateNeighbourPossibilities(element);
        }

        //find element (to collapse) that has lowest entropy, pick from random of all these lowest
        List<GridObject> equalSmallestEntropy = new List<GridObject>();

        GridObject smallestEntropy = gridElementsToCollapse[0];
        foreach (GridObject go in gridElementsToCollapse){
            if (go.possibilities.Count < smallestEntropy.possibilities.Count){
                smallestEntropy = go;
                equalSmallestEntropy.Clear();
                equalSmallestEntropy.Add(go);
            }
            if (go.possibilities.Count == smallestEntropy.possibilities.Count){
                equalSmallestEntropy.Add(go);
            }
        }

        smallestEntropy = equalSmallestEntropy[Mathf.FloorToInt(Random.Range(0.1f,equalSmallestEntropy.Count))];

        //collapse smallest entropy to a random type within its possibilities
        int size = smallestEntropy.possibilities.Count;
        int typeChosen;

        if (size > 0) {
            typeChosen = Random.Range (0, size);
            CollapseElement(smallestEntropy, smallestEntropy.possibilities[typeChosen]);
        }
        else {
            typeChosen = 0;
            CollapseElement(smallestEntropy, 5);
        }

        //repeat observation process until there are no elements to collapse
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

        GameObject mod;
        if (element.number.x != 0 && element.number.y != 0 && element.number.x != gridLength-1 && element.number.y != gridWidth-1){
            mod = Instantiate(element.model, new Vector3(element.number.x * elementSize, 0f, element.number.y * elementSize), element.model.transform.rotation);
            ElementDataHolder holder = mod.GetComponent<ElementDataHolder>();
            holder.gridObject = element;
            holder.type = T;
            elementsInScene.Add(mod);

            if (T == 5 && genSceneScript.sceneryOn){
                genSceneScript.GenerateScene(element, elementSize, T);
            }
        }

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
            
        if (y < gridWidth-1 && x < gridLength-1 && y > 0 && x > 0){
                
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

        element.updatedNeighbours = true;
    }

    GridObject GenerateElement(int i, int j, int l, int w) {
        GridObject element = new GridObject();

        element.number = new Vector2 (i,j);
        element.possibilities = new List<int> {0,1,2,3,4,5,6,7,8,9,10,11,12,13};

        return element;
    }

}
