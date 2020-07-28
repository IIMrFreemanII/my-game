using System.Collections;
using MyGame.Weapons.Projectiles.Scripts;
using UnityEngine;

namespace MyGame.Weapons.Guns.Scripts
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private new Camera camera = null;
        [SerializeField] private Transform firePositionTransform = null;
        [SerializeField] private Bullet bulletPrefab = null;
        
        [SerializeField] private float fireRate = 1;
        private float TimeToFire => 1 / fireRate;
        
        public GameObject owner = null;
        
        private Coroutine _fireCoroutine;
        
        private void Awake()
        {
            camera = camera ? camera : Camera.main;
            
            SetUpCursor();
        }

        private void Start()
        {
            transform.SetParent(camera.transform);
        }
        
        private void SetUpCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            HandleFire();
        }

        private void HandleFire()
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
            Quaternion spawnRotation = firePositionTransform.rotation;

            Bullet bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);
            bullet.Initialize(owner);
        }
    }
}