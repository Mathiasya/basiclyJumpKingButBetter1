using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
   
    [SerializeField]
    private Image HealthBarImage;

    void Start()
    {
       PlayerHealth.instance.EventHealthChanged += OnHealthChanged;
    }

   


    public void OnHealthChanged() {
        
        Debug.Log("onhealt");

        float health = PlayerHealth.instance.currentHealth;
        float maxHealth = PlayerHealth.instance.maxHealth;

        HealthBarImage.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1f);
    }
}
