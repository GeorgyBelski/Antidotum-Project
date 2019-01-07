using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_bullet : MonoBehaviour
{
    public float speed = 20;
    public float life_time = 2;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        life_time -= Time.deltaTime;
        if (life_time < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
