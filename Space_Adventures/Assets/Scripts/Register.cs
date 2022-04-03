using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    public TMP_Text username;
    public GameObject password ;
    public TMP_Text m_TextComponent;
    public TMP_Text output;
    public TMP_Text outputMain;
    public Button back;

    private string US;
    private string PW;

    private string form;
    private Button btn;

    // Start is called before the first frame update
    void Start()
    {

        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(on_click);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        output.text = "Name: " + PlayerPrefs.GetString("Name") + "\n Score: " + PlayerPrefs.GetInt("Score") + "\n HighScore: " + PlayerPrefs.GetInt("HighScore") + "\n GlobalScore: " + PlayerPrefs.GetInt("GlobalScore");
    }
    IEnumerator createAccount()
    {
        m_TextComponent.text = "Connecting to server......";
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("PlayerName", PlayerPrefs.GetString("Name")));
        formData.Add(new MultipartFormDataSection("Pin", PlayerPrefs.GetString("Pin")));
        formData.Add(new MultipartFormDataSection("HighScore", PlayerPrefs.GetInt("HighScore") + ""));
        formData.Add(new MultipartFormDataSection("GlobalScore", PlayerPrefs.GetInt("GlobalScore") + ""));
        formData.Add(new MultipartFormDataSection("Grade", PlayerPrefs.GetInt("Grade") + ""));
        formData.Add(new MultipartFormDataSection("New", 1 + ""));

        UnityWebRequest www = UnityWebRequest.Post("http://74.101.192.185:8080/Test1/FileUploadServlet", formData);

        yield return www.SendWebRequest();
        if (www.error != null)
        {
            m_TextComponent.text = "No Internet Connection";
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            Dictionary<string, string> input = www.GetResponseHeaders();
            int score = Int32.Parse(input["Status"]);
            if (score == 0)
            {
                //did not cerate account ask them for new pin/username
                m_TextComponent.text = "Account Already Exists";
            }
            else
            {
                m_TextComponent.text = "Account Created";
                outputMain.text = "Account Created";
                //Resume here
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    private void on_click()
    {
        US = username.text;
        PW = password.GetComponent<TMP_InputField>().text;
        m_TextComponent.text = "";
        if (PW.Length > 1 && US.Length > 1 && PW.Length <= 80 && US.Length <= 80)
        {
            PlayerPrefs.SetString("Name", US);
            PlayerPrefs.SetString("Pin", PW);
            PlayerPrefs.Save();
            StartCoroutine(createAccount());

        }
        else
        {
            if (PW.Length < 1)
            {
                m_TextComponent.text += "Enter a Password,";
            }
            if (US.Length < 1)
            {
                m_TextComponent.text += "Enter a UserName,";
            }

            if (PW.Length > 80)
            {
                m_TextComponent.text += "PassWord is Too Long,";
            }
            if (US.Length > 80)
            {
                m_TextComponent.text += "UserName is Too Long,";
            }

        }
    }

}
