using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rb;
    private Animator Anim;
    private float moveH, moveV;
    private bool isJumpPressed = false;
    private int maxHealth = 5;
    public int currentHealth;
    public int myMaxhealth { get { return maxHealth; } }
    public int myCurrentHealth { get { return currentHealth; } }
    private float invincibleTime = 2f;
    private float invincibleTimer;
    private bool isInvincible;

    [SerializeField] private float jumpForce;

    [SerializeField] private float moveSpeed;
    public GameObject bulletPrefab;
    public GameObject newPlayer;
    public GameObject explosion;
    private Vector2 lookDirection = new Vector2(1, 0);//default



    void Start()
    {
        currentHealth = 5;
        invincibleTimer = 0;
        rb = GetComponent<Rigidbody2D>();
        Anim = gameObject.GetComponent<Animator>();

        UImanager2.instance.UpdateHealthBar(currentHealth, maxHealth);

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
        ///////////////////////////////////////////////////
        if (Input.GetAxisRaw("Horizontal") > 0)
        {

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-0.075f, 0.075f);
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {

            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(0.075f, 0.075f);
        }
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.x);
            transform.localScale = new Vector2(0.075f, 0.075f);
        }
        else

        {

            rb.velocity = new Vector2(0, rb.velocity.y);

        }
        Anim.SetFloat("Speed", -rb.velocity.x);



        ///////////////////////////////////////////////////////////////////

        ////////////////////////////////
        if (Input.GetKey(KeyCode.K))
        {
            Anim.SetInteger("Attack", 0);
        }
        else
        {
            Anim.SetInteger("Attack", 1);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Anim.SetInteger("Jump", 0);
        }
        else
        {
            Anim.SetInteger("Jump", 1);
        }
        ///////////////////////////////////////////////////////////////////




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

            if (bc != null)
            {
                bc.Move(lookDirection.normalized, 200);
            }
        }

    }
    public void ChangeHealth(int amount)
    {
        //if (amount < 0)
        //{
        //    if (isInvincible == true)
        //    {
        //        return;
        //    }
        //    isInvincible = true;
        //    invincibleTimer = invincibleTime;
        //}
            Debug.Log(currentHealth + "/" + maxHealth);
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            UImanager2.instance.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth<=0)
        {
            GameOverUI.instance.GameOver(gameObject);

        }
            Debug.Log(currentHealth + "/" + maxHealth);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if(pc != null)
        {
            Debug.Log("health-1");
            pc.ChangeHealth(-1);
        }
            if (other.gameObject.CompareTag("posion"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
                Instantiate(newPlayer, transform.position, transform.rotation);
            }
        if (other.gameObject.CompareTag("Explo"))
        {

            Destroy(other.gameObject);
            ChangeHealth(-44);
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);

        }
    }

        void FixedUpdate()
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