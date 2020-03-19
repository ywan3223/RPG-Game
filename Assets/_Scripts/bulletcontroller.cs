using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcontroller : MonoBehaviour
{
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2f);
    }
     void Update()
    {
        
    }

    // Update is called once per frame
    public void Move(Vector2 moveDirection, float moveForce)
    {
        rb.AddForce(moveDirection * moveForce);
    }
    void OnCollisionEnter2D(Collision2D other)

    {
        Destroy(this.gameObject);  
    }
}
