using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ArrowPool : MonoBehaviour
{
    private Queue<GameObject> _pool;
    public Queue<GameObject> Pool => _pool;
    [SerializeField] private int poolSize = 0;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int speed = 0;

    #region singleton

    private static ArrowPool _instance;

    public static ArrowPool Instance
    {
        get
        {
            Init();
            return _instance;
        }
    }

    private static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.FindWithTag("ArrowPool");
            if (go == null)
            {
                Debug.Log("ArrowPool not found");
                return;
            }

            _instance = go.GetComponent<ArrowPool>();
        }
    }

    #endregion

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