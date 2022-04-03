using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ViridaxGameStudios.AI;

public class Add_PLayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Button btn;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(on_click);

    }

    private void on_click()
    {
        //player.GetComponent<PlayerController>().toggle_fire();
    }
}
