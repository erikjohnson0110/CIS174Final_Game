using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBeam : MonoBehaviour
{
    private const int BEAM_DMG = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<Scout>() != null)
            {
                other.gameObject.GetComponent<Scout>().takeDamage(BEAM_DMG);
            }

            if (other.gameObject.GetComponent<UFO>() != null)
            {
                other.gameObject.GetComponent<UFO>().takeDamage(BEAM_DMG);
            }

            if (other.gameObject.GetComponent<Corvette>() != null)
            {
                other.gameObject.GetComponent<Corvette>().takeDamage(BEAM_DMG);
            }
        }
        
        // add explosive effect?
        gameObject.SetActive(false);
    }
}
