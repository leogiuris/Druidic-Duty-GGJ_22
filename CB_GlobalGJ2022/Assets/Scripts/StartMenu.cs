using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{

    public void LoadLevel()
    {
        SceneManager.LoadScene("cena_do_leo");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
