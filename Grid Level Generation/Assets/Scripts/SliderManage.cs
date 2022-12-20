using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManage : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] GenerateGrid generateGrid;
    [SerializeField] bool length;
    [SerializeField] Text text;
    [SerializeField] Toggle tog;

    void Start()
    {
        if (length){
            slider.value = generateGrid.gridLength;
        } else {
            slider.value = generateGrid.gridWidth;
        }

        text.text = "" + slider.value;

        slider.onValueChanged.AddListener((v) => {
            if (!length){
                generateGrid.gridWidth = Mathf.FloorToInt(v+0.1f);
            }
            else {
                generateGrid.gridLength = Mathf.FloorToInt(v+0.1f);
            }
            text.text = "" + v;
        });

        if (tog != null){
            tog.onValueChanged.AddListener((b) => {
                generateGrid.genSceneScript.sceneryOn = b;
            });
        }
    }
}
