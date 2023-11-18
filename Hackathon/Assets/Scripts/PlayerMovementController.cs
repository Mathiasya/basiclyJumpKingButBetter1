using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float baseJumpStrength;
    [SerializeField] private int jitterFrequency;

    private Rigidbody2D playerBody;
    private bool isAirBorne;
    Vector2 position;
    Vector2 footDirection;


    // Start is called before the first frame update
    void Start()
    {
        baseMoveSpeed *= 10;
        footDirection = Vector2.down;
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        position = playerBody.transform.position;

        Vector2 direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction = direction.normalized;
        Vector2 velocity = baseMoveSpeed * Time.fixedDeltaTime * direction;
        velocity.x = Mathf.Lerp(playerBody.velocity.x, velocity.x, 0.25f);
        velocity.y = playerBody.velocity.y;

        if ((Input.GetAxisRaw("Vertical") > 0.5 || Input.GetKey(KeyCode.Space)) && !isAirBorne)
        {
            velocity = Jump(velocity, baseJumpStrength);
        }
        playerBody.velocity = velocity;

        if (IsGrounded())
        {
            isAirBorne = false;
        }

        if (TouchesReverseGravity() && !isAirBorne)
        {
            isAirBorne = true;
            footDirection *= -1;
            playerBody.gravityScale *= -1;
        }

        Jitter();
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(position + footDirection * 0.5f, 0.2f, LayerMask.GetMask("Ground")) != null;
    }

    Vector2 Jump(Vector2 velocity, float jumpStrength)
    {
        isAirBorne = true;
        velocity.y = jumpStrength * playerBody.gravityScale;
        return velocity;
    }
    
    bool TouchesReverseGravity()
    {
        return Physics2D.OverlapCircle(position, 1.05f, LayerMask.GetMask("RevertGravity")) != null;
    }

    void Jitter()
    {
        int rand = Random.Range(0,101);
        if (rand < jitterFrequency)
        {

        }
    }
}
