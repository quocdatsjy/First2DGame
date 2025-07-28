using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollider;
    [FormerlySerializedAs("swordAttackCD")] [SerializeField] private float swordAttackCd = 0.5f; //
    
    private PlayerControls _playerControls;
    private Animator _myAnimator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    private bool _attackButtonDown, _isAttacking = false;
    
    private GameObject _slashAnim;
    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _playerControls = new PlayerControls();
        _myAnimator = GetComponent<Animator>();
    }
    
    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => StartAttacking();
        _playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }

    private void StartAttacking()
    {
        _attackButtonDown = true;
    }
    
    private void StopAttacking()
    {
        _attackButtonDown = false;
    }

    private void Attack()
    {
        if(_attackButtonDown && !_isAttacking)
        {
            _isAttacking = true;
            _myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            _slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            _slashAnim.transform.SetParent(transform.parent);
            StartCoroutine(AttackCdRoutine());
        }
    }
    
    private IEnumerator AttackCdRoutine()
    {
        yield return new WaitForSeconds(swordAttackCd);
        _isAttacking = false;
    }
    
    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (_playerController.FacingLeft)
        {
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    public void SwingDownFlipAnimEvent()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (_playerController.FacingLeft)
        {
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(_playerController.transform.position);
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
			weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
