using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image _healthbar;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _healthbar.fillAmount = currentHealth / maxHealth;
    }
}
