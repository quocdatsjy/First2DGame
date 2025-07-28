using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D _rb;
    private Vector2 _moveDir;
    private Knockback _knockback;

    private void Awake()
    {
        _knockback = GetComponent<Knockback>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_knockback.GettingKnockedBack)
        {
            // If the enemy is getting knocked back, do not move
            return;
        }
        _rb.MovePosition(_rb.position + _moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDir = targetPosition;
    }
}
