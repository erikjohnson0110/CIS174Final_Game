using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    private GameObject enemyType;
    private Transform location;
    private int numEnemies;

    public Spawner()
    {
        enemyType = null;
        location = null;
        numEnemies = 0;
    }

    public Spawner(GameObject type, int num)
    {
        enemyType = type;
        location = null;
        numEnemies = num;
    }

    public void setEnemyType(GameObject type)
    {
        enemyType = type;
    }

    public void setLocation(Transform l)
    {
        location = l;
    }

    public void setNumberOfEnemies(int i)
    {
        numEnemies = i;
    }

    public void spawn()
    {
        Quaternion parentRotation = location.rotation;
        Vector3 loc1 = location.position;
        Vector3 loc2 = location.position;
        Vector3 loc3 = location.position;

        switch (numEnemies)
        {
            case 1:
                GameObject enemy = Object.Instantiate(enemyType, location);
                break;
            case 2:
                loc1 += new Vector3(5.0f, 0f, 0f);
                GameObject enemyOne = Object.Instantiate(enemyType, loc1, parentRotation);

                loc2 += new Vector3(-5.0f, 0f, 0f);
                GameObject enemyTwo = Object.Instantiate(enemyType, loc2, parentRotation);
                break;
            case 3:

                GameObject enemyOneA = Object.Instantiate(enemyType, loc1, parentRotation);

                loc2 += new Vector3(5.0f, 0f, -3f);
                GameObject enemyTwoA = Object.Instantiate(enemyType, loc2, parentRotation);

                loc3 += new Vector3(-5f, 0f, -3f);
                GameObject enemyThree = Object.Instantiate(enemyType, loc3, parentRotation);
                break;
            default:
                break;
        }
  
    }
}
