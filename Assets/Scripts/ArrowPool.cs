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

    [SerializeField] private float timeSpan;
    private float _timer = 0f;

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

    void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        if (_timer > timeSpan)
        {
            GameObject arrow = _pool.Dequeue();

            int posX = Random.Range(-6, 7);
            arrow.transform.position = new Vector3(posX, 7f, 0f);
            arrow.SetActive(true);

            _timer = 0f;
        }
    }
}