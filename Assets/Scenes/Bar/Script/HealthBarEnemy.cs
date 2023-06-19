using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    public Image fillBar;
    public TextMeshProUGUI valueText;
    public void UpdateBar(int currentValue, int maxValue)
    {
        fillBar.fillAmount = (float)currentValue / (float)maxValue;
        valueText.text = currentValue.ToString() + "/" + maxValue.ToString();

        Vector3 enemyScale = enemy.localScale;
        Vector3 fillBarScale = fillBar.transform.localScale;
        Vector3 valueTextScale = valueText.transform.localScale;

        if (enemyScale.x > 0 && fillBarScale.x > 0 || enemyScale.x < 0 && fillBarScale.x < 0)
        {
            fillBar.transform.localScale = Vector3.Scale(fillBarScale, new Vector3(-1, 1, 1));
            valueText.transform.localScale = Vector3.Scale(valueTextScale, new Vector3(-1, 1, 1));
        }
    }
}
