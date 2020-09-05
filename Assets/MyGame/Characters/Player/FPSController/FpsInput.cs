using UnityEngine;

namespace MyGame.Characters.Player.FPSController
{
    public static class FpsInput
    {
        public static float MouseX => Input.GetAxis("Mouse X");
        public static float MouseY => Input.GetAxis("Mouse Y");
        
        public static float Horizontal => Input.GetAxisRaw("Horizontal");
        public static float Vertical => Input.GetAxisRaw("Vertical");

        public static bool Run => Input.GetButton("Fire3");
        public static bool Jump => Input.GetButton("Jump");
        public static bool Aim => Input.GetMouseButton(1);
    }
}