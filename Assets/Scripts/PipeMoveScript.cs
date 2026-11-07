using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -45;
    public float speedIncrease = 0.5f;
    private float previousSpeed = -1;
    private LogicScript logic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.isGameStarted == false || logic.isGameOver == true)
        {
            return;
        }

        float currentSpeed = Mathf.Min(moveSpeed + Mathf.FloorToInt(logic.playerScore / 10f) * speedIncrease, 9f);
        // Log only when speed changes
        if (currentSpeed != previousSpeed)
        {
            Debug.Log("Score: " + logic.playerScore + " | Pipe Speed updated: " + currentSpeed);
            previousSpeed = currentSpeed;
        }

        transform.position = transform.position + (Vector3.left * currentSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("Pipe deleted");
            Destroy(gameObject);
        }
    }
}
