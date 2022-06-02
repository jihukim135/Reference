using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ArrowPool : Singleton<ArrowPool>
{
    private Queue<GameObject> _pool;
    [SerializeField] private int poolSize = 0;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int speed = 0;

    void Start()
    {
        _pool = new Queue<GameObject>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform, true);
            _pool.Enqueue(arrow);
            arrow.SetActive(false);
        }
    }
	
	public GameObject Pop(Vector2Int pos, Vector2Int dir)
	{
		int mag = Mathf.FloorToInt(Mathf.Sqrt(dir.x * dir.x + dir.y * dir.y));
		dir *= speed;
		dir /= mag;

		GameObject arrow = _pool.Dequeue();
		
		arrow.GetComponent<Arrow>().Initialize(pos, dir);
    	arrow.SetActive(true);

		return arrow;
	}

	public void Discard(GameObject instance)
	{
		instance.SetActive(false);
		_pool.Enqueue(instance);
	}
}