using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float speed = 3f;
    private float rayDistance = 2f;
    private bool movingRight;
    public Transform groundCollision;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundCheck = Physics2D.Raycast(groundCollision.position, Vector2.down, rayDistance);


        if (!groundCheck.collider)
        {

            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }

        }
    }
}
