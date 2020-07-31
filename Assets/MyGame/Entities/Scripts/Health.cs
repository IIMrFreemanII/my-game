using System;
using NodeCanvas.Framework;
using UnityEngine;

namespace MyGame.Entities.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        private float _currentHealth;
        [SerializeField] private SignalDefinition applyDamageSignal = null;

        public event Action<float> OnHealthPercentChanged; 
        public event Action<float, Vector3> OnDamage; 
        
        private void OnEnable()
        {
            _currentHealth = maxHealth;
            applyDamageSignal.onInvoke += OnApplyDamageSignal;
        }

        private void OnDisable()
        {
            applyDamageSignal.onInvoke -= OnApplyDamageSignal;
        }

        public float CurrentHealth
        {
            get => _currentHealth;
        
            private set
            {
                if (value <= 0)
                {
                    Die();
                }

                HandleHealthPercent(value, maxHealth);
                _currentHealth = value;
            }
        }

        private void HandleHealthPercent(float currHealth, float maxHealth)
        {
            // value between 0 and 1
            float normalizedHealth = currHealth / maxHealth;
            OnHealthPercentChanged?.Invoke(normalizedHealth);
        }

        private void OnApplyDamageSignal(Transform sender, Transform receiver, bool isGlobal, object[] args)
        {
            if (receiver != transform) return;
        
            float damage = (float)args[0];
            ApplyDamage(damage, sender.position);
        }

        public void ApplyDamage(float damage, Vector3 hitPos)
        {
            CurrentHealth -= damage;
            OnDamage?.Invoke(damage, hitPos);
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
