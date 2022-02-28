using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoETower : TowerBase
{
    protected override void Attack()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyBehaviour>().reduceHealth(damage);
        }
    }



    
}
