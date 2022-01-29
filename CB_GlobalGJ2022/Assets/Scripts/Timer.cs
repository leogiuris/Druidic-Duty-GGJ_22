using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float init;


    public void StartTimer()
    {
        init = Time.timeSinceLevelLoad;
    }

    public float getTime()
    {
        return Time.timeSinceLevelLoad - init;
    }

    private void Update()
    {

    }

}