using UnityEngine;

namespace MyGame.Characters.Player.FPSController
{
    [RequireComponent(typeof (AudioSource))]
    public class FpsController : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        
        [SerializeField] private MouseLook mouseLook = new MouseLook();
        [SerializeField] private Movement movement = new Movement();
        [SerializeField] private HeadBob headBob = new HeadBob();
        [SerializeField] private HandsBob handsBob = new HandsBob();
        [SerializeField] private BobStepCounter bobStepCounter = new BobStepCounter();

        private void Start()
        {
            camera = camera ? camera : Camera.main;
            
            mouseLook.Init(transform, camera.transform);
            movement.Init(transform);
            headBob.Init(transform, camera.transform);
            bobStepCounter.Init(transform);
        }

        private void Update()
        {
            RotateView();
            headBob.Update(bobStepCounter.Count);
            handsBob.Update(bobStepCounter.Count);
        }

        private void FixedUpdate()
        {
            bobStepCounter.FixedUpdate(movement.IsGrounded);
            movement.FixedUpdate(headBob.CameraYStep);
        }

        private void RotateView()
        {
            mouseLook.LookRotation(transform, camera.transform);
        }

        // private void OnCollisionEnter(Collision collision)
        // {
        //     float yImpulse = collision.impulse.y;
        //     if (yImpulse > 1)
        //     {
        //         Debug.Log(yImpulse);
        //     }
        // }
    }
}