using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViridaxGameStudios.AI;

public class Upgrade_Behaviour : MonoBehaviour
{

    public int upgrade_type;
    public bool random = true;
    public GameObject single_shot;
    public GameObject double_shot;
    public GameObject rocket_shot;
    // Start is called before the first frame update
    void Start()
    {
        if(random)
        {
            setRandom();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, 0.5f);
    }
    public void setUpgrade(int n)
    {
        upgrade_type = n;
    }
    public void setRandom()
    {
        upgrade_type = Random.Range(1, 7);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            
            Destroy(gameObject);
            switch (upgrade_type)
            {
                case 1:
                    //heal
                    UnityEngine.Debug.Log("Heal");
                    collision.gameObject.GetComponent<Health_Manager_Temp>().heal(10);
                    break;
                case 2:
                    //more damage
                    UnityEngine.Debug.Log("Damage Up");
                    collision.gameObject.GetComponent<PlayerController>().increaseDamage(1);
                    break;
                case 3:
                    //max health
                    UnityEngine.Debug.Log("Health Up");
                    collision.gameObject.GetComponent<Health_Manager_Temp>().add_health_max(5);
                    break;
                case 4:
                    //fire speed
                    UnityEngine.Debug.Log("Rate Up");
                    collision.gameObject.GetComponent<PlayerController>().increasefireRate();
                    break;
                case 5:
                    //upgrade projectile
                    UnityEngine.Debug.Log("Weapon Up");
                    int tier = collision.gameObject.GetComponent<PlayerController>().get_tier() + 1;
                    collision.gameObject.GetComponent<PlayerController>().upgrade();
                    switch (tier)
                    {
                        case 2:
                            collision.gameObject.GetComponent<PlayerController>().changeProjectile(double_shot);
                            collision.gameObject.GetComponent<PlayerController>().increaseDamage(1);
                            break;
                        case 3:
                            collision.gameObject.GetComponent<PlayerController>().changeProjectile(rocket_shot);
                            break;
                        case 4:
                            //nothing homing is gained nothing to do here for now
                            break;
                        default:
                            collision.gameObject.GetComponent<PlayerController>().increaseDamage(2);
                            break;
                    }
                    break;
                case 6:
                    //projectile lifetime
                    UnityEngine.Debug.Log("Range Up");
                    collision.gameObject.GetComponent<PlayerController>().increaseLifetime();
                    break;
                case 7:
                    //more projectile speed
                    UnityEngine.Debug.Log("Speed Up");
                    collision.gameObject.GetComponent<PlayerController>().increase_speed();
                    break;
                default:
                    collision.gameObject.GetComponent<Health_Manager_Temp>().heal(Random.Range(2,10));
                    break;
            }

        }
    }
}
