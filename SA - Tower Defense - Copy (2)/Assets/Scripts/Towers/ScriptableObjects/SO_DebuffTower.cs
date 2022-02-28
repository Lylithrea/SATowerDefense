using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Debuff Tower")]
public class SO_DebuffTower : SO_Tower
{
    public Debuff debuff;
    public Target target;
    public float duration;
    public float effectiveness;
}

public enum Debuff
{
    Slow,
    Poison
}

public enum Target
{
    Single,
    AoE
}
