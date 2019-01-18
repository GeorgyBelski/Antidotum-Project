using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHumanChasing : MonoBehaviour
{
    public float speed = 6f;
    public float turningSpeed = 6f;
    public Transform target;
    public Transform centralBase;
    public Transform catcher;

    List<ZombieAttributes> humanList;
    DroneCatchingHuman catchingHuman;
    bool catchCommand = false;
    bool manOnBoard = false;
    float startSpeed;
    float defineTargetCooldown = 0.5f;
    float timer = 0f;

    void Awake()
    {
        humanList = HumanManager.humanList;
        catchingHuman = GetComponent<DroneCatchingHuman>();
        timer = defineTargetCooldown;
        startSpeed = speed;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        //Target
        if (humanList.Count > 0 && !manOnBoard) { 
            if(timer >= defineTargetCooldown) { 
                target = DefineTarget();
                timer = 0f;
            }
        }
        else
            target = centralBase;

        //Rotation
        if (target) { 
            Vector3 toTarget = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
            float flatDistance = toTarget.magnitude;
            Quaternion toRotation;
            if (flatDistance > 0.1f)
            {
                toRotation = Quaternion.LookRotation(toTarget, Vector3.up);
                //Position
                transform.localPosition += toTarget.normalized * speed * Time.deltaTime;
            }
            else {
                toRotation = target.rotation;
                float angle = Quaternion.Angle(transform.rotation, target.rotation);
                if (angle < 0.1f)
                {
                    if (target != centralBase && !catchCommand)
                        Catch();
                }
            }

            if (catchingHuman.isLifted)
            {
                target = centralBase;
                manOnBoard = true;
            }
            else if (catchingHuman.cancel)
            {
                speed = 0f;
            }
            else {
                speed = startSpeed;
                catchCommand = false;
                manOnBoard = false;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turningSpeed * Time.deltaTime);
        }
        
    }

    private void Catch()
    {
        Transform targetNeck = target.GetComponent<ZombieAttributes>().neckPosition;
        catchingHuman.targetNeck = targetNeck;
        catchCommand = true;

    }

    private Transform DefineTarget()
    {
        int indexOfTarget = -1;
        float minDistance = float.PositiveInfinity;
        float distance;
        for (int i = 0; i < humanList.Count; i++)
        {
            if (humanList[i])
            {
                distance = (this.transform.position - humanList[i].GetPosition()).magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    indexOfTarget = i;
                }
            }
        }
        return humanList[indexOfTarget].transform;
    }
}
