using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float baseJumpStrength;
    [SerializeField] private int jitterFrequency;
    [SerializeField] private float jitterStrength;
    [SerializeField] private bool activateJitter;
    [SerializeField] private float waterSpeedRatio;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audiosource;
   

    [SerializeField] SpriteRenderer playerSpriteRenderer;

    private Rigidbody2D playerBody;
    private bool isAirBorne;
    private Vector2 position;
    private Vector2 direction;
    private Vector2 footDirection;

    [SerializeField]private bool isInWater;
    private bool isInFlight;
    private float lastGravity;
    private bool isWalking;
    private bool isFlying;

   

     
    


    // Start is called before the first frame update
    void Start()
    {
        isInWater = false;
        isInFlight = false;
        baseMoveSpeed *= 10;
        footDirection = Vector2.down;
        playerBody = GetComponent<Rigidbody2D>();
        isWalking = animator.GetBool("IsWalking");
        isFlying = animator.GetBool("IsFlying");
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        position = playerBody.transform.position;
        direction = Vector2.zero;

        if (isInFlight)
        {
            FlightMechanics();
        } else if (IsInWater())
        {
            WaterMechanics();
        } else
        {
            GroundMechanics();
        }
    }

    void GroundMechanics()
    {
       
        
        
        if (isInWater)
        {
            animator.SetBool("IsFlying", false);
            playerBody.gravityScale /= waterSpeedRatio;
            isInWater = false;

           
        }
        direction.x = Input.GetAxisRaw("Horizontal");
        direction = direction.normalized;

        if (direction.x < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            playerSpriteRenderer.flipX = false;
        }

        

        Vector2 velocity = baseMoveSpeed * Time.fixedDeltaTime * direction;
        velocity.x = Mathf.Lerp(playerBody.velocity.x, velocity.x, 0.25f);
        velocity.y = playerBody.velocity.y;

        if ((Input.GetAxisRaw("Vertical") > 0.5 || Input.GetKey(KeyCode.Space)) && !isAirBorne)
        {
            velocity = Jump(velocity, baseJumpStrength);
        }

        if ((direction.x > 0.02f || direction.x < -0.02f) && !isWalking && !isFlying)
        {
            Debug.Log("Moving");
            animator.SetBool("IsWalking", true);
        }
        else {
            animator.SetBool("IsWalking", false);
        }


        playerBody.velocity = Jitter(velocity);

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

        
    }



    void WaterMechanics()
    {

        if (!isInWater)
        {
            playerBody.gravityScale *= waterSpeedRatio;
            isInWater = true;
            animator.SetBool("IsFlying", true);
            animator.SetBool("IsWalking", false);
        }


        direction.x = Input.GetAxisRaw("Horizontal"); 
        direction.y = Input.GetAxisRaw("Vertical") * -footDirection.y;

        if (direction.x < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            playerSpriteRenderer.flipX = false;
        }

        direction = direction.normalized;
        Vector2 velocity = baseMoveSpeed * Time.fixedDeltaTime * direction;
        velocity.x = Mathf.Lerp(playerBody.velocity.x, velocity.x, 0.2f);
        velocity.y = Mathf.Max(Mathf.Lerp(playerBody.velocity.y, velocity.y, 0.2f), -20);
        playerBody.velocity = velocity;


    }

    void FlightMechanics() 
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction = direction.normalized;
        Vector2 velocity = baseMoveSpeed * Time.fixedDeltaTime * direction; 
        velocity.x = Mathf.Lerp(playerBody.velocity.x, velocity.x, 0.1f);
        velocity.y = Mathf.Lerp(playerBody.velocity.y, velocity.y, 0.1f);
        Debug.Log(velocity);
        playerBody.velocity = Jitter(velocity);


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TriggerEnter");
        Debug.Log(other.gameObject.layer);
        Debug.Log(LayerMask.NameToLayer("FlightGate"));
        if (other.gameObject.layer == LayerMask.NameToLayer("FlightGate") && !isInFlight)
        {
            Debug.Log("Enter Flight");
            isInFlight = true;
            isAirBorne = true;
            lastGravity = playerBody.gravityScale;
            playerBody.gravityScale = 0;
        } else if (other.gameObject.layer == LayerMask.NameToLayer("FlightGate") && isInFlight)
        {
            isInFlight = false;
            playerBody.gravityScale = lastGravity;
        }
    }

    bool IsInWater()
    {
        return Physics2D.OverlapBox(position, Vector2.one * 0.6f, 0f, LayerMask.GetMask("Water"));
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(position + footDirection * 0.5f, 0.3f, LayerMask.GetMask("Ground")) != null;
    }

    Vector2 Jump(Vector2 velocity, float jumpStrength)
    {
        isAirBorne = true;
        velocity.y = jumpStrength * playerBody.gravityScale;
        return velocity;
    }
    
    bool TouchesReverseGravity()
    {
        return Physics2D.OverlapBox(position, Vector2.one * 1.1f, 0f, LayerMask.GetMask("RevertGravity")) != null;
        //return Physics2D.OverlapCircle(position+ footDirection * 0.5f, 0.1f, LayerMask.GetMask("RevertGravity")) != null;
    }

    Vector2 Jitter(Vector2 velocity)
    {
        if (Random.Range(0, 101) < jitterFrequency && activateJitter)
        {
            Vector2 jitterVelocity = Vector2.zero;
            jitterVelocity.x = Random.Range(-1f, 1f);
            jitterVelocity.y = Random.Range(-1f, 1f);
            jitterVelocity = jitterVelocity.normalized;
            jitterVelocity = baseMoveSpeed * Time.fixedDeltaTime * jitterStrength * jitterVelocity.normalized;
            return isAirBorne ? velocity + jitterVelocity : Jump(velocity + jitterVelocity, baseJumpStrength * jitterStrength);
        }
        return velocity;
    }
}
