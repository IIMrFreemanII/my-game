using UnityEngine;

namespace MyGame.Weapons.Scripts
{
    public abstract class Weapon : MonoBehaviour
    {
        public new Collider collider;
        public Rigidbody rb;
        public GameObject owner = null;
        
        public abstract void HandleFire();
    }
}