using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFactory : MonoBehaviour
{
    [SerializeField] private GameObject CoinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProduceCoin(Vector2 pos)
    {
        GameObject coin = Instantiate(CoinPrefab, transform);
        coin.transform.position = pos;
    }
}
