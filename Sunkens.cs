using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地刺
/// </summary>
public class Sunkens : MonoBehaviour
{


    public float mTimer;

    private void Update()
    {
        mTimer += Time.deltaTime;
    }

   

    void OnTriggerStay2D(Collider2D other)
    {
       
        if (mTimer<=1)
        {
            return;
        }

        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        Debug.Log(other.gameObject.name);
        if (pc != null)
        {
            pc.ChangeHealth(-1);
            mTimer = 0;
        }
      
    }

}
