using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 0.12f;


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed);

        if (transform.position.y >= 5)
        {
            Destroy(gameObject);
        }   
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Brick" || target.tag == "Brick1" || target.tag == "Brick2" || target.tag == "Brick3" || target.tag == "Brick4")
        {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 100);

            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }


}
