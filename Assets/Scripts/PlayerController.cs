using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMoving = false;
    private int destPosition = 0;
	private int yVelocity;
    
    [SerializeField] private int speed = 10;
    [SerializeField] private int gravity = 10;
    [SerializeField] private int jumpPower = 10;
	[SerializeField] private int ground = 0;

    private FixedPosTransform fixedTransform;

    private void Awake()
    {
        fixedTransform = GetComponent<FixedPosTransform>();
    }

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			Move(fixedTransform.position, FixedPosTransform.MousePosition());
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Shoot(fixedTransform.position, FixedPosTransform.MousePosition() - fixedTransform.position);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && fixedTransform.position.y <= ground)
        {
            Jump(fixedTransform.position, Vector2Int.down);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            if (destPosition < fixedTransform.position.x - speed)
                fixedTransform.position += Vector2Int.left * speed;
            else if (destPosition > fixedTransform.position.x + speed)
                fixedTransform.position += Vector2Int.right * speed;
            else
			{
				fixedTransform.position = new Vector2Int(destPosition, fixedTransform.position.y);
                isMoving = false;
			}
        }

		if (fixedTransform.position.y + yVelocity - gravity <= ground)
		{
			yVelocity = 0;
			fixedTransform.position = new Vector2Int(fixedTransform.position.x, ground);
		}
		else
		{
			yVelocity -= gravity;
			fixedTransform.position += Vector2Int.up * yVelocity;
		}
    }

    public void Move(Vector2Int initPosition, Vector2Int destination)
    {
        isMoving = true;
        fixedTransform.position = initPosition;
        destPosition = destination.x;
    }

    public void Shoot(Vector2Int initPosition, Vector2Int destination)
    {
        fixedTransform.position = initPosition;

		ArrowPool.Instance.Pop((Vector3)(Vector2)initPosition, (Vector3)(Vector2)destination);
        //destPosition = destination;
    }

    public void Jump(Vector2Int initPosition, Vector2Int destination)
    {
		//todo
		yVelocity = jumpPower;
    }

}
