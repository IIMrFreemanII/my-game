using MyGame.Entities.Scripts;
using UnityEngine;

namespace MyGame.Weapons.DamagePopups.HitDamagePopup
{
    public class HandleDamagePopup : MonoBehaviour
    {
        [SerializeField] private DamagePopup damagePopup = null;
        [SerializeField] private Health health;

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
            DamagePopup popup = Instantiate(damagePopup, hitPos, Quaternion.identity);
            popup.Setup(damage);
        }
    }
}
