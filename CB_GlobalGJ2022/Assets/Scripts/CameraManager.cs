using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.mouseScrollDelta.y < 0 )
        {
            if(cam.orthographicSize < 12 || Input.GetKey(KeyCode.LeftShift))
            {
                cam.orthographicSize += 2;
            }
                
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            if (cam.orthographicSize > 6 || Input.GetKey(KeyCode.LeftShift))
            {
                cam.orthographicSize -= 2;
            }
        }
    }
}
