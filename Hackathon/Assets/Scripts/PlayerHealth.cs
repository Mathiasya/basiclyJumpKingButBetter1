using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private float maxHealth = 100;
    [SerializeField] private float currentHealth;
   private float damageAmount = 20;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getDamage() {
        

        currentHealth -= damageAmount;

    }

    

    public void OnCollisionEnter2D(Collision2D collision)
    {
        getDamage();
        Debug.Log("collision");
    }



}
