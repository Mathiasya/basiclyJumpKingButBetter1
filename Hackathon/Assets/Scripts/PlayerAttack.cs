using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float shootingDistance;
    [SerializeField] private LineRenderer linerenderer;

    private Rigidbody2D playerBody;
    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        position = playerBody.transform.position;
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        Vector2 shootingDirection = playerBody.transform.right;

        RaycastHit2D hit = Physics2D.Raycast(position, shootingDirection, shootingDistance, LayerMask.GetMask("Enemy"));
        if (hit)
        {
            PlayerHealth enemyHealth = hit.transform.GetComponent<PlayerHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.GetDamage(10);
            }

            linerenderer.SetPosition(0, position);
            linerenderer.SetPosition(1, hit.point);
        }
        else
        {
            linerenderer.SetPosition(0, position);
            linerenderer.SetPosition(1, position + shootingDirection * shootingDistance);
        }

        linerenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        linerenderer.enabled = false;

    }
}
