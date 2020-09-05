using System;
using MyGame.Characters.Player.FPSController;
using MyGame.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyGame.Weapons.Guns.Scripts
{
    [Serializable]
    public class GunFireRecoil
    {
        [Header("Debug")]
        [SerializeField] private bool aimDebug = false;

        [Header("Weapon positions")] 
        [SerializeField] private Vector3 defaultPosition = Vector3.zero;
        [SerializeField] private Vector3 aimPosition = Vector3.zero;

        [Header("Time settings")] 
        [SerializeField] private float positionRecoilTime = 0.1f;
        [SerializeField] private float positionReturnRecoilTime = 0.05f;
        [SerializeField] private float rotationRecoilTime = 0.1f;
        [SerializeField] private float rotationReturnRecoilTime = 0.05f;

        [Header("Amount settings")] 
        [SerializeField] private Vector3 recoilAmountRotation = new Vector3(5f, 2f, 4f);
        [SerializeField] private Vector3 recoilKickBack = new Vector3(0.015f, 0f, -0.2f);

        private SmoothVelocityVector _currentRotationRecoil = new SmoothVelocityVector();
        private SmoothVelocityVector _localRotation = new SmoothVelocityVector();
        private SmoothVelocityVector _currentPositionRecoil = new SmoothVelocityVector();
        private SmoothVelocityVector _localPosition = new SmoothVelocityVector();

        private Transform _transform;

        public void Init(Transform transform)
        {
            _transform = transform;
        }

        public void Update()
        {
            bool aim = aimDebug ? aimDebug : FpsInput.Aim;
            Vector3 initialPosition = aim ? aimPosition : defaultPosition;

            Vector3 currentRotationRecoil = _currentRotationRecoil.Update(Vector3.zero, rotationReturnRecoilTime);
            Vector3 localRotation = _localRotation.Update(currentRotationRecoil, rotationRecoilTime);
            _transform.localRotation = Quaternion.Euler(localRotation);

            Vector3 currentPositionRecoil = _currentPositionRecoil.Update(initialPosition, positionReturnRecoilTime);
            _transform.localPosition = _localPosition.Update(currentPositionRecoil, positionRecoilTime);
        }

        public void Recoil()
        {
            float posX = Random.Range(-recoilKickBack.x, recoilKickBack.x);
            float posY = Random.Range(-recoilKickBack.y, recoilKickBack.y);
            float posZ = recoilKickBack.z;
            _currentPositionRecoil.Current += new Vector3(posX, posY, posZ);

            float rotX = -recoilAmountRotation.x;
            float rotY = Random.Range(-recoilAmountRotation.y, recoilAmountRotation.y);
            float rotZ = Random.Range(-recoilAmountRotation.z, recoilAmountRotation.z);
            _currentRotationRecoil.Current += new Vector3(rotX, rotY, rotZ);
        }
    }
}