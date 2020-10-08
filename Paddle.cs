using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float speed = 0.06f;
    public float xBound = 2.3f;
    public float timer = 8f, laserTimer = 0.5f;
    int direction = 0;
    float previousPositionX;

    public GameObject laser;
    public Transform pos1, pos2;
    private GameObject ball;
    private float speedBall;

    public AudioClip laserShotClip, ballClip;

    // Start is called before the first frame update
    void Start()
    { 
        ball = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
            Movement();

        if(direction > 1)
        {
            ball.GetComponent<Ball>().ballDir = 100;
        }else if (direction < -1)
        {
            ball.GetComponent<Ball>().ballDir = -100;
        }
        else
        {
            ball.GetComponent<Ball>().ballDir = 0;
        }
    }

    private void LateUpdate()
    {
        previousPositionX = transform.position.x;
    }

    void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if(h > 0)
        {
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }else if( h < 0)
        {
            transform.position = new Vector2(transform.position.x - speed, transform.position.y);
        }

        if(previousPositionX > transform.position.x)
        {
            direction = -1;
        }else if (previousPositionX < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -xBound, xBound), transform.position.y);

    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        float adjust = 3 * direction;
        target.rigidbody.velocity = new Vector2(target.rigidbody.velocity.x + adjust, target.rigidbody.velocity.y);

        AudioSource.PlayClipAtPoint(ballClip, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "BallExpand")
        {
            StartCoroutine(BallExpand());
            Destroy(target.gameObject);
        }

        if (target.tag == "BallSpeedUp")
        {
            StartCoroutine(BallSpeedUp());
            Destroy(target.gameObject);
        }

        if (target.tag == "BallSlow")
        {
            StartCoroutine(BallSlow());
            Destroy(target.gameObject);
        }

        if (target.tag == "SlideExpand")
        {
            StartCoroutine(Expand());
            Destroy(target.gameObject);
        }

        if (target.tag == "SlideShrink")
        {
            StartCoroutine(Shrink());
            Destroy(target.gameObject);
        }

        if (target.tag == "Laser")
        {
            StartCoroutine(LaserShooting());
            Destroy(target.gameObject);
        }
        if (target.tag == "Skull")
        {
            Destroy(target.gameObject);
            //Scxx
        }
    }

    IEnumerator Expand()
    {
        transform.localScale = new Vector2(0.6f, transform.localScale.y);
        
        yield return new WaitForSeconds(timer);

        transform.localScale = new Vector2(0.4f, transform.localScale.y);
    }

    IEnumerator Shrink()
    {
        transform.localScale = new Vector2(0.25f, transform.localScale.y);

        yield return new WaitForSeconds(timer);

        transform.localScale = new Vector2(0.4f, transform.localScale.y);
    }

    IEnumerator BallExpand()
    {
        ball.transform.localScale = new Vector2(0.5f, 0.5f);

        yield return new WaitForSeconds(timer);

        ball.transform.localScale = new Vector2(0.3f, 0.3f);
    }

    IEnumerator BallSpeedUp()
    {
        ball.GetComponent<Ball>().constantSpeed = 7f;

        yield return new WaitForSeconds(timer);

        ball.GetComponent<Ball>().constantSpeed = 5f;
    }

    IEnumerator BallSlow()
    {
        ball.GetComponent<Ball>().constantSpeed = 3.5f;

        yield return new WaitForSeconds(timer);

        ball.GetComponent<Ball>().constantSpeed = 3.5f;
    }

    IEnumerator LaserShooting()
    {
        for (int i = 0; i < 6; i++)
        {
            Instantiate(laser, pos1.position, Quaternion.identity);
            Instantiate(laser, pos2.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(laserShotClip, transform.position);
            yield return new WaitForSeconds(laserTimer);
        }

    }
}
