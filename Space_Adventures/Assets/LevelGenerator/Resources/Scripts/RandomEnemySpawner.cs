using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    public GameObject[] enemytypes;
    public GameObject temp;
    public Transform[] enemypositions;
    public int rand = 0;


    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            for(int i = 0 ; i<enemypositions.Length ; i++){
                rand = Random.Range(0, enemytypes.Length);
              temp =  Instantiate(enemytypes[rand], enemypositions[i].position, enemypositions[i].rotation);
              temp.transform.localScale = new Vector3((float).5,(float).5,(float).5);
              temp.transform.parent = gameObject.transform;
              
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
