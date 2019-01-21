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
    Transform manOnBoard;
    float startSpeed;
    float startTurningSpeed;
    float defineTargetCooldown = 0.5f;
    float timer = 0f;

    void Awake()
    {
        humanList = HumanManager.humanList;
        catchingHuman = GetComponent<DroneCatchingHuman>();
        timer = defineTargetCooldown;
        startSpeed = speed;
        startTurningSpeed = turningSpeed;
    }

    void Update()
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

        
        if (target) { 
            Vector3 toTarget = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
            float flatDistance = toTarget.magnitude;
            Quaternion toRotation;
            if (flatDistance > 0.15f)
            {
                toRotation = Quaternion.LookRotation(toTarget, Vector3.up);
                //Position
                transform.localPosition += toTarget.normalized * speed * Time.deltaTime;
                if ((turningSpeed != startTurningSpeed) && flatDistance > 3f)
                {
                    turningSpeed = startTurningSpeed;
                }
            }
            else {
                toRotation = target.rotation;
                float angle = Quaternion.Angle(transform.rotation, target.rotation);
                if (angle < 20f)
                {
                    if (target != centralBase && !catchCommand)
                    {
                        Catch();
                    }
                    else if (target == centralBase && manOnBoard)
                    {
                        HumanManager.savedPeopleNumber++;
                        HumanManager.humanList.Remove(manOnBoard.GetComponent<ZombieAttributes>());
                        Destroy(manOnBoard.gameObject);
                        catchingHuman.Reload();
                        catchCommand = false;
                        turningSpeed = startTurningSpeed;
                    }
                }
                else if (angle <= 40f && turningSpeed >= 1.5f) {
                    turningSpeed /= 2;
                }
            }

            if (catchingHuman.isLifted && !manOnBoard)
            {
                if (target != centralBase) {
                    manOnBoard = target;
                    target = centralBase;
                }
                catchCommand = false;
            //    catchingHuman.Reload();
            }
            else if (catchingHuman.cancel)
            {
                speed = 0f;
                manOnBoard = null;
            }
            else {
                speed = startSpeed;
                catchCommand = false;             
            }
            //Rotation
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
