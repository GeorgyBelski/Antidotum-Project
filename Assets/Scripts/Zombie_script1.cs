using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Zombie_script1 : MonoBehaviour
{

    
    public GameObject enemy;
 //   public Image helthBar;
    public bool attack = false;
    public bool move = false;
    public float distanceToTarget;
    public float rotationSpeed = 3f;
    private Vector3 target;

    GameObject player;
    ZombieAttributes zombieAttributes;
    List<ZombieAttributes> humanList;
    PlayerAttributes playerAttributes;
    int triggerCount = 0;
    int reactionTime = 50;
    int reaction;
    int reactionTimer = 0;

    void Start()
    {
        target = new Vector3(0,0,0);
        player = GameObject.Find("Player");
        if(player)
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
           target = new Vector3(0, 0, 0); 
            distanceToTarget = (enemy.transform.position - this.transform.position).magnitude;
            //    transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
            Vector3 toTarget = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z) - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(toTarget,Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        }
        else
        {
            
            if (target.x == 0 && target.z == 0) {
                
                target = new Vector3(this.transform.position.x + Random.Range(-6f, 6f), this.transform.position.y , this.transform.position.z + Random.Range(-6f, 6f));
                //print("+");
            }
            distanceToTarget = (target - this.transform.position).magnitude;
            //    transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
            Vector3 toTarget = new Vector3(target.x, transform.position.y, target.z) - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(toTarget, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            move = true;
            if(distanceToTarget < 1)
                target = new Vector3(0, 0, 0);
            //distanceToTarget = float.PositiveInfinity;
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
        if (collision.gameObject.layer == Layers.obstacle)
        {
            target = new Vector3(0, 0, 0);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        triggerCount++;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == Layers.player || other.gameObject.layer == Layers.human) { 
            enemy = null;
            move = false;
            triggerCount--;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (distanceToTarget > 1.2f)
        {
            
            attack = false;
            move = true;
        }
        else
        {
            target = new Vector3(0, 0, 0);
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
        //float maxHealthRatio = playerAttributes.GetHealthRatio();
        float maxBaitCoefficient = 0;
        if(playerAttributes)
            maxBaitCoefficient = CalculateBaitCoefficient(playerAttributes);
        float humanBaitCoefficient = 0;
        int indexOfTarget = -1;
        for (int i = 0; i < humanList.Count; i++)
        {
            if (humanList[i]) {
                humanBaitCoefficient = CalculateBaitCoefficient(humanList[i]);
                if (humanBaitCoefficient >= maxBaitCoefficient) {
                    maxBaitCoefficient = humanBaitCoefficient;
                    indexOfTarget = i;
                }
            }
        }
        if (indexOfTarget != -1)
            return humanList[indexOfTarget].gameObject;
        else
            return player;
    }
    float CalculateBaitCoefficient(IDamageable human) {
        float distance = (gameObject.transform.position - human.GetPosition()).magnitude;
        return human.GetHealthRatio() / (distance * distance);
    }

}

