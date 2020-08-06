using MyGame.Entities.Scripts;
using UnityEngine;

namespace MyGame.Weapons.DamagePopups.HitDamagePopup
{
    public class HandleDamagePopup : MonoBehaviour
    {
        [SerializeField] private DamagePopup damagePopup = null;
        [SerializeField] private Health health;

        [SerializeField] private Vector3 minOffset = new Vector3(1, 1, 0);
        [SerializeField] private Vector3 maxOffset = new Vector3(-1, -1, 0);

        private void Awake()
        {
            health = health ? health : GetComponent<Health>();
        }

        private void OnEnable()
        {
            health.OnDamage += CreateDamagePopup;
        }
        
        private void OnDisable()
        {
            health.OnDamage -= CreateDamagePopup;
        }

        private void CreateDamagePopup(float damage, Vector3 hitPos)
        {
            float randX = Random.Range(minOffset.x, maxOffset.x);
            float randY = Random.Range(minOffset.y, maxOffset.y);
            float randZ = Random.Range(minOffset.z, maxOffset.z);

            Vector3 spawnPos = hitPos + new Vector3(randX, randY, randZ);
            DamagePopup popup = Instantiate(damagePopup, spawnPos, Quaternion.identity);
            popup.Setup(damage);
        }
    }
}
