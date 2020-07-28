using System.Collections;
using NodeCanvas.Framework;
using UnityEngine;

namespace MyGame.Weapons.Projectiles.Scripts
{
    public class Bullet : Projectile
    {
        [SerializeField] private SignalDefinition applyDamageSignal = null;
        [SerializeField] private float selfDestroyTime = 5f;

        private Vector3 _lastPosition;
        private Coroutine _selfDestroyCoroutine;

        private void OnEnable()
        {
            _lastPosition = transform.position;
            _selfDestroyCoroutine = StartCoroutine(SelfDestroyWithDelay());
        }

        private void OnDisable()
        {
            StopCoroutine(_selfDestroyCoroutine);
        }

        private void Update()
        {
            MoveForward();
            CheckHit();
        }

        private void MoveForward()
        {
            Vector3 movement = Vector3.forward * speed * Time.deltaTime;
            transform.Translate(movement);
        }

        private IEnumerator SelfDestroyWithDelay()
        {
            yield return new WaitForSeconds(selfDestroyTime);
            SelfDestroy();
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }

        private void CheckHit()
        {
            Vector3 currentPosition = transform.position;
            if (_lastPosition != currentPosition)
            {
                Vector3 startPos = _lastPosition;
                Vector3 endPos = currentPosition;

                if (Physics.Linecast(startPos, endPos, out RaycastHit hit, layersToShoot))
                {
                    ApplyDamageTo(hit.transform);
                }

                _lastPosition = currentPosition;
            }
        }

        private void ApplyDamageTo(Transform target)
        {
            applyDamageSignal.Invoke(transform, target, false, damage);
            SelfDestroy();
        }
    }
}