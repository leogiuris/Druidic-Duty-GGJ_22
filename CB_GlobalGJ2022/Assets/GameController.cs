using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int maxInitSpawn = 5;
    public float ratioHerb;
    public int herbQtd;
    public int predQtd;

    int herb_init_spawns = 1;

    Vector3[] herbSpawnLocations;
    public GameObject Herb;
    public GameObject Pred;
    
    void BeginGame()
    {
        int i = 0;
        while (i < herb_init_spawns)
        {
            GameObject initHerb = Instantiate(Herb, herbSpawnLocations[i], transform.rotation);
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
        herbSpawnLocations = new Vector3[maxInitSpawn];
        for(int i = 0; i<maxInitSpawn; i++)
        {
            herbSpawnLocations[i] = new Vector3((Random.value * 40) - 20, (Random.value * 40) - 20);
        }

        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
