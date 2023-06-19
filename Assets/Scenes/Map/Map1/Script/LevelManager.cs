using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI remainingEnemy;
    public int totalEnemies;
    private int remainingEnemies;

    private bool canEnterNextLevel = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        remainingEnemies = totalEnemies;
        CheckNextLevelAvailability();
        UpdateEnemies();
    }

    public void EnemyDestroyed()
    {
        remainingEnemies--;

        CheckNextLevelAvailability();

        UpdateEnemies();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canEnterNextLevel)
        {
            LoadNextLevel();
        }
    }

    private void CheckNextLevelAvailability()
    {
        if (remainingEnemies <= 0)
        {
            canEnterNextLevel = true;
            anim.SetTrigger("Open");
        }
        else
        {
            canEnterNextLevel = false;
        }
    }

    public void UpdateEnemies()
    {
        remainingEnemy.text = "Enemies : " + remainingEnemies.ToString() + " / " + totalEnemies.ToString();
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
