using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private float shootTimer;
    public GameObject aoe;
    public float firerate = 2f;
    Transform[] spawnLocations;
    void Start()
    {
        spawnLocations = this.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootTimer < Time.time)
        {
            Transform spawn = spawnLocations[Random.Range(1, spawnLocations.Length - 1)];
            GameObject spawned = Instantiate(aoe, spawn.position, Quaternion.identity);
            spawned.GetComponent<aoe_script>().setValues(1f, 2f, Random.Range(1f,2f), 1, 1f);
            shootTimer = Time.time + firerate;

        }
    }
}
