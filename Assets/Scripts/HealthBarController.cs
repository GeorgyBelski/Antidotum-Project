using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    Transform player;
    Vector3 fromPlayerToBar;
    Quaternion StableRotation;

    void Start()
    {
        player = transform.root;
        fromPlayerToBar = this.transform.position - player.transform.position;
        StableRotation = Camera.main.transform.rotation;
        this.transform.rotation = StableRotation;
    }

    void Update()
    {
        this.transform.position = player.transform.position + fromPlayerToBar;
        this.transform.rotation = StableRotation;
    }
}
