using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public GameObject shoot;
    private Transform playerPosition;
    private Transform myPosition;

    public int range = 20;

    public float firerate = 1f;


    private float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(myPosition.position, playerPosition.position) <= range)
        {

            if (shootTimer < Time.time)
            {
                    Instantiate(shoot, myPosition.position, Quaternion.identity);
                   
                    shootTimer = Time.time + firerate;
                
            }
        }

    }
}
