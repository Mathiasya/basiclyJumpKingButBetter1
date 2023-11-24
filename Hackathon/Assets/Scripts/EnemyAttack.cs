using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == CompareTag("Player"))
        PlayerHealthSound.instance.GetDamage(20);
    }
}
