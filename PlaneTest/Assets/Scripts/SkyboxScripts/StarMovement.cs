using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMovement : MonoBehaviour
{
    private SpriteRenderer objectSprite;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5.0f);
        objectSprite = gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        objectSprite.color = new Color(255,255,255,0);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position -= new Vector3(0f, 0f, (3f * Time.deltaTime));
        objectSprite.color += new Color(0,0,0,(0.5f * Time.deltaTime));
    }
}
