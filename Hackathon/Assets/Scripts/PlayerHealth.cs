using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public Action EventHealthChanged;
    public float maxHealth = 100;
    public float currentHealth;
    public float damageAmount = 20;
   
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        currentHealth = maxHealth;

       
    }

    private void Update()
    {
        if (currentHealth <= 0) {
            die();
        }
    }

    public void getDamage() {
        

        currentHealth -= damageAmount;

     
            EventHealthChanged();
        

    }



    public void die() {
        GameObject.Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        getDamage();
    }

}
