using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    //Script from components
    private Projectile Projectile;
    public ActionButtons ActionButtons; //get Canvas ("Menu") Buttons

    //Player RigidBody
    private Rigidbody2D playerRB;

    //Swipe Constraints
    public float maxSwipeTime;//0.5
    public float minSwipeDistance;//100

    //Swipe Timing
    private float startTime;
    private float endTime;
    private float swipeTime;//this will be compared with maxTime;

    //Swipe Positions and Geometry
    private Vector2 swipeStartPos;
    private Vector2 swipeEndPos;
    private Vector2 swipeDirection;
    private float swipeDistance;//this will be compared with minSwipeDistance;

    private Vector2 worldSwipeStartPos; //swipe position transform on world coordinates


    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        Projectile = GetComponent<Projectile>();
    }

    void Update()
    {
        //TODO: create bullet (spawn) limit based on time using object pooling
        SwipeTest();
    }


    void SwipeTest()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTime = Time.time;//this will see when we started touching the screen
                swipeStartPos = touch.position;//where we have started touching the screen
                worldSwipeStartPos = Camera.main.ScreenToWorldPoint(swipeStartPos);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTime = Time.time;//the time when we left th screen
                swipeEndPos = touch.position;//the position when we left the screen

                swipeTime = endTime - startTime;//this will calculate how long our swip took
                swipeDirection = swipeEndPos - swipeStartPos;
                swipeDistance = swipeDirection.magnitude; //this will calculate how long our swipe is

                //Constraints
                if (swipeTime < maxSwipeTime && swipeDistance > minSwipeDistance)
                {
                    switch (ActionButtons.getCurrentAction())
                    {
                        case ActionButtons.Action.BULLET:
                            //Debug.Log(ActionButtons.Action.BULLET.ToString());
                            //playerRB.hide
                            Projectile.spawn(swipeDirection, worldSwipeStartPos); //bullet spawn
                            break;
                        case ActionButtons.Action.CHARACTER:
                            //Debug.Log(ActionButtons.Action.CHARACTER.ToString());
                            playerRB.AddForce(-swipeDirection * (5f)); //player movement
                            break;
                        case ActionButtons.Action.CHARACTERANDBULLET:
                            //Debug.Log(ActionButtons.Action.CHARACTERANDBULLET.ToString());
                            if (touch.position.x < Screen.width / 2)
                            {
                                Debug.Log("Left click");
                                playerRB.AddForce(-swipeDirection * (5f));
                            }
                            else if (touch.position.x > Screen.width / 2)
                            {
                                Debug.Log("Right click");
                                Projectile.spawn(swipeDirection, playerRB.position); //bullet spawn on player's position
                            }
                            break;
                    }
                }
            }
        }
    }




}
