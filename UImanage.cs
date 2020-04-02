using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Image healthBar;
    
    public void UpdateHealthBar(int curAmount,int maxAmount)
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }
}
