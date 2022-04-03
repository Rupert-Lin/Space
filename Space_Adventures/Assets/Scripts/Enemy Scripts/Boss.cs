using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed;
    private Transform playerPosition;
    private Transform myPosition;
    private Vector2 lastPosition;
    private Player player;

    public int range = 8;
    public int health = 25;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(myPosition.position, playerPosition.position) > range)
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
            player.health--;
        }

     /*   if (other.CompareTag("PlayerProjectile"))
        {
            Destroy(other.gameObject);
            health--;
        }
    */}

}
