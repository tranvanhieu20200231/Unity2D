using UnityEngine;

public class ShootBallHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Update()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = -enemy.localScale.x;
        transform.localScale = newScale;
    }
}