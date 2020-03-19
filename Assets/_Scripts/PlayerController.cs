using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rb;
    private float moveH, moveV;
    [SerializeField] private float moveSpeed = 5.0f;
    private int maxHealth = 5;
    private int currentHealth;
    
   private Vector2 lookDirection = new Vector2(1, 0);//default
    public GameObject bulletPrefab;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveH = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveV = Input.GetAxisRaw("Vertical") * moveSpeed;

        Vector2 moveVector = new Vector2(moveH, moveV);

        if (moveVector.x != 0 || moveVector.y != 0)
        {
            lookDirection = moveVector;
        }

        //anim

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, lookDirection, 2f, LayerMask.GetMask("NPC"));
            if (hit.collider != null) {
                NPCmanager npc = hit.collider.GetComponent<NPCmanager>();

                if (npc != null) {
                    npc.ShowDialog();
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject bullet = Instantiate(bulletPrefab, rb.position, Quaternion.identity);
            bulletcontroller bc = bullet.GetComponent<bulletcontroller>();
            if (bc != null)
            {
                bc.Move(lookDirection, 100);
            }
        }
    }

    
   


    public void FixedUpdate()
    {
       
        rb.velocity = new Vector2(moveH, moveV);
    }
}
