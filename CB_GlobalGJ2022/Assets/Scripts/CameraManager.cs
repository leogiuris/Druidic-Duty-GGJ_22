using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            GetComponent<Camera>().orthographicSize += 2;
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            GetComponent<Camera>().orthographicSize -= 2;
        }
    }
}
