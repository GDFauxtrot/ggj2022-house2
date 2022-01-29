using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private int displayedMoney = 0; 
    private float timer = 0;
    private int money = 0;
    public RandomAudioPlayer collectMoneySound;

    void Start(){
        displayedMoney = money;
        moneyText.text = money.ToString();
    }

    void FixedUpdate(){
        UpdateMoneyText();
    }

    public int GetMoney(){
        return money;
    }

    public void AddMoney(int value){
        money += value;
        collectMoneySound.PlayRandomClip();
    }

    public void ReduceMoney(int value){
        money -= value;
    }

    public void UpdateMoneyText(){
        timer += Time.deltaTime;
        int diff = money - displayedMoney;
        if(diff != 0)
        {
            int minSpeed = diff < 0 ? -1 : 1;
            int speed = (diff / 10 == 0)? minSpeed : diff / 10;
            displayedMoney += speed;
            moneyText.text = displayedMoney.ToString();
        }

    }
}
