using UnityEngine;

namespace MyGame.Weapons.Projectiles.Scripts
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float damage;
        [SerializeField] protected float speed;
        public GameObject owner;
        [SerializeField] protected LayerMask layersToShoot;

        public void Initialize(GameObject owner)
        {
            this.owner = owner;
        }
    }
}