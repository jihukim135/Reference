using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private GameObject hpBar;
    [SerializeField] private Image hpFillImage;
    private float _decreaseAmount;

    [SerializeField] private int maxHp;
    private int _currentHp;

    private bool _isDead = false;
    private SpriteRenderer _renderer;

    void Start()
    {
        _decreaseAmount = 1f / maxHp;
        _currentHp = maxHp;
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // 아직 발사 화살이 없어서 떨어지는 화살에 태그 달아놓음
        if (!_isDead && col.CompareTag("Arrow"))
        {
            DecreaseHp();
        }
    }

    private void DecreaseHp()
    {
        if (_currentHp <= 0)
        {
            Die();
            return;
        }
        
        hpFillImage.fillAmount -= _decreaseAmount;
        _currentHp--;
    }

    private void Die()
    {
        Debug.Log("Die");
        _isDead = true;
        hpBar.SetActive(false);

        StartCoroutine(FadeOut(3f));
    }

    private IEnumerator FadeOut(float t)
    {
        Color color = Color.white;

        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / t;
            _renderer.color = color;

            yield return null;
        }
        
        gameObject.SetActive(false);
    }
}
