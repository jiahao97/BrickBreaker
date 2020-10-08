using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public Sprite yellowBrick;
    private SpriteRenderer _spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag == "Brick")
        {
            _spriteRenderer.sprite = yellowBrick;
        }
    }
}
