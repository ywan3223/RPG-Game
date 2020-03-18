using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCmanager : MonoBehaviour
{
    public GameObject DialogImage;
    public float showTime = 4;
    public float showTimer;

    //initialization
    void Start()
    {
        DialogImage.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        showTimer -= Time.deltaTime;

        if (showTimer < 0)
        {
            DialogImage.SetActive(false);
        }
    }

    public void ShowDialog()
    {
        showTimer = showTime;
        DialogImage.SetActive(true);
    }
}
