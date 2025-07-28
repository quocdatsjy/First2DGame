using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    
    private int _currentHealth;
    private Knockback _knockback;
    private Flash _flash;

    private void Awake()
    {
        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        _currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _knockback.ApplyKnockback(PlayerController.Instance.transform, 15f); // Assuming PlayerController is a singleton
        StartCoroutine(_flash.FlashRoutine());
    }

    public void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Die();
        }
    }

    private void Die()
    {
        // Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}
