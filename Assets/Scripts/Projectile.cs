using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D bullet;
    public float speed;//5f
    public float bulletTimer;//1f


    public void spawn(Vector2 swipeDirection, Vector2 launcherPosition)
    {
        Rigidbody2D clone = Instantiate(bullet, launcherPosition, Quaternion.identity);
        clone.AddForce(-swipeDirection * speed);
        Destroy(clone.gameObject, bulletTimer);
        //bulletClone.velocity = -swipeDirection * speed * Time.deltaTime;
    }

    public void clickSpawn(Vector2 spawnPosition)
    {
        Rigidbody2D clone = Instantiate(bullet, spawnPosition, Quaternion.identity);
        //Destroy(clone.gameObject, projectileTimer);
        //clone.velocity = -swipeDirection * speed * Time.deltaTime;
    }

}
