using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public List<SO_Wave> waves = new List<SO_Wave>();
    public int buildTimer;
    public float waveTimer;
    public float enemyInterval;
    private float timerPerEnemyTimer;
    private int currentWave = -1;
    private bool isWaving = false;
    private float currentTimer;
    private float maxPercentage;
    private Dictionary<SO_EnemyBase, float> sortedEnemies = new Dictionary<SO_EnemyBase, float>();

    public GameObject enemyPrefab;
    public WaypointCreator waypointCreator;

    public UIManager uiManager;

    public void Start()
    {
        currentTimer = buildTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaving)
        {
            DoWave();
        }
        else
        {
            DoBuild();
        }
        uiManager.updateWaveTimerText(isWaving, currentTimer);
    }

    private float enemyTimer;

    public void DoWave()
    {
        if(currentTimer > 0)
        {
            if(enemyTimer > 0)
            {
                enemyTimer -= Time.deltaTime;
            }
            else
            {
                spawnEnemy();
                enemyTimer = enemyInterval;
            }
            currentTimer -= Time.deltaTime;
        }
        else
        {
            currentTimer = buildTimer;
            isWaving = false;
        }
    }

    public void spawnEnemy()
    {
        float randomValue = Random.Range(0, maxPercentage);
        foreach(KeyValuePair<SO_EnemyBase, float> enemy in sortedEnemies)
        {
            if(randomValue < enemy.Value)
            {
                CreateEnemy(enemy.Key);
                return;
            }
        }
    }

    public void CreateEnemy(SO_EnemyBase enemyBase)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.GetComponent<EnemyBehaviour>().waypoints = waypointCreator.wayPoints;
        enemy.GetComponent<EnemyBehaviour>().Setup(enemyBase);
    }

    public void DoBuild()
    {
        if(currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
        else
        {
            nextWaveSetup();
            currentTimer = waveTimer;
            isWaving = true;
        }
    }

    public void nextWaveSetup()
    {
        if(currentWave == waves.Count - 1)
        {
            currentWave = 0;
        }
        else
        {
            currentWave++;
        }
        SetupWave();
    }

    public void SetupWave()
    {
        maxPercentage = 0;
        sortedEnemies.Clear();
        foreach(EnemyPercentage enemy in waves[currentWave].enemies)
        {
            maxPercentage += enemy.percentage;
            sortedEnemies.Add(enemy.enemy, maxPercentage);
        }
        waveTimer = waves[currentWave].waveTimer;
        enemyInterval = waves[currentWave].enemyInterval;
        uiManager.updateWaveNumberText(currentWave);
    }

}
