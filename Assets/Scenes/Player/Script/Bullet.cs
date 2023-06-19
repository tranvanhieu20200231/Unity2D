using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int attackShoot;
    private Rigidbody2D rb;
    private Controller controller;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(attackShoot);
        }

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(attackShoot);
        }

        gameObject.SetActive(false);
    }
}
