using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public WaypointCreator waypointCreator;
    public GameObject enemyPrefab;
    private List<GameObject> enemies = new List<GameObject>();
    public List<SO_EnemyBase> availableEnemies = new List<SO_EnemyBase>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreateEnemy(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.GetComponent<EnemyBehaviour>().waypoints = waypointCreator.wayPoints;
            enemy.GetComponent<EnemyBehaviour>().Setup(availableEnemies[Random.Range(0, availableEnemies.Count)]);
            enemies.Add(enemy);
        }
    }

    public void DestroyAllEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
    }

}
