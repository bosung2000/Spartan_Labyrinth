using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitfall : MonoBehaviour
{
    public int damage;
    public float dmamgeRate;

    List<IDamageable> things = new List<IDamageable>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DealDamage", 0, dmamgeRate);
    }


    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakePhysicalDamage(damage);
            things.Add(damageable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            things.Remove(damageable);
        }
    }
}
