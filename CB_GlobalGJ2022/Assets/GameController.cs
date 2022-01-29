using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public float ratioHerb;
    public int herbQtd;
    public int predQtd;

    int herb_init_spawns = 5;



    GameObject Herb;
    GameObject Pred;
    
    void BeginGame()
    {
        int i = 0;
        while (i < herb_init_spawns)
        {
            GameObject initHerb = Instantiate(Herb, transform.position, transform.rotation);
            i++;
        }
        i = 0;
        while (i < herb_init_spawns)
        {
            GameObject initPred = Instantiate(Pred, transform.position, transform.rotation);
            i++;
        }
    }


    void ResetGame()
    {


        BeginGame();
    }


    // Start is called before the first frame update
    void Start()
    {
        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
