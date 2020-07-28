using NodeCanvas.Framework;
using TMPro;
using UnityEngine;

namespace MyGame.Entities.Scripts
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
        [SerializeField] private SignalDefinition applyDamageSignal = null;
        [SerializeField] private TMP_Text healthText = null;

        private void OnEnable()
        {
            applyDamageSignal.onInvoke += OnApplyDamageSignal;
        }

        private void OnDisable()
        {
            applyDamageSignal.onInvoke -= OnApplyDamageSignal;
        }

        public float Health
        {
            get => health;
        
            private set
            {
                if (value <= 0)
                {
                    Die();
                }
            
                SetUiHealthText(value);
                health = value;
            }
        }
    
        private void OnApplyDamageSignal(Transform sender, Transform receiver, bool isGlobal, object[] args)
        {
            if (receiver != transform) return;
        
            float damage = (float)args[0];
            ApplyDamage(damage);
        }

        public void ApplyDamage(float damage)
        {
            Health -= damage;
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void SetUiHealthText(float health)
        {
            if (healthText)
            {
                healthText.text = $"{health}";
            }
        }
    }
}
