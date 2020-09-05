using UnityEngine;

namespace MyGame.Weapons.Scripts
{
    public abstract class Weapon : MonoBehaviour
    {
        public new Collider collider;
        public Rigidbody rb;
        public GameObject owner = null;
        
        public abstract void HandleFire();

        public void SetLayer(LayerMask layerMask)
        {
            void TraverseHierarchy(Transform root)
            {
                foreach (Transform child in root)
                {
                    child.gameObject.layer = layerMask;
                    TraverseHierarchy(child);
                }
            }
            
            TraverseHierarchy(transform);
        }
    }
}