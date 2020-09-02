using System.Collections;
using MyGame.Weapons.Projectiles.ScriptableObjects;
using UnityEngine;

namespace MyGame.Weapons.Projectiles.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletSO bulletSO = null;
        public GameObject owner;

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
        
        public void Initialize(GameObject owner)
        {
            this.owner = owner;
        }

        private void MoveForward()
        {
            Vector3 movement = Vector3.forward * bulletSO.speed * Time.deltaTime;
            transform.Translate(movement);
        }

        private IEnumerator SelfDestroyWithDelay()
        {
            yield return new WaitForSeconds(bulletSO.selfDestroyTime);
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

                if (Physics.Linecast(startPos, endPos, out RaycastHit hit, bulletSO.layersToHit))
                {
                    transform.position = hit.point;
                    
                    ApplyDamageTo(hit.transform);
                    SpawnImpact(hit);
                }

                _lastPosition = currentPosition;
            }
        }

        private void SpawnImpact(RaycastHit hit)
        {
            Vector3 pos = hit.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            GameObject impactGO = Instantiate(bulletSO.impactPrefab, pos, rot, hit.transform);
            
            ParticleHandler particleHandler = impactGO.GetComponent<ParticleHandler>();
            particleHandler.Play();
        }

        private void ApplyDamageTo(Transform target)
        {
            bulletSO.applyDamageSignal.Invoke(transform, target, false, bulletSO.damage, owner.transform);
            SelfDestroy();
        }
    }
}