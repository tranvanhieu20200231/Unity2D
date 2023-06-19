using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Controller playerHealth;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private int healingNPC;
    public float speed;
    public float targetPosition;
    public Transform playerTransForm;

    private Rigidbody2D rb;
    private Transform target;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetFollow();
        FlipSprite();

        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= 5)
        {
            cooldownTimer = 0;
        anim.SetTrigger("healing");
        }

    }

    void TargetFollow()
    {
        if(Vector2.Distance(transform.position, target.position) > targetPosition)
        {
            anim.SetBool("move", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("move", false);
        }
    }

    void FlipSprite()
    {
        if(playerTransForm.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if(playerTransForm.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    //private void HealingPlayer()
    //{
    //    playerHealth.Healing(healingNPC);
    //}
}
