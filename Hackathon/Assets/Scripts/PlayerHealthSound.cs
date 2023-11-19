using UnityEngine;
using System;



public class PlayerHealthSound : MonoBehaviour
{
    public static PlayerHealthSound instance;
    public Action EventHealthChanged;
    public float maxHealth = 100;
    public float currentHealth;

    [SerializeField] private AudioSource deathSound;

    private Rigidbody2D playerBody;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        currentHealth = maxHealth;
        playerBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (currentHealth <= 0) {
            Die();
        }
        if (playerBody.velocity.y < -100f )
        {
            Debug.Log("Death by out of world");
            Die();
        }
    }

    public void GetDamage(float damageAmount) {
        currentHealth -= damageAmount;
        EventHealthChanged();
    }

    public void Die() {
        deathSound.Play();
        GameObject.Destroy(gameObject);
    }
}
