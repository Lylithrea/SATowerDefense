using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private TowerBase tower;

    public void setVisualRadius(bool isEnabled)
    {
        gameObject.SetActive(isEnabled);
    }

    public void Setup(float radius)
    {
        gameObject.transform.localScale = new Vector3(radius, radius, radius);
    }

    private void Start()
    {
        tower = gameObject.transform.parent.GetComponentInChildren<TowerBase>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.transform.GetComponent<EnemyBehaviour>() != null)
        {
            tower.enemies.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.transform.GetComponent<EnemyBehaviour>() != null)
        {
            tower.enemies.Remove(collision.gameObject);
        }
    }

}
