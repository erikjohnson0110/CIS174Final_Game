using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public GameObject enemyUfo;
    public GameObject cannonParent;
    public Rigidbody cannonBeam;

    private float maxHorizontal;
    private float minHorizontal;

    public float speed = 5f;

    // position of the UFO Game object.  Used for validation
    private Vector3 pos;
    private bool moveLeft = true;

    private int health = 2;
    private const int MAX_HP = 2;

    // enemy weapon info
    private const float FIRE_RATE = 1;
    private float fireCount = 0;
    private Transform cannon1;
    private Transform cannon2;
    private Transform cannon3;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 7f);
        maxHorizontal = (enemyUfo.transform.position.x + 3);
        minHorizontal = (enemyUfo.transform.position.x - 3);

        // set transforms for cannons
        cannon1 = cannonParent.transform.GetChild(0);
        cannon2 = cannonParent.transform.GetChild(1);
        cannon3 = cannonParent.transform.GetChild(2);
    }

    // Update is called once per frame
    void Update()
    {
        // if game is not paused.
        if (Time.timeScale > 0)
        {
            moveObject();
            fireWeapon();
            updateState();
        }

    }

    private void updateState()
    {
        // any code needed to update at the end of each frame
        // typically for tracking camera movement and player positions.
    }

    private void fireWeapon()
    {
        if (fireCount >= FIRE_RATE)
        {
            //Debug.Log("UFO FIRED");
            Rigidbody beam1;
            Rigidbody beam2;
            Rigidbody beam3;

            beam1 = Instantiate(cannonBeam, cannon1) as Rigidbody;
            beam2 = Instantiate(cannonBeam, cannon2) as Rigidbody;
            beam3 = Instantiate(cannonBeam, cannon3) as Rigidbody;

            beam1.AddForce(cannon1.forward * 2500);
            beam2.AddForce(cannon2.forward * 2500);
            beam3.AddForce(cannon3.forward * 2500);
            fireCount = 0;
        }
        else
        {
            fireCount += Time.deltaTime;
        }
    }

    private void moveObject()
    {
        // UFO Movement
        pos = transform.position;

        if (moveLeft)
        {
            pos.x += (speed * Time.deltaTime);
        }
        else
        {
            pos.x -= (speed * Time.deltaTime);
        }

        if (pos.x >= maxHorizontal)
        {
            pos.x = maxHorizontal;
            moveLeft = false;
        }
        else if (pos.x <= minHorizontal)
        {
            pos.x = minHorizontal;
            moveLeft = true;
        }

        transform.position = pos;
    }

    public void takeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            // death script.
            PlayerInfo.addPoints(5000);
            gameObject.SetActive(false);
            Destroy(gameObject);
            //increase player score
        }
    }
}
