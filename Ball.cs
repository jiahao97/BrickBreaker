using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public GameObject[] powerUps;

    public float speed, multiplier, constantSpeed;
    public float ballDir;
    private int score;

    public Transform startPoint;
    private Rigidbody2D myBody;

    public bool canMove, shoot;

    public AudioClip hitBrickClp;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (!shoot)
            {
                shoot = true;
                myBody.AddForce(new Vector2(ballDir, speed * multiplier));
            }
        }
        else
        {
            transform.position = startPoint.position;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            canMove = true;
        }

        myBody.velocity = constantSpeed * (myBody.velocity.normalized);
    }

    private void OnCollisionEnter2D(Collision2D target)
    {

        if(target.gameObject.tag == "Brick")
        {
            Destroy(target.gameObject, 0.01f);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 100);
            
            if (Random.value < .3f)
            {
                int randomPowerUp = Random.Range(0, powerUps.Length);
                Instantiate(powerUps[randomPowerUp], target.transform.position, Quaternion.identity);
            }

            AudioSource.PlayClipAtPoint(hitBrickClp, transform.position);
        }

        if (target.gameObject.tag == "Brick1")
        {
            target.gameObject.tag = "Brick";
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 150); ;

            if (Random.value < .3f)
            {
                int randomPowerUp = Random.Range(0, powerUps.Length);
                Instantiate(powerUps[randomPowerUp], target.transform.position, Quaternion.identity);
            }

            AudioSource.PlayClipAtPoint(hitBrickClp, transform.position);
        }

    }

}
