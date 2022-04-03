using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Vector2 target;
    public float projectileSpd;

    private Enemy enemy;
    private Boss boss;
    private EnemyRange range;

    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, projectileSpd + Time.deltaTime);
        if(Vector2.Distance(transform.position, target) < 0.2f)
        {
            Destroy(gameObject);
        }
    }

}
