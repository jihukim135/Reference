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
    public bool IsDead => _isDead;
    private SpriteRenderer _renderer;

    void Start()
    {
        _decreaseAmount = 1f / maxHp;
        _currentHp = maxHp;
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void DecreaseHp(int damage)
    {
        if (!_isDead && _currentHp <= 0)
        {
            Die();
            return;
        }
        
        hpFillImage.fillAmount -= damage * _decreaseAmount;
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
        
        Destroy(gameObject);
    }
}
