using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float speed;
    public int health = 10;
    public int damage = 1;
    public Text damageUI;
    public Text healthUI;
    public Text gameOver;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        damageUI.text = "Damage: " + damage;
        healthUI.text = "Health: " + health;
        if(health <= 0)
        {
            gameOver.text = "GAME OVER";
            Pause();
            if (Input.GetMouseButton(0))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void Pause()
    {
        Time.timeScale = 0f;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
