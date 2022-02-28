using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Upgrade")]
public class SO_TowerUpgrades : ScriptableObject
{
    public string cardName;
    public int cost;
    public int damage;
    public int radius;
    public Sprite icon;
    [Range(0f, 2f)]
    public float attackSpeed;
    public Sprite model;
}
