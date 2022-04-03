using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectile;
    private Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(projectile, playerPosition.position, Quaternion.identity);
        }
    }
}
