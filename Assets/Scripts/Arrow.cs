using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    private ArrowPool _arrowPool;

    void Start()
    {
        _arrowPool = ArrowPool.Instance;
    }

    void FixedUpdate()
    {
        transform.Translate(0f, -speed, 0f);

        if (transform.position.y < -5.0f)
        {
            _arrowPool.Pool.Enqueue(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        _arrowPool.Pool.Enqueue(gameObject);
        gameObject.SetActive(false);
    }
}
