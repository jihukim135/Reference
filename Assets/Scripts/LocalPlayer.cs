using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : PlayerController
{
    private PlayerHp _hp;
    private NetworkHandler _networkHandler;
    private bool _isDeadFlagSent = false;

    private void Start()
    {
        _hp = GetComponent<PlayerHp>();
        _networkHandler = NetworkHandler.instance;
    }

    void Update()
    {
        if (_hp.IsDead)
        {
            if (!_isDeadFlagSent)
            {
                _networkHandler.SendDatagram(5, fixedTransform.position, fixedTransform.position);
                _isDeadFlagSent = true;
            }

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _networkHandler.SendDatagram(2, fixedTransform.position, FixedPosTransform.MousePosition());
        }
        else if (Input.GetMouseButtonDown(1))
        {
            _networkHandler.SendDatagram(3, fixedTransform.position,
                FixedPosTransform.MousePosition() - fixedTransform.position);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && fixedTransform.position.y <= ground)
        {
            _networkHandler.SendDatagram(4, fixedTransform.position, Vector2Int.down);
        }
    }
}