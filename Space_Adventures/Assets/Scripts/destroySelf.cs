using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySelf : MonoBehaviour
{
    // Start is called before the first frame update
    public float time = 1f;
    void Start()
    {
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setTime(float f)
    {
        time = f;
    }
}
