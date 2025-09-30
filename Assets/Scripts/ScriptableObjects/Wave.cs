using UnityEngine;

[CreateAssetMenu(menuName = "Wave")]
public class Wave : ScriptableObject
{
    public WaveEnemy[] waveEnemies;
    public float spawnInterval;
}

[System.Serializable]
public struct WaveEnemy
{
    public EnemyType enemyType;
    public int count;
}
