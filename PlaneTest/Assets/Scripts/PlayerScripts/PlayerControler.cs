using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    // camera to track
    public Camera gameCam;
    public Slider HPVal;
    public Text ScoreText;

    // hold position of camera last frame
    private float camZPosLastFrame;
    // distanace camera moved between frames.
    private float camZFrameDist;

    //move speed modifier
    public float moveSpeed = 5f;

    // store values from input
    private float horizontal;
    private float vertical;

    //set boundries for input
    private float maxHorizontal;
    private float minHorizontal;
    private float maxVertical;
    private float minVertical;

    private const int MAX_PLAYER_HP = 3;
    public int playerHealth = MAX_PLAYER_HP;

    private bool playerDead = false; // controls exiting level
    private float exitTime = 0;

    void Start()
    {
        HPVal.maxValue = MAX_PLAYER_HP;
        HPVal.minValue = 0;
        HPVal.value = playerHealth;
        ScoreText.text = PlayerInfo.getScoreString();

        // set last frame position for first frame.
        camZPosLastFrame = gameCam.transform.position.z;

        // set horizontal boundary.
        maxHorizontal = (gameCam.transform.position.x + 10);
        minHorizontal = (gameCam.transform.position.x - 10);

        // set initial vertical boundary.
        maxVertical = (gameCam.transform.position.z + 20f);
        minVertical = (gameCam.transform.position.z + 5.0f);
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            ScoreText.text = PlayerInfo.getScoreString();

            //before user input
            //calculate distance camera has moved on z axis since last frame.
            camZFrameDist = gameCam.transform.position.z - camZPosLastFrame;
            //move objet forward by amt camera has moved.
            transform.position += new Vector3(0, 0, camZFrameDist);

            //update boundary box Z values
            maxVertical += camZFrameDist;
            minVertical += camZFrameDist;

            //get user input
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            // get current positon and save to addition vector3
            Vector3 pos = transform.position;
            // add movement value to addition Vector3
            pos += new Vector3((horizontal * (moveSpeed * Time.deltaTime)), 0, (vertical * (moveSpeed * Time.deltaTime)));

            //check value
            if (pos.x > maxHorizontal)
            {
                pos.x = maxHorizontal;
            }
            else if (pos.x < minHorizontal)
            {
                pos.x = minHorizontal;
            }

            if (pos.z > maxVertical)
            {
                pos.z = maxVertical;
            }
            else if (pos.z < minVertical)
            {
                pos.z = minVertical;
            }
            transform.position = pos;

            //rotation calcs?

            // end of frame.
            // save camera Z for next frame.
            camZPosLastFrame = gameCam.transform.position.z;

            // if player is dead, count to 5 seconds and then exit level
            if (playerDead)
            {
                if (exitTime >= 5)
                {
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    exitTime += Time.deltaTime;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerHealth -= 3;
        }

        if (playerHealth <= 0)
        {
            playerDeath();
        }
    }

    public void takeDamagePlayer(int dmg)
    {
        playerHealth -= dmg;
        if (playerHealth <= 0)
        {
            playerDeath();
        }
        HPVal.value = playerHealth;
    }

    public void playerDeath()
    {
        // deactivate player game object
        gameObject.SetActive(false);
        // display explosive effect.
        playerDead = true;
        SceneManager.LoadScene("PostGame");
    }

}
