using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonBeam : MonoBehaviour
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
        //Debug.Log("Enemy Beam Hit" + other.gameObject.ToString());
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerControler>().takeDamagePlayer(BEAM_DMG);
            // add explosive effect?
            gameObject.SetActive(false);
        }

    }
}
