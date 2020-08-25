using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform brickBreaking;
    public GameManager gm;
    public Transform powerup;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            return;
        }

        if (!inPlay)
        {
            transform.position = paddle.position;
        }

        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bottom"))
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Brick"))
        {
            if (collision.gameObject.GetComponent<Brick>().hitsNo > 1)
            {
                collision.gameObject.GetComponent<Brick>().BreakBrick();
            }
            else
            {
                int randChance = Random.Range(1, 101);
                if (randChance < 30)
                {
                    Instantiate(powerup, collision.transform.position, collision.transform.rotation);
                }

                Transform temp = Instantiate(brickBreaking, collision.transform.position, collision.transform.rotation);
                Destroy(temp.gameObject, 2.5f);
                gm.UpdateScore(collision.gameObject.GetComponent<Brick>().points);
                gm.UpdateNumberOfBricks();
                Destroy(collision.gameObject);
            }

            audio.Play();
        }
    }
}
