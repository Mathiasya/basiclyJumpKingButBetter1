using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float speed = 3f;
    private float rayDistance = .5f;
    private bool movingRight;
    public Transform groundCollision;
    [SerializeField] private Animator animator;
    public bool isPlayerNear = false;
    public Transform playerTransform;

    private Quaternion right;
    private Quaternion left;



    private void Awake()
    {
        right = Quaternion.LookRotation(Vector3.forward);
        left = Quaternion.LookRotation(Vector3.back);
    }


    private void Update()
    {
        EnemyPatrol();
    }

    private void EnemyPatrol() {

        RaycastHit2D groundCheck = Physics2D.Raycast(groundCollision.position, Vector2.down, rayDistance);

        if (isPlayerNear && groundCheck.collider)
        {
            Debug.Log("Playernear");
            float xPositionDifference = playerTransform.position.x - transform.position.x;

            transform.rotation = xPositionDifference < 0 ? left : right;

            transform.Translate(Vector2.right * speed * Time.deltaTime);

        }
        else if (isPlayerNear && !groundCheck.collider)
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

            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (!isPlayerNear && groundCheck.collider)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (!isPlayerNear && !groundCheck.collider)
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
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void EnemyAttack() {

        bool enemyAttack = animator.GetBool("attack");
        bool isInTransition = animator.IsInTransition(0);


        if (!enemyAttack) {

           animator.SetBool("attack", true);

        } else if (enemyAttack && !isInTransition) {
            animator.SetBool("attack", false);
        }
        
    }

    

    public void OnTriggerEnter2D(Collider2D collision)
    {

        
       CapsuleCollider2D capsuleCollider = GetComponentInChildren<CapsuleCollider2D>();

        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNear = true;
            EnemyAttack();
            capsuleCollider.enabled = true;
            Debug.Log("ontriggerenter");
        } 
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerNear = false;
        Debug.Log("ontriggerexit");
    }






}
