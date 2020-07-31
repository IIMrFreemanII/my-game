using UnityEngine;

public class DamageAmount : MonoBehaviour
{
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
