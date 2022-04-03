using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerv2 : MonoBehaviour
{
    Animator DoorAnimator;
<<<<<<< Updated upstream:Space_Adventures/Assets/LevelGenerator/Resources/Scripts/DoorControllerv2.cs
    private void OnTriggerEnter2D(Collider2D other){
        //put logic for enemy count here
        if(other.CompareTag("Player")){
        if(true){
=======
    GameObject [] enemies;
    private void OnTriggerEnter2D(Collider2D other){
        //put logic for enemy count here
        if(other.CompareTag("Player")){
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0){
>>>>>>> Stashed changes:Space_Adventures/Assets/LevelGenerator/Scripts/DoorControllerv2.cs
            DoorAnimator.SetBool("isOpening", true);
        }
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            DoorAnimator.SetBool("isOpening", false);
        }
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        DoorAnimator = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform child in this.transform.parent)
        {
            if(child.tag == "Enemy")
            {
                Debug.Log("Hit");
            }
        }
    }
}
