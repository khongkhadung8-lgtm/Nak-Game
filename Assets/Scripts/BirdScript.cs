
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public float outZone = 12;

    [Header("Wing Animation")]
    public Animator wingAnimator;
    public string flapStateName = "Wing animation";

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic")
            .GetComponent<LogicScript>();
    }

    void Update()
    {
        if (logic.isGameStarted == false || birdIsAlive == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Phát âm thanh vỗ cánh
            logic.PlayFlapSound();

            // Chạy lại animation từ đầu
            wingAnimator.Play(flapStateName, 0, 0f);

            // Làm chim bay lên
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }

        // Kiểm tra giới hạn trên và dưới của camera
        float screenTop = Camera.main.transform.position.y
                          + Camera.main.orthographicSize;

        float screenBottom = Camera.main.transform.position.y
                             - Camera.main.orthographicSize;

        float birdHeight = GetComponent<Collider2D>().bounds.extents.y;

        float minY = screenBottom + birdHeight;
        float maxY = screenTop - birdHeight;

        if (transform.position.y < minY ||
            transform.position.y > maxY)
        {
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

        myRigidbody.linearVelocity = Vector2.zero;
        myRigidbody.gravityScale = 0;

        myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        logic.gameOver();
    }
}
