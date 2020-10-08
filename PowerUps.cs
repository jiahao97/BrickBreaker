using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public float speed = 0.03f;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 6f);   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - speed);
    }
}
