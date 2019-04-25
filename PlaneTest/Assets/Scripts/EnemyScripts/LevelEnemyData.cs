using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnemyData : MonoBehaviour
{
    public int spawnCount = 0;

    public GameObject enemyTypeOne;
    public GameObject enemyTypeTwo;
    //public GameObject enemyTypeThree;

    private Queue<Spawner> spawns;
    private bool isReady = false;
    private bool beginExit = false;

    private float exitCount = 0;
    private float exitTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        spawnCount = GameObject.FindGameObjectsWithTag("SpawnPoint").Length;
        spawns = new Queue<Spawner>();
        // on level start, load list of enemies into a Queue or some other struct.
        // Queue should be game objets?

        for (int i = 0; i < spawnCount; i++)
        {
            int randEnemyType = Random.Range(1, 3);  // max is exclusive - will only generate 1 or 2
            int randEnemyNum = Random.Range(1, 4);   // max is exclusive - will only generate 1, 2, or 3

            if (randEnemyType == 1)
            {
                spawns.Enqueue(new Spawner(enemyTypeOne, randEnemyNum));
            }
            else if (randEnemyType == 2)
            {
                spawns.Enqueue(new Spawner(enemyTypeTwo, randEnemyNum));
            }
        }
        isReady = true;
        Debug.Log("Spawns Populated: " + spawns.Count);
    }

    private void Update()
    {
        if (isReady)
        {
            Debug.Log("Spawner Ready");
            if (spawns.Count <= 0)
            {
                Debug.Log("Spawner has 0 enemies");
                if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
                {
                    Debug.Log("Ready to Exit");
                    // if the spawner was populated, if spawner is empty, and if no enemies are in the scene
                    // begin exit sequence.
                    beginExit = true;
                }
            }
        }

        // exit sequence - wait 3 seconds and load new post game scene.
        if (beginExit)
        {
            Debug.Log("Exit Sequence has started");

            if (exitCount < exitTime)
            {
                Debug.Log("Exit Timer Value: " + exitCount);
                exitCount += Time.deltaTime;
            }
            else
            {
                Debug.Log("GAME NOW EXITING");
                SceneManager.LoadScene("PostGame");
            }

        }
    }

    public Spawner getNextSpawn()
    {
        Spawner returnSp = spawns.Dequeue();
        Debug.Log("Enemy Spawned.  Count Remaining: " + spawns.Count);
        return returnSp;
    }

    public bool hasSpawns()
    {
        if (spawns.Count <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
