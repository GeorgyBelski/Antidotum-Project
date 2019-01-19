using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Box : MonoBehaviour
{
    public float deleteTime = 30f;
    public float deleteTimeLeft;
    public float ammo = 90f;
    // Start is called before the first frame update
    void Start()
    {
        deleteTimeLeft = deleteTime;
    }

    // Update is called once per frame
    void Update()
    {
        deleteTimeLeft -= Time.deltaTime;
        if (deleteTimeLeft <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("AddAmmo", ammo, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }

    }

}
