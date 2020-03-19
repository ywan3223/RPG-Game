using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rb;

    private float moveH, moveV;
    private bool isJumpPressed = false;
    private int maxHealth = 5;
    private int currentHealth;
    public int myMaxhealth { get { return maxHealth; } }
    public int myCurrentHealth { get { return currentHealth; } }

    [SerializeField] private float jumpForce;

    [SerializeField] private float moveSpeed = 5.0f;
    public GameObject bulletPrefab;

    private Vector2 lookDirection = new Vector2(1, 0);//default

    private void Start()
    {
        currentHealth = 2;





        rb = GetComponent<Rigidbody2D>();
        if (isJumpPressed)
        {
            // the cube is going to move upwards in 10 units per second


            rb.velocity = new Vector2(-70, 30);
            Debug.Log("jump");
        }

    }

    private void Update()
    {

        moveH = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveV = Input.GetAxisRaw("Vertical") * moveSpeed;

        Vector2 moveVector = new Vector2(moveH, moveV);

        if (moveVector.x != 0 || moveVector.y != 0)
        {
            lookDirection = moveVector;
        }

        isJumpPressed = Input.GetButtonDown("Jump");











        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, lookDirection, 2f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NPCmanager npc = hit.collider.GetComponent<NPCmanager>();

                if (npc != null)
                {
                    npc.ShowDialog();
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject bullet = Instantiate(bulletPrefab, rb.position, Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            if(bc != null)
            {
                bc.Move(lookDirection, 50);
            }
        }
    }
    public void ChangeHealth(int amount)
    {
        Debug.Log(currentHealth + "/" + maxHealth);
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("posion"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
    }

    private void FixedUpdate()
    {

        rb.velocity = new Vector2(moveH, moveV);

        if (isJumpPressed)
        {
            // the cube is going to move upwards in 10 units per second
            rb.velocity = new Vector2(-100, 0);

            Debug.Log("jump");
        }
    }
}