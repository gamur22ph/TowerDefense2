using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Wave testWave;

    private Wave currentWave;

    public float spawnTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        currentWave = Instantiate(testWave);
        for (int i = 0; i < currentWave.waveEnemies[0].count; i++) ObjectPool.Instance.AddObject(enemyPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            GameObject enemy = ObjectPool.Instance.GetObject(enemyPrefab, PathManager.instance.GetFirstPoint(), Quaternion.identity);
            enemy.GetComponent<Enemy>().Initialize();
            spawnTimer = currentWave.spawnInterval;
        }
    }
}
