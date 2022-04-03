using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class Login : MonoBehaviour
{
    public TMP_Text username;
    public GameObject password;
    public TMP_Text m_TextComponent;
    public TMP_Text output;
    public TMP_Text outputMain;
    public Button back;

    private string US;
    private string PW;

    private string form;
    private Button btn;
    void Start()
    {
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(on_click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Download()
    {
        m_TextComponent.text = "Connecting to server......";
        UnityWebRequest www = UnityWebRequest.Get("http://74.101.192.185:8080/Test1/FileUploadServlet" + "?Name=" + PlayerPrefs.GetString("Name") + "&Pin=" + PlayerPrefs.GetString("Pin"));
        Debug.Log("http://74.101.192.185:8080/Test1/FileUploadServlet" + "?Name=" + PlayerPrefs.GetString("Name") + "&Pin=" + PlayerPrefs.GetString("Pin"));
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            m_TextComponent.text = "No Internet Connection";
        }
        else
        {

            Dictionary<string, string> input = www.GetResponseHeaders();

            if (input.ContainsKey("Score"))
            {
                string score = input["Score"];
                Debug.Log(score);
                string[] variables = score.Split(':');
                int[] var1 = new int[3];
                foreach (string s in variables)
                {
                    Debug.Log("Variables: " + s);
                }
                if (!System.Int32.TryParse(variables[0], out var1[0]))
                {
                    Debug.Log("String could not be parsed." + variables[0]);
                }
                if (!System.Int32.TryParse(variables[1], out var1[1]))
                {
                    Debug.Log("String could not be parsed." + variables[1]);
                }
                if (!System.Int32.TryParse(variables[2], out var1[2]))
                {
                    Debug.Log("String could not be parsed." + variables[2]);
                }
                PlayerPrefs.SetInt("HighScore", var1[0]);
                PlayerPrefs.SetInt("GlobalScore", var1[1]);
                PlayerPrefs.SetInt("Grade", var1[2]);
                PlayerPrefs.Save();
                m_TextComponent.text = "Account Info Pulled";
                //resume here
                outputMain.text = "Account Info Pulled";
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                m_TextComponent.text = "Account Does Not Exist";
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
            StartCoroutine(Download());
        }
        else
        {
            if(PW.Length < 1)
            {
                m_TextComponent.text += "Enter a Password,";
            }
            if(US.Length < 1)
            {
                m_TextComponent.text += "Enter a UserName,";
            }

            if(PW.Length > 80)
            {
                m_TextComponent.text += "PassWord is Too Long,";
            }
            if(US.Length > 80)
            {
                m_TextComponent.text += "UserName is Too Long,";
            }

        }
    }
}
