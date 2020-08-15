using MyGame.Characters.Player.NewFPSController;
using UnityEngine;

namespace MyGame.Characters.Player.FPSController
{
    public class FpsController : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        
        [SerializeField] private MouseLook mouseLook = new MouseLook();
        [SerializeField] private Movement movement = new Movement();

        private void Start()
        {
            camera = camera ? camera : Camera.main;
            
            mouseLook.Init(transform, camera.transform);
            movement.Init(transform);
        }

        private void Update()
        {
            RotateView();
        }

        private void FixedUpdate()
        {
            movement.UpdateMovement();
        }

        private void RotateView()
        {
            mouseLook.LookRotation(transform, camera.transform);
        }
    }
}