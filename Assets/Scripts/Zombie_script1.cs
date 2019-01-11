using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie_script1 : MonoBehaviour
{
    public GameObject enemy;
    public Image helthBar;
    public bool attack = false;
    public bool move = false;
    public float distanceToTarget;
    public float rotationSpeed = 3f;

    GameObject player;
    ZombieAttributes zombieAttributes;
    List<ZombieAttributes> humanList;
    PlayerAttributes playerAttributes;
    int triggerCount = 0;
    int reactionTime = 50;
    int reaction;
    int reactionTimer = 0;
    // List<float> ratios;
    void Start()
    {
        player = GameObject.Find("Player");
        playerAttributes = player.GetComponent<PlayerAttributes>();
        zombieAttributes = GetComponent<ZombieAttributes>();
        humanList = HumanManager.humanList;
        reaction = Random.Range(0, reactionTime);
    }

    void Update()
    {
        if (reactionTimer < reactionTime)
            reactionTimer++;
        else
            reactionTimer = 0;
    }

    void FixedUpdate()
    {
        if (enemy)
        {
            distanceToTarget = (enemy.transform.position - this.transform.position).magnitude;
            //    transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
            Vector3 toTarget = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z) - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(toTarget,Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        }
        else
        {
            distanceToTarget = float.PositiveInfinity;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.layer == Layers.bullet)
        {
            Destroy(collision.gameObject);
            zombieAttributes.getSound();
        }
        
        if (collision.gameObject.layer == Layers.antidotBullet)
        {
            Destroy(collision.gameObject);
            zombieAttributes.Recover();
            this.enabled = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        triggerCount++;
    }
    private void OnTriggerExit(Collider other)
    {
        enemy = null;
        move = false;
        triggerCount--;
    }
    void OnTriggerStay(Collider other)
    {
        if (distanceToTarget > 1f)
        {
            attack = false;
            move = true;
        }
        else
        {
            attack = true;
            move = false;
        }

        if ((other.gameObject.layer == Layers.human || other.gameObject.layer == Layers.player) && reactionTimer == reaction)
        {
            if (triggerCount == 1)
            { 
                enemy = other.gameObject;
            }
            else
            {
                enemy = DefineTarget();
            }
        }
    }
    GameObject DefineTarget()
    {
        float maxHealthRatio = playerAttributes.healthRatio;
        float distanceToPlayer = (gameObject.transform.position - player.transform.position).magnitude;
        float maxBaitCoefficient = playerAttributes.healthRatio / distanceToPlayer;
        float humanBaitCoefficient = 0;
        int indexOfTarget = -1;
        for (int i = 0; i < humanList.Count; i++)
        {
            /*   if (ratios.Count < i + 1)
               {
                   ratios.Add(humanList[i].healthRatio);
               }
               else {
                   ratios[i] = humanList[i].healthRatio;
               }*/
            if (humanList[i]) { 
                float distance = (gameObject.transform.position - humanList[i].transform.position).magnitude;
                humanBaitCoefficient = humanList[i].healthRatio / distance;
                if (humanBaitCoefficient >= maxBaitCoefficient) {
                    maxBaitCoefficient = humanBaitCoefficient;
                    indexOfTarget = i;
                }
            }
        }
    /*    if (ratios.Count < humanList.Count + 1)
        {
            ratios.Add(playerAttributes.healthRatio);
        }
        else {
            ratios[ratios.Count - 1] = playerAttributes.healthRatio;
        }*/
        if (indexOfTarget != -1)
            return humanList[indexOfTarget].gameObject;
        else
            return player;
    }

}

