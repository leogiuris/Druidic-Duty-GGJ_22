using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parede : MonoBehaviour
{
    private bool mousehere = false;
    public MouseController mouse;
    public float duration;
    public PlayerController dono;
    void Start()
    {
        Invoke("Sumir", duration);
        mouse = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseController>();
    }

    private void Sumir()
    {
        dono.paredes++;
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        if (mousehere)
        {
            mouse.emArvore = false;
        }
    }

    private void OnMouseOver()
    {
        mouse.emArvore = true;
        mousehere = true;
    }
    private void OnMouseExit()
    {
        mouse.emArvore = false;
        mousehere = false;
    }
}
