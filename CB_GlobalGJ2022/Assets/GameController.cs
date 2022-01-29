using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static bool isPaused = false;


    public int yWalls = 10;
    public int xWalls = 20;



    public int maxQtdAnimais = 20;
    public float ratioHerb;
    public int herbQtd;
    public int predQtd;

    public List<GameObject> plantas;
    public List<GameObject> herbivoros;
    public List<GameObject> predadores;

    public int herb_init_spawns = 2;
    public int pred_init_spawns = 2;

    Vector3[] herbSpawnLocations;
    Vector3[] predSpawnLocations;

    public GameObject planta;
    public GameObject Herb;
    public GameObject Pred;
    
    void BeginGame()
    {
        int i = 0;
        while (i < herb_init_spawns)
        {
            SpawnaAnimal(Herb, herbSpawnLocations[i]);
            i++;
        }

        i = 0;
        while (i < herb_init_spawns)
        {
            SpawnaAnimal(Pred, predSpawnLocations[i]);
            i++;
        }
    }


    void ResetGame()
    {


        BeginGame();
    }


    public void SpawnaAnimal(GameObject animal, Vector3 pos)
    {
        GameObject newAnimal = Instantiate(animal, pos, transform.rotation);

        if (animal.GetComponent<Presa>())
        {
            herbivoros.Add(newAnimal);
            herbQtd++;
        }
        if (animal.GetComponent<Predador>())
        {
            predadores.Add(newAnimal);
            herbQtd++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        herbSpawnLocations = new Vector3[herb_init_spawns];
        predSpawnLocations = new Vector3[pred_init_spawns];

        for (int i = 0; i< herb_init_spawns; i++)
        {
            herbSpawnLocations[i] = new Vector3((Random.value * 2 * xWalls) - xWalls, (Random.value * 2 * yWalls) - yWalls);

        }
        for (int i = 0; i < pred_init_spawns; i++)
        {
            predSpawnLocations[i] = new Vector3((Random.value * 2 * xWalls) - xWalls, (Random.value * 2 * yWalls) - yWalls);

        }
        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
