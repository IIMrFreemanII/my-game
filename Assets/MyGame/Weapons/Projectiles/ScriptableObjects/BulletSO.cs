using NodeCanvas.Framework;
using UnityEngine;

namespace MyGame.Weapons.Projectiles.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Bullets/BulletSO")]
    public class BulletSO : ScriptableObject
    {
        [Header("Properties")]
        public float damage;
        public float speed;
        public LayerMask layersToHit;
        public float selfDestroyTime = 5f;
        
        [Header("Prefabs")]
        public GameObject impactPrefab;
        
        [Header("NodeCanvas")]
        public SignalDefinition applyDamageSignal;
        
        private void OnValidate()
        {
            layersToHit = LayerMask.GetMask("Default", "Enemy");
        }
    }
}