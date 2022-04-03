using System.Collections;
using System.Collections.Generic;
using TestFairyUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject logInMenu;
    void Start()
    {
        TestFairy.begin("SDK-v2Xdgzmw");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LogIn()
    {
        pause();
    }

    public void Resume()
    {
        logInMenu.SetActive(false);
    }

    void pause()
    {
        logInMenu.SetActive(true);
    }

}
