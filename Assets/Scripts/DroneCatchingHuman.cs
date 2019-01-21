using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCatchingHuman : MonoBehaviour
{
    public Transform catcher;
    public Transform targetNeck;
    public float speed = 6.5f;
    public bool isLifted = false;
    public bool cancel = false;
    float path =0f;
    bool startCathing = false;
    bool isCached = false;
    bool reloadEnded = true;

    
    Vector3 startLocalPosition , startWorldPosition, endPosition;
    public GameObject z;
    float startSpeed;
    void Start()
    {
        startSpeed = speed;
    }

    void Update()
    {
        if (targetNeck && !isLifted && !cancel)
        {
            //    
            // z = targetNeck.root;
            if (!startCathing)
            {
                startLocalPosition = transform.InverseTransformPoint(catcher.position);
                startWorldPosition = catcher.position;
                startCathing = true;
            }
            if (!isCached)
            {
                Vector3 toTarget = SendingCatcher();
                reloadEnded = false;
                if (toTarget.magnitude <= 0.02f)
                {
                    targetNeck.root.GetComponent<CapsuleCollider>().enabled = false;
                    targetNeck.root.parent = catcher;
                    isCached = true;
                    endPosition = catcher.position;
                    speed = startSpeed;
                }
                else if(toTarget.magnitude <= 0.25f && speed > 0.5f)
                {
                    speed /= 2;
                }

            }
            else
            {
                if (ReturningCatcher().magnitude <= 0.05f)
                {
                    isLifted = true;
                    reloadEnded = true;
                    catcher.position = transform.TransformPoint(startLocalPosition);
                }
            }

        }
        else if(!reloadEnded)
        {
            cancel = true;
            Reload();
        }
    }
    public void Reload() {
    
        if (ReturningCatcher().magnitude <= 0.05f)
        {
            isLifted = false;
            isCached = false;
            startCathing = false;
            cancel = false;
            reloadEnded = true;
            catcher.position = transform.TransformPoint(startLocalPosition);
        }

    }

    Vector3 SendingCatcher()
    {
        Vector3 toTarget = targetNeck.position - catcher.position;
        catcher.position += toTarget.normalized * speed * Time.deltaTime;
        return toTarget;
    }
    Vector3 ReturningCatcher()
    {
        Vector3 toStart = transform.TransformPoint(startLocalPosition) - catcher.position;
        catcher.position += toStart.normalized * speed * Time.deltaTime;
        return toStart;
    }
}
