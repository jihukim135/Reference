using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : PlayerController
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			NetworkHandler.instance.SendDatagram(2, fixedTransform.position, FixedPosTransform.MousePosition());
        }
        else if (Input.GetMouseButtonDown(1))
        {
            NetworkHandler.instance.SendDatagram(3, fixedTransform.position, FixedPosTransform.MousePosition() - fixedTransform.position);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && fixedTransform.position.y <= ground)
        {
            NetworkHandler.instance.SendDatagram(4, fixedTransform.position, Vector2Int.down);
        }
    }
}
