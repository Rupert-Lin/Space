using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using ViridaxGameStudios.AI;

public class nukeButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Button btn;
    private GameObject player;
    private int pScore;
    public int scoreCost;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(on_click);

    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            pScore = player.GetComponent<Score_Script>().getScore();
            if (pScore < scoreCost)
            {
                this.GetComponent<Image>().color = new Color(255f, 0f, 0f);
            }
            else
            {
                this.GetComponent<Image>().color = new Color(0f, 255f, 0f);
            }
        }
    }
    private void on_click()
    {
        pScore = player.GetComponent<Score_Script>().getScore();
        if(pScore >= scoreCost)
        {
            player.GetComponent<Score_Script>().nukeButton(scoreCost);
            GameObject[] enemyStuff = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] bossStuff = GameObject.FindGameObjectsWithTag("Boss");
            foreach(GameObject temp in enemyStuff)
            {
                Destroy(temp);
            }
            foreach (GameObject temp in bossStuff)
            {
                temp.GetComponent<Health_Manager_Temp>().take_damage(player.GetComponent<PlayerController>().getDamage()*10);
            }
        }
    }
}
