using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    public int speed;

    public string atkSound;

    private bool isRight;
    private bool isLeft;

    private bool moveRight = true;
    private bool moveLeft = false;

    void Start()
    {
        Invoke("Delete", 15f);

        if (gameObject.transform.position.x < 0)
        {
            isRight = true;
            isLeft = false;
        }
        else if (gameObject.transform.position.x > 0)
        {
            isRight = false;
            isLeft = true;
        }
        speed = 10;
    }
    private void Update()
    {
        if(isRight)
        {
            transform.localScale = new Vector3(-6f, 6f, 1f);
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (isLeft)
        {
            transform.localScale = new Vector3(6f, 6f, 1f);
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            Delete();
        }
    }

    void Delete()
    {
        gameObject.SetActive(false);
    }
}
