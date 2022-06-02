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
    public GameObject Shooter { get; set; }

    private void Awake()
    {
        fixedTransform = GetComponent<FixedPosTransform>();
    }

    void FixedUpdate()
    {
        velocity += gravity;
        fixedTransform.position += velocity;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(velocity.x, -velocity.y) * Mathf.Rad2Deg);

        if (fixedTransform.position.y < ground)
        {
            ArrowPool.Instance.Discard(gameObject);
        }
    }

    public void Initialize(Vector2Int pos, Vector2Int vel)
    {
        fixedTransform.position = pos;
        velocity = vel;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject go = col.gameObject;
        if (go == Shooter || !go.CompareTag("Player"))
        {
            return;
        }

        ArrowPool.Instance.Discard(gameObject);

        PlayerHp opponentHp = go.GetComponent<PlayerHp>();
        PlayerHp shooterHp = Shooter.GetComponent<PlayerHp>();

        opponentHp.WhoAttackedMe = shooterHp;
        opponentHp.DecreaseHp(1);
    }
}