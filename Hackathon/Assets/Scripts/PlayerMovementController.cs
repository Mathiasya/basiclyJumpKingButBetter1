using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float jumpingSpeed;

    private Rigidbody2D playerBody;
    private bool isAirBorne;
    

    // Start is called before the first frame update
    void Start()
    {
        baseMoveSpeed *= 10;
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction = direction.normalized;
        Vector2 velocity= direction * baseMoveSpeed * Time.fixedDeltaTime;
        velocity.x = Mathf.Lerp(playerBody.velocity.x, velocity.x, 0.25f);
        velocity.y = playerBody.velocity.y;

        if((Input.GetAxisRaw("Vertical") > 0.5 || Input.GetKey(KeyCode.Space)) && !isAirBorne)
        {
            velocity = Jump(velocity);
        }
        playerBody.velocity = velocity;

        if (IsGrounded())
        {
            isAirBorne = false;
        }
    }

    bool IsGrounded()
    {
        return playerBody.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    Vector2 Jump(Vector2 velocity) 
    {
        isAirBorne = true;
        velocity.y = jumpingSpeed;
        return velocity;
    }
}
