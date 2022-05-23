using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMoving = false;
    private Vector2Int destPosition = new Vector2Int(0, 0);
    
    [SerializeField] private int speed = 10;

    private FixedPosTransform fixedTransform;

    private void Awake()
    {
        fixedTransform = GetComponent<FixedPosTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (destPosition.x < fixedTransform.position.x - speed)
                fixedTransform.position += Vector2Int.left * speed;
            else if (destPosition.x > fixedTransform.position.x + speed)
                fixedTransform.position += Vector2Int.right * speed;
            else
                isMoving = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Move(fixedTransform.position, FixedPosTransform.MousePosition());
        }
    }

    private void Move(Vector2Int initPosition, Vector2Int destination)
    {
        isMoving = true;
        fixedTransform.position = initPosition;
        destPosition = destination;
    }

}
