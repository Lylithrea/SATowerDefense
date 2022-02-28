using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text playerMoneyText;
    public Text playerHealthText;
    public Text waveTimerText;
    public Text waveNumberText;
 
    // Start is called before the first frame update
    void Start()
    {
        updatePlayerMoneyText();
        GameManager.onMoneyUpdate += updatePlayerMoneyText;
        updatePlayerHealthText();
        GameManager.onHealthUpdate += updatePlayerHealthText;
    }

    public void updatePlayerMoneyText()
    {
        playerMoneyText.text = "$" + GameManager.playerMoney.ToString();
    }

    public void updatePlayerHealthText()
    {
        playerHealthText.text = "Health: " + GameManager.currentHealth.ToString();
    }

    public void updateWaveTimerText(bool isWaving, float time)
    {
        int timeInt = Mathf.RoundToInt(time);
        if (isWaving)
        {
            waveTimerText.text = "Wave timer: " + timeInt.ToString();
        }
        else
        {
            waveTimerText.text = "Build timer: " + timeInt.ToString();
        }

    }

    public void updateWaveNumberText(int number)
    {
        //increase number because arrays start at 0, but we like to visually see 1
        number++;
        waveNumberText.text = "Wave: " + number.ToString();
    }


}
