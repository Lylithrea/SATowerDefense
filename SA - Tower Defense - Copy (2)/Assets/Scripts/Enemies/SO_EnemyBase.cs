using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Enemy")]
public class SO_EnemyBase : ScriptableObject
{
    public Sprite sprite;
    public int health;
    public float movementSpeed;
    public int money;
}
