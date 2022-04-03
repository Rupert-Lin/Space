using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Player player;
    private Transform playerPosition;
    public float speed;
    private float destroyTimer = 2.4f;
    private float timer = 0.0f;
    private Vector2 lastPostion;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lastPostion = new Vector2(playerPosition.position.x, playerPosition.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
        //  transform.Translate(Vector2.up * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if (gameObject != null)
        {
      //      if (Vector2.Distance(transform.position, lastPostion) < 0.5f)
            if (destroyTimer < timer)
            {

                Destroy(gameObject);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health_Manager_Temp>().take_damage(damage);
            Destroy(gameObject);
        }
        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }


}
