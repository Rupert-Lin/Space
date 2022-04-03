using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public float speed;
    private Transform playerPosition;
    private Vector2 lastPosition;
    private Player player;

    private int range = 10;
    public int health = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, playerPosition.position) > range)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Health_Manager_Temp>().take_damage(1);
        }

      //  if (other.CompareTag("PlayerProjectile"))
        //{
          //  Destroy(other.gameObject);
            //health--;
       // }
    }
}
