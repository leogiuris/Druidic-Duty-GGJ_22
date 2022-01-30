using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public bool isPaused = false;
    public bool isPlaying = false;
    public float WinTime = 600;
    public int yWalls = 10;
    public int xWalls = 20;

    public Transform extremo1;
    public Transform extremo2;

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
    public GameObject pausePanel;


    // hud
    public Slider barraEquilibrio;
    public float equilibrio;

    public bool ChecaSeTaDentro(Vector3 v)
    {
        Vector3 e1 = extremo1.position;
        Vector3 e2 = extremo2.position;

        if (v.x > e2.x || v.x < e1.x)
        {
            return false;
        }
        else if (v.y > e1.y || v.y < e2.y)
        {
            return false;
        }
        else return true;
    }



    void BeginGame()
    {
        int i = 0;
        isPlaying = true;
        isPaused = false;
        Time.timeScale = 1f;

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
        SceneManager.LoadScene("cena_do_leo");

    }


    public void Pause()
    {
        if (isPaused)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            isPlaying = true;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            isPlaying = false;
        }

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


    public bool LoseCondition()
    {
        if (herbivoros.Count <= 0)
        {
            // herbivoros extintos
            Debug.Log("herbivoros extintos");
            ResetGame();
        }
        if (predadores.Count <= 0)
        {
            // predadores extintos
            Debug.Log("predadores extintos");
            ResetGame();
        }
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        herbSpawnLocations = new Vector3[herb_init_spawns];
        predSpawnLocations = new Vector3[pred_init_spawns];
        Timer.StartTimer();

        for (int i = 0; i < herb_init_spawns; i++)
        {
            herbSpawnLocations[i] = new Vector3((Random.value * 2 * xWalls) - xWalls, (Random.value * 2 * yWalls) - yWalls);

        }
        for (int i = 0; i < pred_init_spawns; i++)
        {
            predSpawnLocations[i] = new Vector3((Random.value * 2 * xWalls) - xWalls, (Random.value * 2 * yWalls) - yWalls);

        }
        BeginGame();
    }


    void GC_input()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Pause();
        }
        if (Timer.getTime() >= WinTime)
        {
            Debug.Log("ganhou");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(Timer.getTime());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    public void ExitToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    // Update is called once per frame
    void Update()
    {
        float unga = predadores.Count;
        float bunga = herbivoros.Count;
        equilibrio = 1 / ((unga + bunga) / predadores.Count);
        barraEquilibrio.value = equilibrio;
        GC_input();
    }
}
