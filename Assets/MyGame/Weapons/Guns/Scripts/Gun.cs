using System.Collections;
using MyGame.Weapons.Projectiles.Scripts;
using MyGame.Weapons.Scripts;
using UnityEngine;

namespace MyGame.Weapons.Guns.Scripts
{
    public class Gun : Weapon
    {
        [SerializeField] private new Camera camera = null;
        [SerializeField] private Transform firePositionTransform = null;
        [SerializeField] private Bullet bulletPrefab = null;
        public LayerMask layerToHit;
        public float maxDistToHit = 100f;
        

        [SerializeField] private float fireRate = 1;
        private float TimeToFire => 1 / fireRate;

        private Coroutine _fireCoroutine;
        
        private void Awake()
        {
            camera = camera ? camera : Camera.main;
            collider = collider ? collider : GetComponent<Collider>();
            rb = rb ? rb : GetComponent<Rigidbody>();

            SetUpCursor();
        }

        private void OnValidate()
        {
            layerToHit = LayerMask.GetMask("Default", "Enemy");
        }

        private void SetUpCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void HandleFire()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_fireCoroutine == null)
                {
                    _fireCoroutine = StartCoroutine(FireCoroutine());
                }
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                if (_fireCoroutine != null)
                {
                    StopCoroutine(_fireCoroutine);
                    _fireCoroutine = null;
                }
            }
        }

        private void Fire()
        {
            CreateBullet();
        }

        private IEnumerator FireCoroutine()
        {
            while (true)
            {
                Fire();
                yield return new WaitForSeconds(TimeToFire);
            }
        }

        private void CreateBullet()
        {
            Vector3 spawnPosition = firePositionTransform.position;
            Quaternion spawnRotation = Quaternion.LookRotation(GetDirToTarget());

            Bullet bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);
            bullet.Initialize(owner);
        }

        private Vector3 GetDirToTarget()
        {
            Vector3 camPos = camera.transform.position;
            Vector3 camForwardDir = camera.transform.forward;
            
            if (Physics.Raycast(camPos, camForwardDir, out RaycastHit hit, maxDistToHit, layerToHit))
            {
                Vector3 dirToTarget = (hit.point - firePositionTransform.position).normalized;
                return dirToTarget;
            }
            
            return camForwardDir;
        }
    }
}