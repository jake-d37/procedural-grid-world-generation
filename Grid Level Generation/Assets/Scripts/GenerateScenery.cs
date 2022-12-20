using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateScenery : MonoBehaviour
{
    public bool sceneryOn = true;
    [SerializeField] GameObject[] models;
    [SerializeField] float chanceOfSpawn;

    //if element is of type 5, there is a slight chance that scenery is instantiated if possible
    public void GenerateScene(GridObject g, float elementSize, int T) {
        float chanceDecider = Random.Range(0.0f, 1.0f);
        if (chanceDecider <= chanceOfSpawn){
            int modelNo = Random.Range(0,models.Length);
            GameObject decor = Instantiate(models[modelNo], new Vector3(g.number.x * elementSize, 0.025f, g.number.y * elementSize), Quaternion.identity);
            Debug.Log("("+g.number.x+","+g.number.y+"): "+modelNo+" (T: " + T + ")");
        }
    }

    public void DestroyScene() {
        GameObject[] toDestroy = GameObject.FindGameObjectsWithTag ("Decor");

        for (int i = 0; i < toDestroy.Length; i++){
            Destroy(toDestroy[i]);
        }
    }
}
