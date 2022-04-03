using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    
    public GameObject[] stuff;

    void Start()
    {
        int rand = Random.Range(0, stuff.Length);
       GameObject trap = Instantiate(stuff[rand], transform.position, Quaternion.identity);
       trap.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

