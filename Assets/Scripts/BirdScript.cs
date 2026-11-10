using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public float outZone = 12;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.isGameStarted == false || birdIsAlive == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive == true)
        {
            logic.PlayFlapSound();
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }
        // Use camera field to calculate when the bird falls off player screen or go up too high
        float screenTop = Camera.main.transform.position.y
                  + Camera.main.orthographicSize;

        float screenBottom = Camera.main.transform.position.y
                             - Camera.main.orthographicSize;

        // Get bird's collider size
        float birdHeight = GetComponent<Collider2D>().bounds.extents.y;

        float minY = screenBottom + birdHeight;
        float maxY = screenTop - birdHeight;

        if (transform.position.y < minY ||
            transform.position.y > maxY)
        {
            // Keep bird inside camera boundaries
            Vector3 pos = transform.position;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            transform.position = pos;

            StopBird();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (birdIsAlive == true && logic.isGameStarted == true)
        {
            StopBird();
        }
    }

    public void StopBird()
    {
        birdIsAlive = false;

        // Stop bird's movement
        myRigidbody.linearVelocity = Vector2.zero;

        // Disable gravity
        myRigidbody.gravityScale = 0;

        // Freeze bird's position and rotation
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        logic.gameOver();
    }
}
