using MyGame.Weapons.Projectiles.Scripts;
using UnityEngine;

namespace MyGame.Weapons.Guns.Scripts
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private Transform firePositionTransform;
        [SerializeField] private Bullet bulletPrefab;
        public GameObject owner;
        [SerializeField] private float fireRate;

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
                CreateBullet();
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