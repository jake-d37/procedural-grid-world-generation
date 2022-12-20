using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManage : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] GenerateGrid generateGrid;
    [SerializeField] bool length;

    void Start()
    {
        slider.onValueChanged.AddListener((v) => {
            if (!length){
                generateGrid.gridWidth = Mathf.FloorToInt(v+0.1f);
            }
            else {
                generateGrid.gridLength = Mathf.FloorToInt(v+0.1f);
            }
        });
    }

}
