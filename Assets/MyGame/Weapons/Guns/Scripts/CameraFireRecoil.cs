using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyGame.Weapons.Guns.Scripts
{
    [Serializable]
    public class CameraFireRecoil
    {
        [SerializeField] private float recoilSpeed = 6f;
        [SerializeField] private float returnSpeed = 25f;

        [SerializeField] private Vector3 recoilRotAmount = new Vector3(6f, 1f, 6f);

        private Vector3 _currentRotation;
        private Vector3 _rotation;
        private Transform _cameraRecoil;

        public void Init(Transform camera)
        {
            _cameraRecoil = camera.parent;
        }

        public void Update()
        {
            _currentRotation = Vector3.Slerp(_currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
            _rotation = Vector3.Slerp(_rotation, _currentRotation, recoilSpeed * Time.deltaTime);
            _cameraRecoil.localRotation = Quaternion.Euler(_rotation);
        }

        public void Recoil()
        {
            float rotX = -recoilRotAmount.x;
            float rotY = Random.Range(-recoilRotAmount.y, recoilRotAmount.y);
            float rotZ = Random.Range(-recoilRotAmount.z, recoilRotAmount.z);
            _currentRotation += new Vector3(rotX, rotY, rotZ);
        }
    }
}
