using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Behaviour : MonoBehaviour
{

    public float trapDelay = 2f;
    private float nextFire;
    public bool persistant = true;
    public GameObject explode;
    public GameObject aoe;
    // Start is called before the first frame update
    void Start()
    {
        nextFire = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            if (Time.time > nextFire)
            {
                GameObject trap = Instantiate(explode, transform.position, Quaternion.identity);
                GameObject dot = Instantiate(aoe, transform.position, Quaternion.identity);
                dot.GetComponent<aoe_script>().setValues(1f,2f,1f,3,1f);
                trap.GetComponent<destroySelf>().setTime(2f);
                collision.gameObject.GetComponent<Health_Manager_Temp>().take_damage(10);

            }
            if(!persistant)
            {
                Destroy(gameObject);
            }

        }
    }
}
