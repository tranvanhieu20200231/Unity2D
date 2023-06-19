using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Controller : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRanged = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 0;
    public float attackRate = 2f;
    public float nextAttackTime = 2f;
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public int maxHealthPlayer;
    public HealthBarPlayer healthBar;
    public UnityEvent OnDeath;

    private int currentHealthPlayer;
    private float speed = 8.0f;
    private float jump = 8.0f;
    private bool canJump;
    private bool isfacingRight = true;
    private float move;
    private Rigidbody2D rb;
    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;
    private float healingTimer = Mathf.Infinity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealthPlayer = maxHealthPlayer;

        healthBar.UpdateBar(currentHealthPlayer, maxHealthPlayer);
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }
        }

        move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(move*speed,rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && canJump || Input.GetKeyDown(KeyCode.UpArrow) && canJump || Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            anim.SetTrigger("jump");
        }
        
        Flip();

        anim.SetFloat("move", Mathf.Abs(move));

        Healing();

    }

    private void OnTriggerEnter2D(Collider2D hitboxother)
    {
        if(hitboxother.gameObject.tag == "floor")
        {
            canJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D hitboxother)
    {
        if (hitboxother.gameObject.tag == "floor")
        {
            canJump = false;
        }
    }

    void Flip()
    {
        if((isfacingRight && move < 0) || (!isfacingRight && move > 0)) 
        {
            isfacingRight = !isfacingRight;
            Vector2 direction = transform.localScale;
            direction.x = direction.x * -1;
            transform.localScale = direction;
        }
    }

    void Attack()
    {
        //tam danh
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRanged, enemyLayers);
        //sat thuong
        foreach(Collider2D enemy in hitEnemies) 
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRanged);
    }

    public void TakeDamagePlayer(int damageEnemy)
    {
        currentHealthPlayer -= damageEnemy;

        anim.SetTrigger("hurt");

        if (currentHealthPlayer <= 0)
        {
            currentHealthPlayer = 0;

            anim.SetTrigger("die");
        }

        healthBar.UpdateBar(currentHealthPlayer, maxHealthPlayer);
    }

    public void Healing()
    {
        healingTimer += Time.deltaTime;

        if (healingTimer >= 5)
        {
            healingTimer = 0;
            currentHealthPlayer += 100;

            if (currentHealthPlayer >= maxHealthPlayer)
            {
               currentHealthPlayer = maxHealthPlayer;
            }

            healthBar.UpdateBar(currentHealthPlayer, maxHealthPlayer);
        }

    }

    public void Death()
    {
        Destroy(gameObject);
    }

}
