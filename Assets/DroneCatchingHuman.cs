using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCatchingHuman : MonoBehaviour
{
    public Transform catcher;
    public Transform targetNeck;
    public float speed = 1.5f;
    float path =0f;
    bool starCathing = false;
    bool isCached = false;
    public bool isLifted = false;
    Vector3 startLocalPosition , startWorldPosition, endPosition;
    public GameObject z;
    void Start()
    {
        
    }

    void Update()
    {
        if (targetNeck) {
        //    
           // z = targetNeck.root;
            if (!starCathing) {
                startLocalPosition = transform.InverseTransformPoint(catcher.position);
                startWorldPosition = catcher.position;
                starCathing = true;
            }
            if (!isCached)
            {
                Vector3 toTarget = targetNeck.position - catcher.position;
                catcher.position += toTarget.normalized * speed * Time.deltaTime;
                if (toTarget.magnitude <= 0.01f) {
                    targetNeck.root.GetComponent<CapsuleCollider>().enabled = false;
                    targetNeck.root.parent = catcher;
                    isCached = true;
                    endPosition = catcher.position;
                }
                    
            }
            else {
                Vector3 toStart= transform.TransformPoint(startLocalPosition) - catcher.position;
                catcher.position += toStart.normalized * speed * Time.deltaTime;
                if (toStart.magnitude <= 0.05f)
                {
                    
                    isLifted = true;
                    targetNeck = null;
                }
            }
            
        }
    }
}
