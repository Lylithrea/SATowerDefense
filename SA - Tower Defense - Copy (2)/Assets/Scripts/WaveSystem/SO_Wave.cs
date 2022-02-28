using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave")]
public class SO_Wave : ScriptableObject
{
    public List<EnemyPercentage> enemies = new List<EnemyPercentage>();
    public float waveTimer;
    public float enemyInterval;
}

[System.Serializable]
public class EnemyPercentage
{
    public SO_EnemyBase enemy;
    public float percentage;
}


