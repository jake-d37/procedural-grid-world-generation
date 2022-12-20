using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform camTransform;
    [SerializeField] private float damp;
    [SerializeField] private Vector3 desiredPos = new Vector3 (0f, 10f,0f);

    public void UpdateDesPos (Vector3 newPos){
        desiredPos = newPos;
    }

    // Update is called once per frame
    void Update()
    {
        camTransform.position = Vector3.Lerp(camTransform.position, desiredPos, damp);
    }
}
