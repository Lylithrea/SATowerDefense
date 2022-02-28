using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage;
    public EnemyBehaviour enemy;
    public float bulletSpeed = 0.15f;


    // Update is called once per frame
    void Update()
    {
        if(enemy == null)
        {
            Destroy(this.gameObject);
            return;
        }

        if (Vector3.Distance(this.gameObject.transform.position, enemy.gameObject.transform.position) <= 0.1f)
        {
            enemy.reduceHealth(damage);
            Destroy(this.gameObject);
        }
        

        this.transform.up = enemy.transform.position - this.transform.position;
        Vector3 forward = this.transform.up;
        forward.z = 0;
        this.transform.position += forward * 0.05f;

    }
}
