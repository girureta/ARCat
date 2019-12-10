using UnityEngine;

public class LavaBlockController : MonoBehaviour
{
    /// <summary>
    /// How many seconds between damage
    /// </summary>
    public float damageInterval = 2.0f;
    protected float nextDamage = Mathf.NegativeInfinity;

    public float damage = 10.0f;

    public MeshRenderer meshRenderer;
    protected static Material materialInstance = null;
    protected bool isMainBlock = false;

    private void OnEnable()
    {
        if (materialInstance == null)
        {
            //The first LavaBlock to be enabled create a copy of the material.
            materialInstance = meshRenderer.material;
            isMainBlock = true;
        }
        meshRenderer.sharedMaterial = materialInstance;
    }

    public void Update()
    {
        if (isMainBlock)
        {
            materialInstance.SetColor("_EmissionColor", Color.white * Mathf.PingPong(Time.time, 2.0f));
        }
    }

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
