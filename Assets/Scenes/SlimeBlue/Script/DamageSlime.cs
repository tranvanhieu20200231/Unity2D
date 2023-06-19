using UnityEngine;

public class DamageSlime : MonoBehaviour
{
    [SerializeField] private int damage;
    private float damageTimer = 0;

    private void Update()
    {
        damageTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageTimer >= 0.5)
        {
            damageTimer = 0;
            if (collision.tag == "Player")
            {
                collision.GetComponent<Controller>().TakeDamagePlayer(damage);
            }
        }
    }
}
