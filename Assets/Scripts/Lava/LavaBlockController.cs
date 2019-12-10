using UnityEngine;

public class LavaBlockController : MonoBehaviour
{
    /// <summary>
    /// How many seconds between damage
    /// </summary>
    public float damageInterval = 2.0f;
    protected float nextDamage = Mathf.NegativeInfinity;

    /// <summary>
    /// The ammount of damage to apply to the cat when we touch it.
    /// </summary>
    public float damage = 10.0f;

    /// <summary>
    /// The renderer of the block. Used to create copy of the shared material.
    /// </summary>
    public MeshRenderer meshRenderer;

    /// <summary>
    /// References to the runtime copy of the shared material. Used to avoid modifying the asset in disk.
    /// </summary>
    protected static Material materialInstance = null;

    /// <summary>
    /// Whether this Block animates the shared material values.
    /// </summary>
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
        //if it is the main block the we animaton the emission color.
        if (isMainBlock)
        {
            materialInstance.SetColor("_EmissionColor", Color.white * Mathf.PingPong(Time.time, 2.0f));
        }
    }

    /// <summary>
    /// Applies damage in case we touch the cat.
    /// </summary>
    /// <param name="other"></param>
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
