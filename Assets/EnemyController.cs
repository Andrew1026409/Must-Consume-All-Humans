using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed of movement
    public int maxHealth = 10; // Maximum health points
    public float runAwayDistance = 5f; // Distance at which the enemy starts running away
    public AudioClip damageSound; // Sound to play when the enemy takes damage

    private int currentHealth; // Current health points
    private Transform player; // Reference to the player's transform
    private AudioSource audioSource; // Reference to the AudioSource component
    private Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player's transform
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void Update()
    {
        // Check if the player is within the run away distance
        if (Vector3.Distance(transform.position, player.position) < runAwayDistance)
        {
            // Calculate direction to run away from the player
            Vector3 directionToPlayer = transform.position - player.position;
            // Ignore the Y component
            directionToPlayer.y = 0f;

            // Move the enemy away from the player using Rigidbody
            rb.MovePosition(transform.position + directionToPlayer.normalized * moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        // Reduce health points by the damage amount
        currentHealth -= damage;

        // Play damage sound
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        // Check if the enemy has no health left
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Destroy the enemy GameObject when it dies
        Destroy(gameObject);
    }
}
