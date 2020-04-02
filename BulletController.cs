﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(Vector2 moveDirection,float moveForce)
    {
        rb.AddForce(moveDirection * moveForce);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
     //   Debug.Log(other.gameObject.name);
        if (other.gameObject.tag=="Player")
        {
            return;
        }
        EnemyController ec = other.gameObject.GetComponent<EnemyController>();
        if (ec != null)
        {
            UImanager2.CurrentScore += ec.mScore;
            Destroy(other.gameObject);
        }
        Destroy(this.gameObject);
    }
}
