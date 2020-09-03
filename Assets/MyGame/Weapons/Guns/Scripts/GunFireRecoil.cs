using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyGame.Weapons.Guns.Scripts
{
    [Serializable]
    public class GunFireRecoil
    {
        [Header("Speed settings")]
        [SerializeField] private float positionRecoilSpeed = 8f;
        [SerializeField] private float rotationRecoilSpeed = 8f;
        [SerializeField] private float positionReturnSpeed = 18f;
        [SerializeField] private float rotationReturnSpeed = 38f;
        
        [Header("Amount settings")]
        [SerializeField] private Vector3 recoilAmountRotation = new Vector3(10f, 5f, 7f);
        [SerializeField] private Vector3 recoilKickBack = new Vector3(0.015f, 0f, -0.2f);
        
        private Vector3 _currentRotationRecoil;
        private Vector3 _currentPositionRecoil;
        private Vector3 _rotation;
        private Transform _transform;

        public void Init(Transform transform)
        {
            _transform = transform;
        }

        public void Update()
        {
            _currentRotationRecoil =
                Vector3.Slerp(_currentRotationRecoil, Vector3.zero, rotationReturnSpeed * Time.deltaTime);
            _currentPositionRecoil =
                Vector3.Slerp(_currentPositionRecoil, Vector3.zero, positionReturnSpeed * Time.deltaTime);

            _transform.localPosition = Vector3.Slerp(_transform.localPosition, _currentPositionRecoil,
                positionRecoilSpeed * Time.deltaTime);
            _rotation = Vector3.Slerp(_rotation, _currentRotationRecoil, rotationRecoilSpeed * Time.deltaTime);
            _transform.localRotation = Quaternion.Euler(_rotation);
        }

        public void Recoil()
        {
            float posX = Random.Range(-recoilKickBack.x, recoilKickBack.x);
            float posY = Random.Range(-recoilKickBack.y, recoilKickBack.y);
            float posZ = recoilKickBack.z;
            _currentPositionRecoil += new Vector3(posX, posY, posZ);
            
            float rotX = -recoilAmountRotation.x;
            float rotY = Random.Range(-recoilAmountRotation.y, recoilAmountRotation.y);
            float rotZ = Random.Range(-recoilAmountRotation.z, recoilAmountRotation.z);
            _currentRotationRecoil += new Vector3(rotX, rotY, rotZ);
        }
    }
}