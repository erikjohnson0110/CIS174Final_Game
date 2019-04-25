using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starSprite;    // the sprite to spawn
    public Transform spawnPoint; // empty game object, spawn point.
    public float spawnFrequency = 1f;

    float spawnTime;
    float currentTime;

    private void Start()
    {
        spawnSprite();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        if ((currentTime - spawnTime) > spawnFrequency)
        {
            spawnSprite();
        }
    }

    void spawnSprite()
    {
        // rotation randomizer
        float rand = Random.Range(0, 50);
        Quaternion rotation = spawnPoint.rotation;
        rotation.z = rand;

        //position randomizer
        Vector3 pos = spawnPoint.position;
        float xRand = Random.Range(-5, 5);
        float yRand = Random.Range(-5, 5);
        pos += new Vector3(xRand, yRand, 0);

        Instantiate(starSprite, pos, rotation);
        spawnTime = Time.time;
    }
}
