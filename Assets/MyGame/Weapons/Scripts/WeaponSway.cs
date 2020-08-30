using MyGame.Characters.Player.FPSController;
using UnityEngine;

namespace MyGame.Weapons.Scripts
{
    public class WeaponSway : MonoBehaviour
    {
        [SerializeField] private Transform weaponPosition = null;
        
        [Tooltip("Toggle weapon sway.")]
        public bool weaponSway;

        [SerializeField] private float swayAmount = 0.02f;
        [SerializeField] private float maxSwayAmount = 0.06f;
        [SerializeField] private float swaySmoothValue = 4.0f;

        private Vector3 initialSwayPosition = Vector3.zero;

        private void LateUpdate()
        {
            //Weapon sway
            if (weaponSway)
            {
                float movementX = -FpsInput.MouseX * swayAmount;
                float movementY = -FpsInput.MouseY * swayAmount;

                //Clamp movement to min and max values
                movementX = Mathf.Clamp(movementX, -maxSwayAmount, maxSwayAmount);
                movementY = Mathf.Clamp(movementY, -maxSwayAmount, maxSwayAmount);

                //Lerp local pos
                Vector3 finalSwayPosition = new Vector3(movementX, movementY, 0);
                weaponPosition.localPosition = Vector3.Lerp(weaponPosition.localPosition, finalSwayPosition + initialSwayPosition,
                    Time.deltaTime * swaySmoothValue);
            }
        }
    }
}