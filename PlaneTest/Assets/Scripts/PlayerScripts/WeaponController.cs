using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform leftCannon;
    public Transform rightCannon;

    public Rigidbody projectile;

    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Rigidbody projectileInstanceLeft;
                projectileInstanceLeft = Instantiate(projectile, leftCannon.position, leftCannon.rotation) as Rigidbody;
                projectileInstanceLeft.AddForce(leftCannon.forward * 5000);

                Rigidbody projectileInstanceRight;
                projectileInstanceRight = Instantiate(projectile, rightCannon.position, rightCannon.rotation) as Rigidbody;
                projectileInstanceRight.AddForce(rightCannon.forward * 5000);
            }
        }
    }
}
