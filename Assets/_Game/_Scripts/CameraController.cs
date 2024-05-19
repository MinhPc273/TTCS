using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    new Camera camera;
    private void Awake()
    {
        camera = GetComponent<Camera>();
    }
    private void Start()
    {
        float x = (float)1080 / (float)1920; // 0.56
        float y = (float)Screen.width / (float)Screen.height;
        float z = (float)2048 / (float)2732; // 0.75
        if (y > 0.66)
        {
            camera.orthographicSize = camera.orthographicSize * (z / y);
        }
        else
        {
            camera.orthographicSize = camera.orthographicSize * (x / y);
        }
        
    }
}
