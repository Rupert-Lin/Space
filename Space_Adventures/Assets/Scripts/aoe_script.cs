using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aoe_script : MonoBehaviour
{
    public float delay;
    public float delay2;
    public float scale;
    public int damage;
    public float fireRate = 0.5f;
    private float nextFire;
    public int scaleSelf = 4;
    public int scaleExplode = 10;
    public GameObject explode;
    // Start is called before the first frame update
    void Start()
    {
        nextFire = 0.0f;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        InvokeRepeating("decTime", 0, 0.1f);
        StartCoroutine(delay_damage(delay));
    }
    public void setValues(float delay3,float delay4,float sc,int d,float fire)
    {
        delay3 = delay;
        delay4 = delay2;
        scale = sc;
        damage = d;
        fireRate = fire;
        this.transform.localScale = new Vector3(scale*scaleSelf, scale*scaleSelf, 0);
    }

    IEnumerator delay_damage(float v)
    {
        yield return new WaitForSeconds(v);
        //this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255f,0f,0f);
        //GameObject exploded = Instantiate(explode, transform.position, Quaternion.identity);
        //exploded.transform.localScale = new Vector3(scale*scaleExplode, scale*scaleExplode, 0);
        //exploded.GetComponent<destroySelf>().setTime(delay2);
        StartCoroutine(persistant_damage(delay2));


    }
    //Just overlapped a collider 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Health_Manager_Temp>().take_damage(damage);
        }
    }

    //Overlapping a collider 2D
    private void OnTriggerStay2D(Collider2D collision)
    {
        StartCoroutine(delay_damage_persistant(delay, collision));

    }
    IEnumerator delay_damage_persistant(float v, Collider2D collision)
    {
        yield return new WaitForSeconds(v);
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (collision.gameObject.tag == "Player")
            {

                collision.GetComponent<Health_Manager_Temp>().take_damage(1f);
            }
        }
    }
    IEnumerator persistant_damage(float v)
    {
        yield return new WaitForSeconds(v);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (delay >= 0f)
        {
            this.GetComponentInChildren<Text>().text = delay.ToString("0.0");
        }
        else
        {
            this.GetComponentInChildren<Text>().enabled = false;
        }


    }

    private void decTime()
    {
        if (delay >= 0)
        {
            delay = delay - 0.1f;

        }
    }
}
