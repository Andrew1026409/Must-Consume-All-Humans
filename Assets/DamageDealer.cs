using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 1; // Amount of damage to deal

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an enemy
        if (other.CompareTag("Enemy"))
        {
            // Get the EnemyController component of the enemy
            EnemyController enemyController = other.GetComponent<EnemyController>();

            // Apply damage to the enemy if it has an EnemyController component
            if (enemyController != null)
            {
                enemyController.TakeDamage(damageAmount);
            }
        }
    }
}
