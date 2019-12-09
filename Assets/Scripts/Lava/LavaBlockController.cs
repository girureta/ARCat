using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBlockController : MonoBehaviour
{
    /// <summary>
    /// How many seconds between damage
    /// </summary>
    public float damageInterval = 1.0f;
    protected float nextDamage = Mathf.NegativeInfinity;

    public float damage = 10.0f;

    private void OnTriggerStay(Collider other)
    {
        CatController cat = other.GetComponent<CatController>();
        if (cat != null && Time.time>=nextDamage)
        {
            nextDamage = Time.time + damageInterval;
            cat.ApplyDamage(damage);
        }
    }
}
