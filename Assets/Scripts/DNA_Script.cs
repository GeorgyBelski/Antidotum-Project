using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 5, transform.eulerAngles.z);
    }


}