using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int minDamage = 6;
    public int maxDamage = 16;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            int damage = Random.Range(minDamage, maxDamage);
            Health enemyHealth = null;
            EnemyController enemyController = null;

            if (collision.TryGetComponent<Health>(out enemyHealth))
            {
                enemyHealth.TakeDam(damage);
            }

            if (collision.TryGetComponent<EnemyController>(out enemyController))
            {
                enemyController.TakeDamEffect(damage);
            }

           

            Destroy(gameObject);
        }
    }

   
}
