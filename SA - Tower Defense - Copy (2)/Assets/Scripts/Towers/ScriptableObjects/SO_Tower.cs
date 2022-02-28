using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_Tower : ScriptableObject
{
    public int cost;
    public int damage;
    public int radius;
    [Range(0f,2f)]
    public float attackSpeed;
    public Sprite icon;
    public Sprite model;
    public upgradeChain[] upgradeChains;

    public void OnValidate()
    {
        if(upgradeChains.Length > 4)
        {
            Array.Resize(ref upgradeChains, 4);
            Debug.LogWarning("Only 4 chains are allowed.");
        }
       
    }

}

[System.Serializable]
public class upgradeChain
{
    public List<SO_TowerUpgrades> upgrades;
}

