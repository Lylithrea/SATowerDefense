using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretTower : TowerBase
{

    private SpriteRenderer turretBarrel;
    private GameObject bullet;

    public string getName()
    {
        return "Turret";
    }

    public override void Update()
    {
        base.Update();
        if(enemies.Count != 0)
        {
            turretBarrel.transform.right = enemies[0].transform.position - this.transform.position;
        }
    }



    protected override void Attack()
    {
        GameObject bul = Instantiate(bullet);
        bul.transform.SetParent(this.gameObject.transform.parent);
        bul.GetComponentInChildren<Bullet>().damage = damage;
        bul.GetComponentInChildren<Bullet>().enemy = enemies[0].GetComponent<EnemyBehaviour>();
        bul.transform.position = this.gameObject.transform.position + turretBarrel.gameObject.transform.right * 0.5f;
    }

    public override void Setup(SO_Tower tower)
    {
        base.Setup(tower);
        SO_TurretTower turretTower = tower as SO_TurretTower;
        turretBarrel = gameObject.transform.Find("Turret Barrel Sprite").GetComponent<SpriteRenderer>();
        turretBarrel.gameObject.SetActive(true);
        turretBarrel.sprite = turretTower.barrel;
        bullet = turretTower.bullet;
    }



}


