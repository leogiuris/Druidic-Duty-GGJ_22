using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    private static Timer _instance;
    public static Timer Instance { get { return _instance; } }

    private static float init;


    public static void StartTimer()
    {
        init = Time.timeSinceLevelLoad;
    }

    public static float getTime()
    {
        return Time.timeSinceLevelLoad - init;
    }


}