using MyGame.Weapons.Projectiles.Scripts;
using UnityEngine;

namespace MyGame.Weapons.Guns.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Guns/GunSO")]
    public class GunSO : ScriptableObject
    {
        [Header("Sounds")]
        public AudioClip shoot;
        public AudioClip takeOut;
        public AudioClip holster;

        [Header("Particles")] 
        public GameObject muzzleFlash;
        public Bullet bullet;
    }
}