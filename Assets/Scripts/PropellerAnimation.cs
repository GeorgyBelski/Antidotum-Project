using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerAnimation : MonoBehaviour
{
    [Range(1f,100f)]
    public float rotationSpeed = 5f;
    public Transform[] propellers;
    int[] rotateDirections;

    void Awake()
    { 
        rotateDirections= new int[propellers.Length];

        int n = 0;
        foreach (Transform propeller in propellers) {
            if (propeller.localScale.x > 0)
                rotateDirections[n] = 1;
            else
                rotateDirections[n] = -1;
            n++;
        }
    }

    void Update()
    {
        for (int i= 0; i < propellers.Length; i++) { 
            propellers[i].localEulerAngles += new Vector3(0, 0, rotationSpeed * rotateDirections[i] * Time.deltaTime * 360);
        }
    }
}
