using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TurretUpgradeManager : MonoBehaviour
{

    public GameObject upgradeUIPrefab;
    public GameObject parent;

    public List<GameObject> currentUpgrades = new List<GameObject>();


    void Update()
    {
        if (!IsPointerOverUIElement())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector3.forward, out hit))
                {
                    if (hit.collider != null)
                    {
                        TowerBase tower = hit.collider.gameObject.GetComponent<TowerBase>();

                        if (tower != null)
                        {
                            List<upgradeChain> upgradeChains = tower.getUpgradeChains();
                            if (upgradeChains != null)
                            {
                                handleUpgrades(upgradeChains, tower.gameObject);
                            }
                        }
                    }
                }
                else
                {
                    parent.transform.parent.gameObject.SetActive(false);
                }
            }

        }
    }



    public void handleUpgrades(List<upgradeChain> upgradeChains, GameObject tower)
    {
        parent.transform.parent.gameObject.SetActive(true);
        clearUpgrades();
        int i = 0;
        foreach (upgradeChain upgrade in upgradeChains)
        {
            if(upgrade.upgrades ==null || upgrade.upgrades.Count == 0)
            {
                return;
            }
            GameObject upgradeUI = Instantiate(upgradeUIPrefab);
            upgradeUI.transform.SetParent(parent.transform);
            currentUpgrades.Add(upgradeUI);
            UpgradeUI ui = upgradeUI.GetComponent<UpgradeUI>();
            ui.turretUpgradeManager = this;
            ui.tower = tower;
            ui.towerUpgrade = upgrade;
            ui.upgradeChainInt = i;
            ui.Setup();
            i++;
        }

        parent.transform.parent.GetComponentInChildren<SellTower>().Setup(tower.GetComponentInChildren<TowerBase>(), this);
    }

    public void SellTower(TowerBase tower)
    {
        this.gameObject.GetComponent<TowerHandler>().towers.Remove(tower.gameObject.transform.position);
        tower.sellTower();
        parent.transform.parent.gameObject.SetActive(false);
    }



    public void clearUpgrades()
    {
        if (currentUpgrades == null || currentUpgrades.Count == 0)
        {
            return;
        }
        foreach(GameObject upgrade in currentUpgrades)
        {
            //cant destroy it, because it is connected to delegete, so need to remove it via this
            upgrade.GetComponent<UpgradeUI>().Remove();
        }
        currentUpgrades.Clear();
    }



    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }

    ///Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }



}
