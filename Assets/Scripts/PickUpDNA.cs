using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDNA : MonoBehaviour
{
    PlayerAttributes pA;

    void Start()
    {
        pA = transform.root.GetComponent<PlayerAttributes>();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!pA.antidoteCreationProcess && other.gameObject.CompareTag("DNA"))
        {
            Destroy(other.gameObject);
            pA.bioAmount++;
            if (pA.bioAmount >= pA.maxBioAmount)
            {
                pA.antidoteCreationProcess = true;
            }

        }
    }

}
