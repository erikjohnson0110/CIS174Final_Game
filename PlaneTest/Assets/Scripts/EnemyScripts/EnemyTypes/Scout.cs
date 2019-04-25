using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : MonoBehaviour
{
    public GameObject enemyScout;
    public GameObject cannon;
    public Rigidbody beam;

    public float enemySpeed = 3f;

    private Vector3 selfPos;
    private Vector3 playerPos;
    private Vector3 cameraPosNow;
    private Vector3 cameraPosLast;
    private Vector3 cameraChangeAmt;

    private int health = 6;
    private const int MAX_HP = 6;

    private GameObject nextScout = null;
    private Scout[] scouts;
    private Guid selfID;

    // weapon properties
    private bool readyToFire = false;
    private const float FIRE_INTERVAL = 1;
    private float fireTime = 0;
    private const float PULSE_INTERVAL = 0.25f;
    private float pulseTime = 0;
    private float pulseCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        selfID = Guid.NewGuid();
        scouts = GameObject.FindObjectsOfType<Scout>();
        if (scouts != null)
        {
            foreach (Scout s in scouts)
            {
                if (s.gameObject.activeInHierarchy)
                {
                    if (!selfID.Equals(s.getID()))
                    {
                        if (s.getNextScout() == null)
                        {
                            s.nextScout = gameObject;
                            break;
                        }
                    }
                }
            }
        }

        selfPos = transform.position;
        playerPos = GameObject.FindWithTag("Player").transform.position;
        cameraPosNow = GameObject.FindWithTag("MainCamera").transform.position;
        cameraPosLast = cameraPosNow;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            moveObject();
            fireWeapon();
            updateState();
        }
    }

    public void takeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            // death script.
            PlayerInfo.addPoints(10000);
            gameObject.SetActive(false);
            Destroy(gameObject);
            //increase player score
        }
    }

    private void moveObject()
    {
        // establish locations this frame
        selfPos = transform.position;
        playerPos = GameObject.FindWithTag("Player").transform.position;
        cameraPosNow = GameObject.FindWithTag("MainCamera").transform.position;
        cameraChangeAmt = (cameraPosNow - cameraPosLast);

        if (nextScout == null || !nextScout.activeInHierarchy)
        { 
            // move toward player X value
            if ((playerPos - selfPos).x > 0)
            {
                selfPos.x += (enemySpeed * Time.deltaTime);
            }
            else if ((playerPos - selfPos).x < 0)
            {
                selfPos.x -= (enemySpeed * Time.deltaTime);
            }
            transform.position = selfPos;  

            // track the camera if player is close.
            if ((selfPos.z - cameraPosNow.z) < 15)
            {
                transform.position += cameraChangeAmt;
            }
        }
        else
        {
            if ((selfPos.z - cameraPosNow.z) < 20)
            {
                transform.position += cameraChangeAmt;
            }
        }
    }

    private void fireWeapon()
    {
        if (fireTime >= FIRE_INTERVAL)
        {
            //Debug.Log("READY TO FIRE");
            readyToFire = true;
            fireTime = 0;
        }
        else
        {
            fireTime += Time.deltaTime;
        }

        if (readyToFire)
        {
            if (pulseTime >= PULSE_INTERVAL)
            {
                //Debug.Log("FIRING");
                Rigidbody newBeam = Instantiate(beam, cannon.transform) as Rigidbody;
                newBeam.AddForce(cannon.transform.forward * 2500);
                pulseTime = 0;
                pulseCount++;
            }
            else
            {
                pulseTime += Time.deltaTime;
            }

            if (pulseCount >= 5)
            {
                readyToFire = false;
                pulseCount = 0;
            }
        }

    }

    private void updateState()
    {
        // update positions.
        cameraPosLast = cameraPosNow;
    }

    public Guid getID()
    {
        return selfID;
    }

    public GameObject getNextScout()
    {
        return nextScout;
    }
}
