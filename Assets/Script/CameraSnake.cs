using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSnake : MonoBehaviour
{
    //Object que a camera vai seguir
    public Transform targetObject;

    //Distancia entre a camera e o object
    public Vector3 cameraOffset;

    

    // Start is called before the first frame update
    void Start()
    {
        //Calcular a distancia inicial entre a camera e o object
        cameraOffset = transform.position - targetObject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = targetObject.transform.position + cameraOffset;
        transform.position = newPosition;
    }
}
