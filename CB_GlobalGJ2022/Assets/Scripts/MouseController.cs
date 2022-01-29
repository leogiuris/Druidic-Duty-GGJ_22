using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public bool emArvore = false;
    public Vector3 pos;
    public Camera cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public Vector3 getMousePos()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
        
    }


}
