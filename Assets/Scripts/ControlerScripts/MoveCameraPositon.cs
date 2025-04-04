using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraPositon : MonoBehaviour
{
    public Transform cameraTransform;
    void Update()
    {
        transform.position = cameraTransform.position;
    }
}
