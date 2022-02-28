using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SellTower : MonoBehaviour, IPointerClickHandler
{

    private TowerBase tower;
    private TurretUpgradeManager turretUpgradeManager;
    public Text sellText;

    public void OnPointerClick(PointerEventData eventData)
    {
        turretUpgradeManager.SellTower(tower);
    }

    public void Setup(TowerBase towerBase, TurretUpgradeManager turretUpgrade)
    {
        tower = towerBase;
        turretUpgradeManager = turretUpgrade;
        GameManager.onMoneyUpdate += textUpdate;
        textUpdate();
    }

    private void textUpdate()
    {
        int worth = Mathf.RoundToInt(tower.towerWorth);
        sellText.text = "Sell for: $" + worth.ToString();
    }

}
