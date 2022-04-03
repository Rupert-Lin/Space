using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Video;

namespace ViridaxGameStudios.AI
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerController : MonoBehaviour
    {

        public FixedJoystick joystick;
        public FixedJoystick joystickLook;
        public Camera cam;
        public GameObject attackProjectile;
        private int upgrade_tier = 1;
        public int damage = 1;
        public Transform spawnPosition;
        public float movementSpeed = 20;
        public float rotationSpeed = 30;
        public float fireRate = 0.5f;
        private float nextFire;
        public float pSpeed = 350f;
        public bool hasAnimations;
        public Text textElement;
        public Vector3 lookAtTargetPos;
        Rigidbody2D rb;
        private float life_time = 5f;
        ParticleSystem[] childrenParticleSytems;
        // Start is called before the first frame update
        void Start()
        {
            //transform.SetParent(null);
            this.GetComponent<Score_Script>().updateSelf();
            this.GetComponent<Score_Script>().autoSave();
            childrenParticleSytems = gameObject.GetComponentsInChildren<ParticleSystem>();
            nextFire = 0.0f;
            rb = gameObject.GetComponent<Rigidbody2D>();

        }

        // Update is called once per frame
        void FixedUpdate()
        {
           // Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            //rb.AddForce(direction * movementSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);

            //float translation = (Input.GetAxis("Vertical") * movementSpeed) * Time.deltaTime;
            //float rotation = (Input.GetAxis("Horizontal") * rotationSpeed) * Time.deltaTime;

            float translation = (joystick.Vertical * movementSpeed) * Time.deltaTime;
            float rotation = (joystick.Horizontal * rotationSpeed) * Time.deltaTime;
            //UnityEngine.Debug.Log(translation + "," + rotation);
            //UnityEngine.Debug.Log(joystickLook.Vertical + "   " + joystickLook.Horizontal);
            //joystick look
            if (joystickLook.Vertical != 0)
            {
                //UnityEngine.Debug.Log(joystickLook.Vertical + " | " + joystickLook.Horizontal);
                float angle = Mathf.Atan2(joystickLook.Vertical, joystickLook.Horizontal);
                transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
                if (Time.time > nextFire)
                {

                    nextFire = Time.time + fireRate;
                    var mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    //var mousePos = new Vector2(joystickLook.Vertical, joystickLook.Horizontal);
                    Vector2 myPos = new Vector2(spawnPosition.position.x, spawnPosition.position.y + 1);
                    Vector2 direction = mousePos - myPos;
                    direction.Normalize();
                    Vector3 spawnPos = new Vector3(spawnPosition.position.x, spawnPosition.position.y, 0);
                    //UnityEngine.Debug.Log(transform.rotation);
                    //spawning ftom a child node does not work for some reason
                    GameObject projectile = Instantiate(attackProjectile, spawnPosition.position, transform.rotation * Quaternion.Euler(0, 0, -90));
                    
                    projectile.GetComponent<RocketMove>().set_Damage(gradeScaleing());
                    projectile.GetComponent<RocketMove>().selfDestruct(life_time);
                    projectile.GetComponent<RocketMove>().set_Pspeed(pSpeed);
                    if (upgrade_tier > 3)
                    {
                        projectile.GetComponent<RocketMove>().isHoming();
                    }
                }
                
                

            }

            //not used with dual joystick
            // faceMouse();
            //transform.Translate(translation,rotation*-1,0);
            //transform.Translate(rotation, translation, 0, Camera.main.transform);
            Vector2 force = new Vector2(rotation, translation);
            force.Normalize();
            force *= movementSpeed;
            rb.MovePosition(rb.position + force * Time.fixedDeltaTime);
            //UnityEngine.Debug.Log(rb.velocity + " | " + rotation + " , " + translation);

            if (translation == 0)
            {
                foreach (ParticleSystem childPS in childrenParticleSytems)
                {
                    // Get the emission module of the current child particle system [childPS].
                    ParticleSystem.EmissionModule childPSEmissionModule = childPS.emission;
                    // Disable the child's emission module.
                    childPSEmissionModule.enabled = false;
                }
            }
            else
            {
                foreach (ParticleSystem childPS in childrenParticleSytems)
                {
                    // Get the emission module of the current child particle system [childPS].
                    ParticleSystem.EmissionModule childPSEmissionModule = childPS.emission;
                    // Disable the child's emission module.
                    childPSEmissionModule.enabled = true;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                //shoot();
            }
            show_info();


        }
        public int gradeScaleing()
        {
            int grade = this.GetComponent<Score_Script>().getGrade()*2;
            return damage + grade;
        }
        public int getDamage()
        {
            return damage;
        }
        public void show_info()
        {
            textElement.text = gameObject.GetComponent<Score_Script>().playerName + " Health: " + gameObject.GetComponent<Health_Manager_Temp>().get_health()[0] + "/" + gameObject.GetComponent<Health_Manager_Temp>().get_health()[1].ToString("#") + "\n" + "Damage: " + damage + "\n" + "Score: " + gameObject.GetComponent<Score_Script>().Score + "\n HighScore: " + gameObject.GetComponent<Score_Script>().highScore + "\n GlobalScore: " + gameObject.GetComponent<Score_Script>().globalScore;
        }
        void shoot()
        {
            var mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 myPos = new Vector2(spawnPosition.position.x, spawnPosition.position.y + 1);
            Vector2 direction = mousePos - myPos;
            direction.Normalize();
            Vector3 spawnPos = new Vector3(spawnPosition.position.x, spawnPosition.position.y, 0);
            //UnityEngine.Debug.Log(transform.rotation);
            GameObject projectile = Instantiate(attackProjectile, spawnPosition.position, transform.rotation * Quaternion.Euler(0, 0, -90));
        }
        void faceMouse()
        {

            //Get the Screen positions of the object
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

            //Ta Daaa
            transform.rotation = Quaternion.Euler(new Vector3(180f, 180f, angle));
        }

        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Enemy")
            {
                this.gameObject.GetComponent<Health_Manager_Temp>().take_damage(1);
            }
        }
        public int get_tier()
        {
            return upgrade_tier;
        }
        public void upgrade()
        {
            upgrade_tier++;
        }    
        public void changeProjectile(GameObject projectile)
        {
            attackProjectile = projectile;
        }
        public void increasefireRate()
        {
            fireRate = fireRate * 1.10f;
        }
        public void increaseDamage(int damage_up)
        {
            damage += damage_up;
        }
        public void increaseLifetime()
        {
            life_time *= 1.10f;
        }
        public void increase_speed()
        {
            pSpeed *= 1.10f;
        }
        public void assign_joysticks(FixedJoystick L, FixedJoystick R)
        {
            joystick = L;
            joystickLook = R;
        }

    }

}
