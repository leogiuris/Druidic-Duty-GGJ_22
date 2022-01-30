using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayerController player;

    

    public bool isPaused = false;
    public bool isPlaying = false;
    public bool gameOver = false;
    public float WinTime = 600;

    public bool taDoidao = false;
    public float doidaoTime = 10f;
    public float intervaloPlanta = 2f;

    private float lastSpawnTime = 0f;
    public int yWalls = 10;
    public int xWalls = 20;

    public Transform extremo1;
    public Transform extremo2;
    private float x1, x2, y1, y2;

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
    public GameObject gameOverPanel;


    // hud
    public Slider barraEquilibrio;
    public float equilibrio;
    public Text textoTempo;
    public GameObject bushUI;
    public GameObject berryUI;
    public GameObject herbExImg;
    public GameObject PredExImg;
    






    public bool ChecaSeTaDentro(Vector3 v)
    {

        if (v.x > x2 || v.x < x1)
        {
            return false;
        }
        else if (v.y > y2 || v.y < y1)
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
        gameOver = false;
        herbExImg.SetActive(false);
        PredExImg.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        berryUI.SetActive(true);
        bushUI.SetActive(true);
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


    public void ResetGame()
    {
        SceneManager.LoadScene("cena_do_leo2");

    }



    public void Pause()
    {
        if (isPaused)
        {
            FindObjectOfType<AudioManager>().Play("somdespause");
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            isPlaying = true;
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("sompause");
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            isPlaying = false;
        }

    }


    /*
     *              NATUREZA
     */

    public void RandomPlantSpawn()
    {
        if(plantas.Count < 80)
        {
            Vector3 v1 = new Vector3((x1 + Random.value * (Mathf.Abs(x1) + x2)), (y1 + Random.value * (Mathf.Abs(y1) + y2)));

            GameObject novaPlanta = Instantiate(planta, v1, transform.rotation);
            plantas.Add(novaPlanta);
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
            herbExImg.SetActive(true);
            GameOver();
        }
        if (predadores.Count <= 0)
        {
            // predadores extintos
            PredExImg.SetActive(true);
            GameOver();
        }
        
        return false;
    }

    void GameOver()
    {
        isPlaying = false;
        FindObjectOfType<AudioManager>().Play("MusicaGameOver");
        gameOver = true;
        berryUI.SetActive(false);
        bushUI.SetActive(false);
        barraEquilibrio.gameObject.SetActive(false);
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        herbSpawnLocations = new Vector3[herb_init_spawns];
        predSpawnLocations = new Vector3[pred_init_spawns];
        Timer.StartTimer();
        x1 = extremo1.position.x;   y1 = extremo1.position.y;
        x2 = extremo2.position.x;   y2 = extremo2.position.y;

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
            FindObjectOfType<AudioManager>().Play("MusicaGameOver");
            Debug.Log(Mathf.Sign(-2f));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(gameOver)
                SceneManager.LoadScene("StartMenu");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!taDoidao)
            {
                Time.timeScale = doidaoTime;
                taDoidao = true;
            }
            else
            {
                Time.timeScale = 1f;
                taDoidao = false;
            }
        }
    }

    public void ExitToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }


    // Update is called once per frame
    void Update()
    {
        LoseCondition();
        float tempo = Timer.getTime();
        float unga = predadores.Count;
        float bunga = herbivoros.Count;
        int semen2 = player.sementes;
        equilibrio = 1 / ((unga + bunga) / predadores.Count);
        barraEquilibrio.value = equilibrio;

        if (!gameOver)
        {
            berryUI.GetComponentInChildren<Text>().text = semen2.ToString();
            bushUI.GetComponentInChildren<Text>().text = player.paredes.ToString();
            textoTempo.text = tempo.ToString("F1").Replace(',', '.'); ;
        }

        GC_input();

        if (tempo - lastSpawnTime > intervaloPlanta)
        {
            lastSpawnTime = tempo;
            RandomPlantSpawn();
        }

        
    }
}
