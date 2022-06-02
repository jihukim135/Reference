using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private GameObject hpBar;
    [SerializeField] private Image hpFillImage;
    private float _fillUnitAmount;

    [SerializeField] private int maxHp;
    private int _currentHp;

    public bool IsDead { get; private set; }

    private SpriteRenderer _renderer;
    [SerializeField] private float fadeOutDuration;

    public PlayerHp WhoAttackedMe { get; set; }

    void Start()
    {
        _fillUnitAmount = 1f / maxHp;
        _currentHp = maxHp;
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void DecreaseHp(int damage)
    {
        if (!IsDead && _currentHp <= 0)
        {
            Die();
            return;
        }

        hpFillImage.fillAmount -= damage * _fillUnitAmount;
        _currentHp -= damage;
    }

    private void IncreaseHp(int amount)
    {
        if (_currentHp >= maxHp)
        {
            return;
        }

        hpFillImage.fillAmount += amount * _fillUnitAmount;
        _currentHp += amount;
    }

    private void Die()
    {
        IsDead = true;
        hpBar.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        WhoAttackedMe.IncreaseHp(1);

        StartCoroutine(FadeOut());
        Destroy(gameObject, fadeOutDuration);
    }

    private IEnumerator FadeOut()
    {
        Color color = Color.white;

        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / fadeOutDuration;
            _renderer.color = color;

            yield return null;
        }
    }
}