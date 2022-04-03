using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class RocketMove : MonoBehaviour
{
    //[SerializeField] Transform Target;
    Transform Target;
    [SerializeField] float MoveSpeed = 300f;
    [SerializeField] float RotateSpeed = 2000f;
    [SerializeField] GameObject explode;
    Rigidbody2D rb;
    bool homing;
    private bool track_delay;
    private bool track;
    public int damage;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        track_delay = true;
        track = false;
    }
    public void overide_target(Transform t)
    {
        Target = t;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.up * (MoveSpeed) * Time.deltaTime;

        if (homing)
        {

            if (track_delay)
            {
                StartCoroutine(startHoming(1f));
            }
            if(track)
            {
                homing_script();
            }

        }
        
    }
    private void homing_script()
    {
        if (Target != null)
        {
            Vector3 targetVector = Target.position - transform.position;

            float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;

            rb.angularVelocity = -1 * rotatingIndex * RotateSpeed * Time.deltaTime;
        }
        else
        {
            GameObject T1 = GetClosestEnemy(GameObject.FindGameObjectsWithTag("Boss"));
            if (T1 == null)
            {
                GameObject T = GetClosestEnemy(GameObject.FindGameObjectsWithTag("Enemy"));
                if (T != null)
                {
                    Target = T.transform;
                }
            }
            else
            {
                Target = T1.transform;
            }
        }

    }
    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
    IEnumerator startHoming(float delay)
    {
        yield return new WaitForSeconds(delay);
        track_delay = false;
        track = true;
    }

    //Just overlapped a collider 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //UnityEngine.Debug.Log(collision.name);
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Boss")
        {
            GameObject exploded = Instantiate(explode, transform.position, Quaternion.identity);
             //Brian : commenting out because of a compile error related to setDamage not being defined
            //xploded.GetComponent<destroySelf>().setDamage((int)(damage / 2));
            exploded.GetComponent<destroySelf>().setTime(.5f);
            if (collision.gameObject.GetComponent<Health_Manager_Temp>() != null)
            {
                collision.gameObject.GetComponent<Health_Manager_Temp>().take_damage(damage);
            }
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Trap")
        {
            GameObject exploded = Instantiate(explode, transform.position, Quaternion.identity);
            exploded.GetComponent<destroySelf>().setTime(.5f);
            //Brian : commenting out because of a compile error related to setDamage not being defined
            //exploded.GetComponent<destroySelf>().setDamage((int)(damage / 2));
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

    }


    public void set_Damage(int damage_parent)
    {
        damage = damage_parent;
    }
    public void set_Pspeed(float speed)
    {
        MoveSpeed = speed;
    }
    public void isHoming()
    {
        homing = true;
    }
    public void selfDestruct(float time)
    {
        Destroy(this.gameObject, time);
    }

}