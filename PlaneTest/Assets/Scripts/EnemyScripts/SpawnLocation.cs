using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    public GameObject spawnLocation;
    public GameObject levelEnemyData;
    private Spawner localSpawner;
    private bool isSpawned = false;

    private const float SPAWN_DISTANCE = 30.0f;
    private float playerZ;

    void Start()
    {
    }

    void Update()
    {
        // check for spawn condiditon each frame?
        // If player is within area, or whatever triggers spawn, then
        // trigger spawn.
        playerZ = GameObject.FindWithTag("MainCamera").transform.position.z;    // every frame find camera "Z" axis position

        if ((spawnLocation.transform.position.z - playerZ) < SPAWN_DISTANCE) // find how far player is on Z-axis, if within spawn dist
        {
            if (!isSpawned)                                                   // check to see if spawned
            {
                if (levelEnemyData.GetComponent<LevelEnemyData>().hasSpawns()) // check to see if there are spawns in queue
                {
                    localSpawner = levelEnemyData.GetComponent<LevelEnemyData>().getNextSpawn();  // if so spawn.
                    triggerSpawn();
                }
            }
        }
    }

    public void triggerSpawn()
    {
        if (!isSpawned)
        {
            isSpawned = true;
            localSpawner.setLocation(spawnLocation.transform);
            localSpawner.spawn();
        }
    }
}
