using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable
{
    void TakePhysicalDamage(int damge);
}
public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition health;

    public event Action OnTakeDamage;
    private void Start()
    {
        CharaterManager.Instance.Player.controller.Eat += OnHeal;
    }

    private void Update()
    {

    }



    public void OnHeal()
    {
        if (CharaterManager.Instance.Player.itemData.type == ItemType.Consumable)
        {
            for (int i = 0; i <CharaterManager.Instance.Player.itemData.consumables.Length ; i++)
            {
                switch (CharaterManager.Instance.Player.itemData.consumables[i].type)
                {
                    case ConsumableType.Health:
                        Heal(CharaterManager.Instance.Player.itemData.consumables[i].value);
                        break;
                    default:
                        break;
                }
            }
        }

    }
    public void Heal(float amount)
    {
        health.Add(amount);
    }
    private void Die()
    {
        Debug.Log("죽었다.");
    }

    public void TakePhysicalDamage(int damge)
    {
        health.Subtract(damge);
        OnTakeDamage?.Invoke();
    }
}
