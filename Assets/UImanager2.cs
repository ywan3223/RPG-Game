using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager2 : MonoBehaviour
{
    public static UImanager2 instance { get; private set; }
    void Start()
    {
        instance = this;
    }
    public Image healthBar;

    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }
}
