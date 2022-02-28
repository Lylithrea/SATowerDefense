using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffTower : TowerBase
{

    public Debuff debuff;
    bool isAoE = false;
    public float duration;
    public float effectiveness;


    protected override void Attack()
    {
        base.Attack();
        switch (debuff)
        {
            case Debuff.Slow:
                doSlowEffect();
                break;
            default:
                Debug.LogWarning("Debuff not implemented yet");
                break;
        }
    }

    public override void Setup(SO_Tower tower)
    {
        base.Setup(tower);
        SO_DebuffTower debuffTower = tower as SO_DebuffTower;
        debuff = debuffTower.debuff;
        if(debuffTower.target == Target.AoE)
        {
            isAoE = true;
        }
        duration = debuffTower.duration;
        effectiveness = debuffTower.effectiveness;
    }


    private void doSlowEffect()
    {
        if (isAoE)
        {
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyBehaviour>().SetSlow(duration, effectiveness);
            }
        }
        else
        {
            //still need to implement
        }
    }

}
