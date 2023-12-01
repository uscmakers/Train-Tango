using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject gameOverUI;
    
    bool alive = true;
    public float speed = 5;
    [SerializeField] Rigidbody rb;

    float horizontalInput;

    [SerializeField] float laneDistance = 4f; // Distance between each lane
    // Define lane positions
    float leftLane = -3f;
    float middleLane = 0f;
    float rightLane = 3f;
    float currentLaneX = 0f; // Start in the middle lane
    [SerializeField] float horizontalMultiplier = 2;

    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;

    private void FixedUpdate() 
    {
        if (!alive) return;

        // Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        // Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        // rb.MovePosition(rb.position + forwardMove + horizontalMove);

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 targetPosition = new Vector3(currentLaneX, rb.position.y, rb.position.z);
        Vector3 moveVector = Vector3.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(moveVector + forwardMove);
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Lane change
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (transform.position.y < -5)
        {
            Die();
        }
    }

    public void Die ()
    {
        alive = false;
        // Restart the game
        // Invoke("Restart", 2);
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MoveLeft()
    {
        if (currentLaneX == middleLane)
        {
            currentLaneX = leftLane;
        }
        else if (currentLaneX == rightLane)
        {
            currentLaneX = middleLane;
        }
    }
    public void MoveRight()
    {
        if (currentLaneX == middleLane)
        {
            currentLaneX = rightLane;
        }
        else if (currentLaneX == leftLane)
        {
            currentLaneX = middleLane;
        }
    }
    public void Jump ()
    {
        // Check wether we are currently grounded
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);
        // If we are, jump
        rb.AddForce(Vector3.up * jumpForce);
    }

}
