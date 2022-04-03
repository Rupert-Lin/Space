using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverWin : MonoBehaviour
{
    public Text gameOver;
    public GameObject Player;
    public GameObject GameOverScreen;
    public Text allStuff;
    private GameObject boss;
    private bool check = false;
    private bool onClick = false;
    public float timer = 2000f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<Health_Manager_Temp>().health <= 0f)
        {
            gameOver.text = "GAME OVER";
            allStuff.text = "Name: " + PlayerPrefs.GetString("Name") + "\n Score: " + PlayerPrefs.GetInt("Score") +
                "\n HighScore: " + PlayerPrefs.GetInt("HighScore") +
                "\n GlobalScore: " + PlayerPrefs.GetInt("GlobalScore");
            Time.timeScale = 0f;
            pause();
            if (Input.GetMouseButton(0) && timer <= 0)
            {
                Time.timeScale = 1f;
                Resume();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
            else
            {
                timer--;
            }
        }

        boss = GameObject.FindWithTag("Boss");
        if (boss != null)
        {
            if (boss.GetComponent<Health_Manager_Temp>().health <= 0f)
            {
                Destroy(boss);
                gameOver.text = "Congratulations";
                GameObject.FindWithTag("Player").GetComponent<Score_Script>().increaseScore(10);
                GameObject.FindWithTag("Player").GetComponent<Score_Script>().calculateScore();
                allStuff.text = "Name: " + PlayerPrefs.GetString("Name") + "\n Score: " + PlayerPrefs.GetInt("Score") +
                    "\n HighScore: " + PlayerPrefs.GetInt("HighScore") +
                    "\n GlobalScore: " + PlayerPrefs.GetInt("GlobalScore");
                Time.timeScale = 0f;
                check = true;
                pause();
            }
        }
        else if(check)
        {
            if (Input.GetMouseButton(0) && timer <= 0)
            {
                Time.timeScale = 1f;
                Resume();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
            else
            {
                timer--;
            }
        }
    }

    public void Resume()
    {
        GameOverScreen.SetActive(false);
    }

    void pause()
    {
        GameOverScreen.SetActive(true);
    }


}
