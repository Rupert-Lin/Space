using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Manager_Temp : MonoBehaviour
{
    public float health = 100;
    private float max_health;
    public GameObject upgrade;
    // Start is called before the first frame update
    void Start()
    {
        max_health = health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    public float[] get_health()
    {
        return new float[] { health,max_health };
    }
    public void take_damage(float d)
    {
            health -= d;
        if (health <=0f)
        {
            if(this.gameObject.tag == "Enemy")
            {
                //get the enemy point value, values from david
                GameObject.FindWithTag("Player").GetComponent<Score_Script>().increaseScore(1);
                GameObject upgradeTemp = Instantiate(upgrade, this.transform.position, Quaternion.identity);
                upgradeTemp.GetComponent<Upgrade_Behaviour>().setUpgrade(20); // only adds ammo

                Destroy(this.gameObject);
            }

            if(this.gameObject.tag == "Player")
            {
                this.gameObject.GetComponent<Score_Script>().calculateScore();
            }
        }
    }
    public void add_health_max(int h)
    {
        max_health += h;
    }
    public void heal(int h)
    {
        if(health < max_health)
        {
            health += h;
        }
        if(health > max_health)
        {
            health = max_health;
        }
    }
}
