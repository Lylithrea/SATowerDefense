using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeUI : MonoBehaviour, IPointerClickHandler
{

    public GameObject tower;
    public upgradeChain towerUpgrade;
    public int upgradeChainInt;
    public Image icon;
    public Image background;

    private TowerBase towerBase;

    public Text cardName;
    public Text costText;
    private int cost;
    public TurretUpgradeManager turretUpgradeManager;


    public void OnPointerClick(PointerEventData eventData)
    {
        CheckMoney();
    }


    public void CheckMoney()
    {
        if(cost <= GameManager.playerMoney)
        {
            tower.GetComponent<TowerBase>().Upgrade(upgradeChainInt);
            GameManager.removeMoney(cost);
            UpdateUI();
        }
    }

    public void checkAlpha()
    {
        if (GameManager.playerMoney >= cost)
        {
            setAlphaOfImages(1);
        }
        else
        {
            setAlphaOfImages(0.5f);
        }
    }

    private void setAlphaOfImages(float alpha)
    {
        Color iconColor = icon.color;
        iconColor.a = alpha;
        icon.color = iconColor;
        Color backgroundColor = background.color;
        backgroundColor.a = alpha;
        background.color = backgroundColor;
    }


    public void UpdateUI()
        {
            switch (upgradeChainInt)
            {
                case 0:
                    checkUpgradeChain(ref towerBase.currentUpgradeTier1);
                    break;
                case 1:
                    checkUpgradeChain(ref towerBase.currentUpgradeTier2);
                    break;
                case 2:
                    checkUpgradeChain(ref towerBase.currentUpgradeTier3);
                    break;
                case 3:
                    checkUpgradeChain(ref towerBase.currentUpgradeTier4);
                    break;
                default:
                    break;
            }
        }

    public void checkUpgradeChain(ref int tier)
    {
        if(tier == towerBase.upgradeChains[upgradeChainInt].upgrades.Count - 1)
        {
            GameManager.onMoneyUpdate -= checkAlpha;
            turretUpgradeManager.currentUpgrades.Remove(this.gameObject);
            Destroy(this.gameObject);
            return;
        }

        updateCard(ref tier);
    }

    public void updateCard(ref int tier)
    {
        //this one looks at the next upgrade, thus + 1
        cardName.text = towerBase.upgradeChains[upgradeChainInt].upgrades[tier + 1].cardName;
        costText.text = "$" + towerBase.upgradeChains[upgradeChainInt].upgrades[tier + 1].cost.ToString();
        icon.sprite = towerBase.upgradeChains[upgradeChainInt].upgrades[tier + 1].icon;
        cost = towerBase.upgradeChains[upgradeChainInt].upgrades[tier + 1].cost;
        checkAlpha();
    }

    public void Remove()
    {
        GameManager.onMoneyUpdate -= checkAlpha;
        Destroy(this.gameObject);
    }

    public void Setup()
    {
        towerBase = tower.GetComponent<TowerBase>();
        UpdateUI();
        GameManager.onMoneyUpdate += checkAlpha;
        checkAlpha();
    }


}
