using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMaterial;
    [SerializeField] private float restoreDefaultMatTime = 0.2f;
    
    private Material _defaultMat;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMat = _spriteRenderer.material;
    }
    
    public float GetRestoreDefaultMatTime()
    {
        return restoreDefaultMatTime;
    }

    public IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = whiteFlashMaterial;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        _spriteRenderer.material = _defaultMat;
    }
}
