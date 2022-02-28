using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerHandler : MonoBehaviour
{
    public GameObject tower;
    public Dictionary<Vector3, GameObject> towers = new Dictionary<Vector3, GameObject>();

    public SO_StartingTowers starterTowers;
    public GameObject towerPanel;
    public GameObject towerUIPrefab;

    public Tilemap tilemap;
    public TileBase towerBase;

    // Start is called before the first frame update
    void Start()
    {
        foreach (SO_Tower tower in starterTowers.towers)
        {
            GameObject towerUI = Instantiate(towerUIPrefab);
            towerUI.transform.SetParent(towerPanel.transform);
            towerUI.GetComponent<TowerUI>().Setup(tower);
            towerUI.GetComponent<TowerUI>().tilemap = tilemap;
            towerUI.GetComponent<TowerUI>().towerBase = towerBase;
        }
    }


    public void CreateTower(Vector3 position, SO_Tower towerStats)
    {

        if (!towers.ContainsKey(position))
        {
            if (GameManager.playerMoney >= towerStats.cost)
            {
                GameManager.removeMoney(towerStats.cost);

                GameObject towerObject = Instantiate(tower);
                position.z = 0;
                position.x += 0.5f;
                position.y += 0.5f;
                towers.Add(position, towerObject);
                towerObject.transform.position = position;
                switch (towerStats)
                {
                    case (SO_TurretTower):
                        towerObject.AddComponent<TurretTower>();
                        towerObject.GetComponent<TurretTower>().Setup(towerStats);
                        break;
                    case (SO_AoETower):
                        towerObject.AddComponent<AoETower>();
                        towerObject.GetComponent<AoETower>().Setup(towerStats);
                        break;
                    case (SO_DebuffTower):
                        towerObject.AddComponent<DebuffTower>();
                        towerObject.GetComponent<DebuffTower>().Setup(towerStats);
                        break;
                    default:
                        Debug.LogWarning("Attack variant not implemented");
                        break;
                }

            }
        }
        else
        {
            Debug.Log("Trying to place tower on tile with already a tower on it.");
        }
    }

}
