using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Transform playerPosition;
    public float speed;
    private Player player;
    private Vector2 lastPostion;

    public int damage = 1;

  //  private float destroyTimer = 1.8f;
  //  private float timer = 0.0f;

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
        transform.position = Vector2.MoveTowards(transform.position, lastPostion, speed * Time.deltaTime);
        //  transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (gameObject != null)
        {
        //  if (destroyTimer < timer)
            if (Vector2.Distance(transform.position, lastPostion) < 0.2f)
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
    }


}
