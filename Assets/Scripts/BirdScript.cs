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

        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive == true)
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }
        // Use camera field to calculate when the bird falls off player screen or go up too high
        float screenTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        float screenBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;

        if ((transform.position.y > screenTop + 1f || transform.position.y < screenBottom - 1f) && birdIsAlive == true)
        {
            birdIsAlive = false;
            logic.gameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        birdIsAlive = false;
        logic.gameOver();
    }

}
