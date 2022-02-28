using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{

    public List<GameObject> waypoints = new List<GameObject>();
    public float movementSpeed;
    public float startSpeed;
    public float health;
    public int money;

    public float currentSlowTimer;

    public GameObject model;
    private float maxHealth;
    public  TextMeshPro healthText;


    private int currentWaypoint = 0;
    private Vector3 startPos;
    private Vector3 offset;

    private float journeyLength;

    private bool isDead = false;
    float distCovered;

    /// <summary>
    /// Prepares all variables when initializating 
    /// </summary>
    /// <param name="enemyBase"></param>
    public void Setup(SO_EnemyBase enemyBase)
    {
        //set all stats
        movementSpeed = enemyBase.movementSpeed;
        this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = enemyBase.sprite;
        health = enemyBase.health;
        money = enemyBase.money;
        maxHealth = health;

        healthText.text = "Hp: " + health + " / " + maxHealth;

        //prepare start location
        offset = new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), 0);
        this.transform.position = waypoints[currentWaypoint].transform.position + offset;
        startPos = this.transform.position;

        //claculate the length towards the waypoint
        journeyLength = Vector3.Distance(waypoints[currentWaypoint + 1].transform.position + offset, waypoints[currentWaypoint].transform.position + offset);
        //change the current waypoint, since we are currently at the first one
        currentWaypoint++;

        //change the way the enemy is facing
        model.transform.up = waypoints[currentWaypoint].transform.position + offset - this.transform.position;

        //store the speed they initially get
        startSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //we use a bool to check if its dead, and not instantly kill it when it drops to 0 hp.
        //This is so the list in the AoE turret doesnt change when it loops through all enemies.
        //change to OnDestroy?
        if (isDead)
        {
            GameManager.addMoney(money);
            Destroy(this.gameObject);
            return;
        }

        SlowHandler();
        movementHandler();
    }

    /// <summary>
    /// Handles movement towards the next waypoint)
    /// </summary>
    private void movementHandler()
    {
        //check if we are already at the way point we are traveling to
        if (this.transform.position != waypoints[currentWaypoint].transform.position + offset)
        {
            //if we are not there, increase the distance covered.
            //we use deltaTime * movementspeed, because movement speed is inconsistent.
            distCovered += Time.deltaTime * movementSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            //keep on lerping towards the waypoint
            transform.position = Vector3.Lerp(startPos, waypoints[currentWaypoint].transform.position + offset, fractionOfJourney);
        }
        else
        {
            //we are currently at waypoint, check if we have another waypoint
            if (currentWaypoint != waypoints.Count - 1)
            {
                //we have another way point, thus reset some values to the new values
                startPos = this.transform.position;
                currentWaypoint++;
                model.transform.up = waypoints[currentWaypoint].transform.position + offset - this.transform.position;
                journeyLength = Vector3.Distance(startPos, waypoints[currentWaypoint].transform.position + offset);
                distCovered = 0;
            }
            else
            {
                //we reached the end, thus run everything that should be run when it reaches the end
                ReachedEnd();
            }
        }
    }

    /// <summary>
    /// Keeps track of the duration of the slow, once duration ran out, it resets the movementspeed back to its start speed.
    /// </summary>
    private void SlowHandler()
    {
        if(currentSlowTimer <= 0)
        {
            movementSpeed = startSpeed;
            currentSlowTimer = 0;
        }
        else
        {
            currentSlowTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    ///  Sets the slow of the enemy
    /// </summary>
    /// <param name="duration"> The duration of the slow </param>
    /// <param name="slowness"> The effectiveness of slow, this value will be subtracted from the original movement speed </param>
    public void SetSlow(float duration, float slowness)
    {
        if(!(currentSlowTimer > 0))
        {
            movementSpeed -= slowness;
        }
        currentSlowTimer = duration;
    }
    
    /// <summary>
    /// Reduces the current health the enemy has.
    /// </summary>
    /// <param name="amount"> The amount that should be substracted from the current health. </param>
    public void reduceHealth(int amount)
    {
        health -= amount;
        healthText.text = "Hp: " + health + " / " + maxHealth;
        if (health <= 0)
        {
            isDead = true;
            health = 0;
        }
    }


    /// <summary>
    /// Runs all methods that need to run when an enemy reached the end point.
    /// </summary>
    private void ReachedEnd()
    {
        GameManager.removeHealth(1);
        Destroy(this.gameObject);
    }



}
