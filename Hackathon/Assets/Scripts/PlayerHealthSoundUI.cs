using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSoundUI : MonoBehaviour
{
   
    [SerializeField]
    private Image HealthBarImage;

    void Start()
    {
       PlayerHealthSound.instance.EventHealthChanged += OnHealthChanged;
    }

   


    public void OnHealthChanged() {
        
        Debug.Log("onhealt");

        float health = PlayerHealthSound.instance.currentHealth;
        float maxHealth = PlayerHealthSound.instance.maxHealth;

        HealthBarImage.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1f);
    }
}
