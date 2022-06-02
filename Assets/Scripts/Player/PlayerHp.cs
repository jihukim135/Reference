using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private GameObject hpBar;
    [SerializeField] private Image hpFillImage;
    private float _decreaseAmount;

    [SerializeField] private int maxHp;
    private int _currentHp;

    public bool IsDead { get; private set; }

    private SpriteRenderer _renderer;
    [SerializeField] private float fadeOutDuration;

    void Start()
    {
        _decreaseAmount = 1f / maxHp;
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

        hpFillImage.fillAmount -= damage * _decreaseAmount;
        _currentHp--;
    }

    private void Die()
    {
        Debug.Log("Die");
        IsDead = true;
        hpBar.SetActive(false);

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