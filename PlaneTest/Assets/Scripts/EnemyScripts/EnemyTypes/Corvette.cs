using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corvette : MonoBehaviour
{
    public GameObject enemyCorvette;

    private int health = 5;
    private const int MAX_HP = 5;

    private Vector3 circleCenter;
    private float radius = 5;
    private float angle = 90; // 0 to 360? - 90 starts at 12 O'Clock on a circle?

    private Vector3 cameraPosCurrentFrame;
    private Vector3 cameraPosLastFrame;
    private Vector3 cameraChangeAmt;

    // Start is called before the first frame update
    void Start()
    {
        circleCenter = (transform.position += new Vector3(0, 0, -5));
        cameraPosCurrentFrame = GameObject.FindWithTag("MainCamera").transform.position;
        cameraPosLastFrame = cameraPosCurrentFrame;
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
            gameObject.SetActive(false);
            //increase player score
        }
    }

    private void moveObject()
    {
        cameraPosCurrentFrame = GameObject.FindWithTag("MainCamera").transform.position;
        cameraChangeAmt = (cameraPosCurrentFrame - cameraPosLastFrame);

        circleCenter += cameraChangeAmt; // move center up by amt of camera movement.

        // calculate path around circle.  we know angle and radius.
        // x = (rad * cos(angle)) + x_offset from camera movement;
        // y = (rad * sin(angle)) + y_offset from camera movement;
        transform.position = new Vector3(((radius * Mathf.Sin(angle)) + circleCenter.x), 0, ((radius * Mathf.Cos(angle)) + circleCenter.z));
    }

    private void updateState()
    {
        // last action of frame, update camera position for last frame
        cameraPosLastFrame = cameraPosCurrentFrame;
        // increment angle
        if (angle < 360)
        {
            angle += Time.deltaTime;
        }
        else
        {
            angle = 0;
        }
    }

    private void fireWeapon()
    {

    }
}
