using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    private GameObject[] bricks1, bricks2;
    private Ball ball;

    public int brickCounter;
    private int life = 2;
    private bool died;
    private float yBound = -5.26f;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        ball = GameObject.FindObjectOfType<Ball>();
        PlayerPrefs.SetInt("Score", 0);

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        BrickCounter();
        ScoreTextFunction();
        LifeCounter();

        if(brickCounter == 0)
        {
            GameController.instance.NextLevelPanel();
        }

    }

    void LifeCounter()
    {
        if (ball.transform.position.y < yBound)
        {
            GameController.instance.lifeScore.text = life.ToString();

            died = true;
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if(life == 0)
            {
                GameController.instance.Died();
            }
            else if(life > 0)
            {
                ball.canMove = false;
                ball.shoot = false;
            }

            if (died)
            {
                if (Time.timeScale == 1)
                    life--;

                died = false;
            }

           
        }
    }

    void BrickCounter()
    {
        bricks1 = GameObject.FindGameObjectsWithTag("Brick");
        bricks2 = GameObject.FindGameObjectsWithTag("Brick1");

        brickCounter = bricks1.Length + bricks2.Length;
    }

    void ScoreTextFunction()
    {
        GameController.instance.scoreText.text = PlayerPrefs.GetInt("Score").ToString();

        if(PlayerPrefs.GetInt("Score",0) > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
        }
    }
}

