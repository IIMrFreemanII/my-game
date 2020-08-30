﻿using MyGame.Weapons.Scripts;
using UnityEngine;

namespace MyGame.Characters.Player.FPSController
{
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
        }

        private void FixedUpdate()
        {
            bobStepCounter.Update(movement.IsGrounded);
            movement.UpdateMovement();
            headBob.Update(bobStepCounter.Count);
            handsBob.Update(bobStepCounter.Count);
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