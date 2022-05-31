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
	
	public GameObject Pop(Vector3 pos, Vector3 direction)
	{
		GameObject arrow = _pool.Dequeue();

        arrow.transform.position = pos;
    	arrow.SetActive(true);

		// Todo arrow velocity

		return arrow;
	}

	public void Discard(GameObject instance)
	{
		instance.SetActive(false);
		_pool.Enqueue(instance);
	}
}