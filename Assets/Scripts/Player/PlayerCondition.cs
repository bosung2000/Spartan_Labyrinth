using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable
{
    void TakePhysicalDamage(int damge);
}
public class PlayerCondition : MonoBehaviour ,IDamageable
{
    public UICondition health;

    public event Action OnTakeDamage;

    private void Update()
    {

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
