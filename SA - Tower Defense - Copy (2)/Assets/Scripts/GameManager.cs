using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public delegate void MoneyUpdateHandler();
public delegate void HealthUpdateHandler();

public class GameManager : MonoBehaviour
{
    public static MoneyUpdateHandler onMoneyUpdate;
    public static HealthUpdateHandler onHealthUpdate;
    public static float playerMoney = 25;
    public static TowerHandler towerHandler;
    public static int currentHealth = 10;


    // Start is called before the first frame update
    void Start()
    {
        towerHandler = this.GetComponent<TowerHandler>();
    }


    public static void removeHealth(int amount)
    {
        currentHealth -= amount;
        if(currentHealth < 0)
        {
            currentHealth = 10;
        }
        onHealthUpdate();
    }

    public static void addMoney(float amount)
    {
        playerMoney += amount;
        onMoneyUpdate();
    }

    public static void removeMoney(float amount)
    {
        playerMoney -= amount;
        onMoneyUpdate();
    }

}
