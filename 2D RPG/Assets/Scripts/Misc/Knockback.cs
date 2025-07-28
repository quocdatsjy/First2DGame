using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool GettingKnockedBack { get; private set; } = false;
    [SerializeField] private float knockbackTime = .2f;
    
    private Rigidbody2D _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    public void ApplyKnockback(Transform damageSource, float knockbackThrust)
    {
        if (GettingKnockedBack) return;

        GettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockbackThrust * _rb.mass;
        _rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockbackCoroutine());
    }
    
    private IEnumerator KnockbackCoroutine()
    {
        yield return new WaitForSeconds(knockbackTime);
        _rb.velocity = Vector2.zero;
        GettingKnockedBack = false;
    }
        
}
