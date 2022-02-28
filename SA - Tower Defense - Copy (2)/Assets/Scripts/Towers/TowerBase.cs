using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*public interface ITower
{

    void addEnemy(GameObject enemy);
    void removeEnemy(GameObject enemy);
    void setup(SO_Tower soTower);
    string getName();
    void attack();
    void upgrade();
    void setUpgrades(int upgradeChain, List<TowerUpgrades> upgrades);
    List<TowerUpgrades> getUpgrades(int upgradeChain, int tier = 99);
}*/



public abstract class TowerBase : MonoBehaviour
{
    /*    protected abstract ITower getName();

        public ITower thisName()
        {
            return this.getName();
        }*/

    protected SpriteRenderer turretBase;
    public List<GameObject> enemies = new List<GameObject>();
    public List<upgradeChain> upgradeChains = new List<upgradeChain>();

    public SpriteRenderer upgradeIcon;

    private float currentRadius;

    public int damage;

    protected float attackSpeed;
    private float currentTimer = 0;

    public float towerWorth;


    //in the scriptable object "SO_Tower" when declaring upgrade chains, the size cant be bigger than 4 different chains
    //start at -1 because there are no upgrades yet
    public int currentUpgradeTier1 = - 1;
    public int currentUpgradeTier2 = -1 ;
    public int currentUpgradeTier3 = -1;
    public int currentUpgradeTier4 = -1;

    public virtual void Update()
    {
        if (enemies.Count != 0)
        {
            if (currentTimer >= attackSpeed)
            {
                currentTimer = 0;
                Attack();
            }
            else
            {
                currentTimer += Time.deltaTime;
                return;
            }
        }
        else
        {
            return;
        }
    }

    protected virtual void Attack()
    {
        
    }

    protected void checkAvailableUpgrade()
    {
        int i = 0;
        //check each upgrade chain
        foreach(upgradeChain upgrade in upgradeChains)
        {
            //with help of an switch statement we know which one we are currently checking
            switch (i)
            {
                case 0:
                    if (hasEnoughMoneyForUpgrade(ref currentUpgradeTier1, upgrade)) return;
                    break;
                case 1:
                    if (hasEnoughMoneyForUpgrade(ref currentUpgradeTier2, upgrade)) return;
                    break;
                case 2:
                    if (hasEnoughMoneyForUpgrade(ref currentUpgradeTier3, upgrade)) return;
                    break;
                case 3:
                    if (hasEnoughMoneyForUpgrade(ref currentUpgradeTier4, upgrade)) return;
                    break;
                default:
                    Debug.Log("Input is higher than 4, while there are no more than 4 different chains");
                    break;
            }
            i++;
        }
        upgradeIcon.gameObject.SetActive(false);

    }

    public bool hasEnoughMoneyForUpgrade(ref int tier, upgradeChain upgrade)
    {
        //check if we are already max tier
        if (tier == upgrade.upgrades.Count - 1)
        {
            return false;
        }
        if (upgrade.upgrades[tier + 1].cost <= GameManager.playerMoney)
        {
            upgradeIcon.gameObject.SetActive(true);
            return true;
        }
        return false;
    }


    public virtual void Setup(SO_Tower tower)
    {
        upgradeIcon = gameObject.transform.Find("Upgrade Icon").GetComponent<SpriteRenderer>();
        GameManager.onMoneyUpdate += checkAvailableUpgrade;
        turretBase = gameObject.transform.Find("Turret Base Sprite").GetComponent<SpriteRenderer>();
        turretBase.sprite = tower.model;
        setUpgrades(tower.upgradeChains);
        damage = tower.damage;
        turretBase.gameObject.SetActive(true);
        attackSpeed = tower.attackSpeed;
        setRadius(tower.radius);
        checkAvailableUpgrade();
        towerWorth = tower.cost / 2;
    }

    public void sellTower()
    {
        GameManager.onMoneyUpdate -= checkAvailableUpgrade;
        GameManager.addMoney(towerWorth);
        Destroy(this.gameObject);
    }

    public void Upgrade(int chain)
    {
        switch (chain)
        {
            case 0:
                doUpgrade(chain, ref currentUpgradeTier1);
                break;
            case 1:
                doUpgrade(chain, ref currentUpgradeTier2);
                break;
            case 2:
                doUpgrade(chain, ref currentUpgradeTier3);
                break;
            case 3:
                doUpgrade(chain, ref currentUpgradeTier4);
                break;
            default:
                Debug.Log("Input is higher than 4, while there are no more than 4 different chains");
                break;
        }
    }

    private void doUpgrade(int chain, ref int tier)
    {
        if (tier < upgradeChains[chain].upgrades.Count - 1)
        {
            tier++;
            statChange(chain, ref tier);
        }
        else
        {
            Debug.Log("maximum tier");
        }

    }

    public void statChange(int chain ,ref int tier)
    {
        attackSpeed -= upgradeChains[chain].upgrades[tier].attackSpeed;
        damage += upgradeChains[chain].upgrades[tier].damage;
        increaseRadius(upgradeChains[chain].upgrades[tier].radius);
        towerWorth += upgradeChains[chain].upgrades[tier].cost / 2;
    }


    public List<SO_TowerUpgrades> getUpgrades(int upgradeChain)
    {
        return upgradeChains[upgradeChain].upgrades;
    }

    public List<upgradeChain> getUpgradeChains()
    {
        return upgradeChains;
    }

    public SO_TowerUpgrades getUpgrades(int upgradeChain, int tier)
    {
        return upgradeChains[upgradeChain].upgrades[tier];
    }

    public void setUpgrades(int upgradeChain, List<SO_TowerUpgrades> upgrade)
    {
        upgradeChains[upgradeChain].upgrades = upgrade;
    }

    public void setUpgrades(upgradeChain chain)
    {
        upgradeChains.Add(chain);
    }

    public void setUpgrades(upgradeChain[] chains)
    {
        foreach(upgradeChain chain in chains)
        {
            upgradeChains.Add(chain);
        }
    }

    public void addEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void removeEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public void setVisualRadius(bool isEnabled)
    {
        gameObject.GetComponentInChildren<CollisionCheck>().setVisualRadius(isEnabled);
    }


    public void setRadius(float radius)
    {
        currentRadius = radius;
        gameObject.GetComponentInChildren<CollisionCheck>().Setup(currentRadius);
    }

    public void increaseRadius(float amount)
    {
        currentRadius += amount;
        gameObject.GetComponentInChildren<CollisionCheck>().Setup(currentRadius);
    }

}
