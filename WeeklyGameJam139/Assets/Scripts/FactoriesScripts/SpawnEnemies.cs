using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private float spawnRange = 5f;
    [SerializeField] private int spawnNum = 3;
    [SerializeField] private bool triggerSpawn = false;
    private DiContainer _diContainer;

    [Inject]
    public void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0;i<spawnNum;i++)
            SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > spawnNum && !triggerSpawn)
            triggerSpawn = true;

        if (triggerSpawn)
            StartCoroutine(SpawnNewEnemy());
    }

    IEnumerator SpawnNewEnemy()
    {
        Debug.Log("Trigger spawn new enemy");
        yield return new WaitForSeconds(5);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Vector2 pos = new Vector2(transform.position.x + Random.Range(-spawnRange, spawnRange), transform.position.y + Random.RandomRange(-spawnRange, spawnRange));
        Quaternion rot = new Quaternion(0, 0, 0, 0);
        Object enemyPrefab = Resources.Load("Prefabs/Enemy");
        GameObject enemy = _diContainer.InstantiatePrefab(enemyPrefab, pos, rot, transform);
    }
}
