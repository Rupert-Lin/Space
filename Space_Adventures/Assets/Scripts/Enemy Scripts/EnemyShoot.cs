using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    public GameObject shoot;
    private Transform playerPosition;

    private int range = 10;

    private float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerPosition.position) <= range)
        {

            if (shootTimer < Time.time)
            {
                Instantiate(shoot, transform.position, Quaternion.identity);
                float firerate = 1;
                shootTimer = Time.time + firerate;
            }
        }

    }
}
