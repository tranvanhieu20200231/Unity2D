using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Animator anim;
    public int maxHealth = 500;
    public HealthBarEnemy healthBar;

    [Header ("Attack Parameter")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damageEnemy;

    [Header("Collider Parameter")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Controller playerHealth;

    [SerializeField] private LevelManager loadLevel;
    private float cooldownTimer = Mathf.Infinity;
    private EnemyPatrol enemyPatrol;

    private int currentHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if(cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attack");
            }
        }

        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }

        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Controller>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
              new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamagePlayer(damageEnemy);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            anim.SetTrigger("Died");
        }

        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    void Die()
    {
        //Xoa hanh dong enemy
        if(GetComponentInParent<EnemyPatrol>() != null)
            GetComponentInParent<EnemyPatrol>().enabled = false;
        //Xoa hitbox enemy
        if (GetComponent<Collider2D>() != null)
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        Destroy(gameObject);
        loadLevel.EnemyDestroyed();
    }
}