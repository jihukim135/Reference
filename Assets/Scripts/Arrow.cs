using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Vector2Int gravity;
    [SerializeField] private int ground;
    public Vector2Int velocity;

    private FixedPosTransform fixedTransform;

    private void Awake()
    {
        fixedTransform = GetComponent<FixedPosTransform>();
    }

    void FixedUpdate()
    {
		velocity += gravity;
		fixedTransform.position += velocity;

        if (fixedTransform.position.y < ground)
            ArrowPool.Instance.Discard(gameObject);
    }

	public void Initialize(Vector2Int pos, Vector2Int vel)
	{
		fixedTransform.position = pos;
		velocity = vel;
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.CompareTag("Player"))
        //{
            //ArrowPool.Instance.Discard(gameObject);
        //}
    }
}
