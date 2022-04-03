using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnLocations;

    private float spawnTimer;
    public float startSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = startSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(startSpawnTime <= 0)
        {
            int randomPosition = Random.Range(0, spawnLocations.Length);
            Instantiate(enemy, spawnLocations[randomPosition].position, Quaternion.identity);
            startSpawnTime = spawnTimer;
        }
        else
        {
            startSpawnTime -= Time.deltaTime;
        }
    }
}
