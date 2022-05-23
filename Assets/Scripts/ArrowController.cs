using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private GameObject[] arrowPool;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int poolSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        arrowPool = new GameObject[poolSize];

        for(int i = 0; i < poolSize; i++)
        {
            arrowPool[i] = Instantiate(arrowPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
