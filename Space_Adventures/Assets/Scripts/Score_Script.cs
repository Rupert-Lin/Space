using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Score_Script : MonoBehaviour
{
    public int Score;
    public int globalScore;
    public int highScore;
    public int grade;
    public string playerName;
    public string pinMaybe;
    private Boolean hasConnection;


    private void Start()
    {
        if(!PlayerPrefs.HasKey("Name") && !PlayerPrefs.HasKey("Pin"))
        {
            createFresh();
        }
        updateSelf();
        StartCoroutine(checkInternetConnection());
    }
    void FixedUpdate()
    {
    }
    public void autoSave()
    {
        Debug.Log("Uploading Start");

        InvokeRepeating("upload_score", 1.0f, 30.0f);
    }public void autoInternet()
    {
        Debug.Log("Checking Connection");
        InvokeRepeating("upload_score", 0.0f, 60.0f);
    }
    public void increaseScore(int point)
    {
        Score += point;
        updateFile();
    }
    public int getScore()
    {
        return Score;
    }
    public void nukeButton(int n)
    {
        Score -= n;
    }
    public void setGlobal(int global)
    {
        globalScore = global;
    }
    public void setHigh(int high)
    {
        highScore = high;
    }
    public void upload_score()
    {
        Debug.Log("Upload");
        StartCoroutine(Upload());
    }

    public void pull_score()
    {
        StartCoroutine(Download());
    }
    public void setPin(string p)
    {
        pinMaybe = p;
    }
    //on death
    public void calculateScore()
    {
        if(Score > highScore)
        {
            highScore = Score;
        }
        globalScore += Score;
        updateFile();
        upload_score();
    }
    public int getGrade()
    {
        return grade;
    }
    //end of stages or level 
    public void calculateGrade(int enemyCount, float time_taken)
    {
        float[] health = gameObject.GetComponent<Health_Manager_Temp>().get_health();
        float missing = health[1] / health[2];
        if(missing >= .7f && missing < .9f)
        {
            grade += 1;
        }
        if (missing >= .9f)
        {
            grade += 2;
        }
        //give user 3 seconds per enemy
        float timeToKill = (float)enemyCount * 3;
        if(time_taken <= timeToKill)
        {
            grade += 2;
        }
        //if you take too long
        if(time_taken >= timeToKill*2f)
        {
            grade -= 1;
        }
        grade += 1; //grade up for clearing

    }
    public void createFile()
    {
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("GlobalScore", globalScore);
        PlayerPrefs.SetInt("Grade", grade);
        PlayerPrefs.SetString("Name", playerName);
        PlayerPrefs.SetString("Pin", pinMaybe);
        PlayerPrefs.Save();

    }
    public void resetPlayer()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("GlobalScore", 0);
        PlayerPrefs.SetInt("Grade", 0);
        PlayerPrefs.Save();

    }
    public void createFresh()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("GlobalScore", 0);
        PlayerPrefs.SetInt("Grade", 0);
        PlayerPrefs.SetString("Name", "Player");
        PlayerPrefs.SetString("Pin", "");
        PlayerPrefs.Save();
    }
    public Boolean HasConnection()
    {
        return hasConnection;

    }

    IEnumerator checkInternetConnection()
    {
        UnityWebRequest www = new UnityWebRequest("http://google.com");
        yield return www;
        if (www.error != null)
        {
            hasConnection = false;
        }
        else
        {
            hasConnection = true;
        }
    }
    public void updateFile()
    {
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("GlobalScore", globalScore);
        PlayerPrefs.SetInt("Grade", grade);
        PlayerPrefs.SetString("Name", playerName);
        PlayerPrefs.SetString("Pin", pinMaybe);
        PlayerPrefs.Save();
    }
    IEnumerator Upload()
    {

        updateFile();
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //form.AddBinaryData("saveFile", FileContent(Application.persistentDataPath + "/score.txt"), "score.txt");
        formData.Add(new MultipartFormDataSection("PlayerName", PlayerPrefs.GetString("Name")));
        formData.Add(new MultipartFormDataSection("Pin", PlayerPrefs.GetString("Pin")));
        formData.Add(new MultipartFormDataSection("HighScore",highScore+""));
        formData.Add(new MultipartFormDataSection("GlobalScore", globalScore+""));
        formData.Add(new MultipartFormDataSection("Grade", grade+""));
        formData.Add(new MultipartFormDataSection("New", 0 + ""));

        UnityWebRequest www = UnityWebRequest.Post("http://74.101.192.185:8080/Test1/FileUploadServlet", formData);
        //www.SetRequestHeader("Content-Type", "multipart/form-data; boundary=---011000010111000001101001");
        //www.method = "POST";
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        if (www.error != null)
        {
            hasConnection = false;
        }
        else
        {
            hasConnection = true;
        }


    }

    IEnumerator Download()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://74.101.192.185:8080/Test1/FileUploadServlet" + "?Name=" + PlayerPrefs.GetString("Name") + "&Pin=" + PlayerPrefs.GetString("Pin"));
        Debug.Log("http://74.101.192.185:8080/Test1/FileUploadServlet" + "?Name=" + PlayerPrefs.GetString("Name") + "&Pin=" + PlayerPrefs.GetString("Pin"));
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            hasConnection = false;
        }
        else
        {
            hasConnection = true;

            Dictionary<string, string> input = www.GetResponseHeaders();
            string score = input["Score"];
            Debug.Log(score);
            if (score != null)
            {
                string[] variables = score.Split(':');
                foreach (string s in variables)
                {
                    Debug.Log("Variables: " + s);
                }
                if (!Int32.TryParse(variables[0], out highScore))
                {
                    Debug.Log("String could not be parsed." + variables[0]);
                }
                if (!Int32.TryParse(variables[1], out globalScore))
                {
                    Debug.Log("String could not be parsed." + variables[1]);
                }
                if (!Int32.TryParse(variables[2], out grade))
                {
                    Debug.Log("String could not be parsed." + variables[2]);
                }
                updateFile();
            }
        }

    }
    public void updateSelf()
    {
        playerName = PlayerPrefs.GetString("Name");
        pinMaybe = PlayerPrefs.GetString("Pin");
        Score = PlayerPrefs.GetInt("Score");
        highScore = PlayerPrefs.GetInt("HighScore");
        globalScore = PlayerPrefs.GetInt("GlobalScore");
        grade = PlayerPrefs.GetInt("Grade");

    }

}
