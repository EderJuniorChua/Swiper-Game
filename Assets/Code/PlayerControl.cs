using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //comment to push
    // this is to make player move
    private float dir = 1;
    public float speed;
    public float jumpHeight;
    private Rigidbody2D playerRB;
    bool facingRight = true;

   //touch/swipecontrol
   //these two will help us know what exactly is a swipe
    public float maxSwipeTime;//0.5
    public float minSwipeDistance;//100


    //these three will help us know how long did our swipe took
    private float startTime;
    private float endTime;
    private float swipeTime;//this will be compared with maxTime;


    //these three will help us know how long the swipe is
    private Vector2 swipeStartPos;
    private Vector2 swipeEndPos;
    private Vector2 swipeDirection;
    private float swipeDistance;//this will be compared with minSwipeDistance;

    //is this rigidbody?
    //private GameObject bullet;  

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //calculate speed based on swipe distance, and bullet (spawn) limit based on time
        //playerRB.velocity = new Vector2(dir * speed * Time.deltaTime, playerRB.velocity.y);
        //ArrowKeyMovement();
        //SpaceBarJump();
        
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
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTime = Time.time;//the time when we left th screen
                swipeEndPos = touch.position;//the position when we left the screen

                swipeTime = endTime - startTime;//this will calculate how long our swip took
                swipeDirection = (swipeStartPos - swipeEndPos);
                swipeDistance = (swipeEndPos - swipeStartPos).magnitude; //this will calculate how long our swipe is

                //Constraint
                if (swipeTime < maxSwipeTime && swipeDistance > minSwipeDistance)
                {//here if we swipe fast and long enough then it will be a swipe
                    //spawnBullet();
                    GetComponent<Rigidbody2D>().AddForce(swipeDirection * (5f));
                    SwipeControl();
                }
            }
        }
    }

    void spawnBullet()
    {
        //quarternion can set depending on angle
        Rigidbody2D bulletClone = (Rigidbody2D) Instantiate(playerRB, playerRB.position, Quaternion.identity);
        bulletClone.velocity = Vector2.up * speed;
        //GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        //destroyRB(bulletClone, 2f);
    }

    void destroyRB(Rigidbody2D rb, float delaytoDestroy)
    {
        StartCoroutine(Wait(delaytoDestroy));

        IEnumerator Wait(float delay)
        {
            yield return new WaitForSeconds(delay);
            {
                Destroy(rb);
            }
        }
    }

    void SwipeControl2()
    {
        Vector2 distance = swipeEndPos - swipeStartPos;
        float xDistance = Mathf.Abs(distance.x);
        float yDistance = Mathf.Abs(distance.y);
        float slope = (swipeEndPos.y - swipeStartPos.y) / (swipeEndPos.x - swipeStartPos.x); //y2-y1 over x2-x1
        bool swipeUp = swipeStartPos.y < swipeEndPos.y; //from lower y to higher y, it swipes up

        //Vector3 forceDirection = camera.ScreenToWorldPoint(swipeEndPos) - camera.ScreenToWorldPoint(swipeStartPos);

        if (xDistance > yDistance)//left and right
        {

            Debug.Log("horizontal swipe");
            if (distance.x > 0 /*&& !facingRight*/)
            {
                Debug.Log("Right");
                //FlipAndMove();
            }
            else if (distance.x < 0 /*&& facingRight == true*/)
            {
                //your swiping left
                Debug.Log("Left");
                //FlipAndMove();
            }
        }
        if (xDistance < yDistance)//if you are swiping up or down
        {
            Debug.Log("vertical swipe");
            if (distance.y > 0)
            {
                Debug.Log("Up");
                //your swiping up
                playerRB.velocity = Vector2.up * jumpHeight * Time.deltaTime;
            }
            else if (distance.y < 0)
            {
                Debug.Log("Down");
                // your swiping down
                //playerRB.velocity = Vector2.up * jumpHeight * Time.deltaTime;
            }

        }
    }

    void SwipeControl()
    {
        Vector2 distance = swipeEndPos - swipeStartPos;
        float xDistance = Mathf.Abs(distance.x);
        float yDistance = Mathf.Abs(distance.y);
        if (xDistance > yDistance)//left and right
        {

            Debug.Log("horizontal swipe");
            if (distance.x > 0 /*&& !facingRight*/)
            {
                Debug.Log("Right");
                //FlipAndMove();
            }
            else if (distance.x < 0 /*&& facingRight == true*/)
            {
                //your swiping left
                Debug.Log("Left");
                //FlipAndMove();
            }
        }
        if (xDistance < yDistance)//if you are swiping up or down
        {
            Debug.Log("vertical swipe");
            if (distance.y > 0)
            {
                Debug.Log("Up");
                //your swiping up
                playerRB.velocity = Vector2.up * jumpHeight * Time.deltaTime;
            }
            else if (distance.y < 0)
            {
                Debug.Log("Down");
                // your swiping down
                //playerRB.velocity = Vector2.up * jumpHeight * Time.deltaTime;
            }

        }
    }

    //void ArrowKeyMovement()
    //{//makes player flip left and right and move left and right
    //    if (Input.GetKeyDown(KeyCode.A) && facingRight)
    //    {
    //        FlipAndMove();//player will flip and go left

    //    }
    //    else if (Input.GetKeyDown(KeyCode.D) &&  !facingRight)
    //    {
    //        FlipAndMove();// player will flip and go right
    //    }
    //}

    void FlipAndMove()
    {//makes the player flip and move
        dir = -dir;
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    //void SpaceBarJump()
    //{// makes player jump
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        playerRB.velocity = Vector2.up * jumpHeight * Time.deltaTime;
    //    }
    //} 




}
