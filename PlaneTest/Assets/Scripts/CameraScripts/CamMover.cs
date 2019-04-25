using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public float speed = 5f;

    private Transform startTransform;
    private Transform endTransform;
    private float endZ;

    void Start()
    {
        startTransform = start.transform;
        endTransform = end.transform;
        endZ = endTransform.position.z;

        transform.LookAt(startTransform);
    }

    void Update()
    {
        // if game not paused
        if (Time.timeScale > 0)
        {
            float currentZ = transform.position.z;

            if (currentZ <= endZ)
            {
                transform.position += (new Vector3(0, 0, (speed * Time.deltaTime)));
            }
        }

    }
}
