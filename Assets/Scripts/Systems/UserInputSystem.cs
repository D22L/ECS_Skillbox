using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ECS_Project
{
    public class UserInputSystem : ComponentSystem
    {
        private EntityQuery _query;
        private EntityQuery _queryLogin;

        private InputAction _inputAction;
        private InputAction _shootAction;
        private InputAction _leapForwardAction;

        private float2 _moveInput;
        private float _shootInput;
        private float _leapForwardInput;

        private bool isAuthorized;
        protected override void OnCreate()
        {
            _query = GetEntityQuery(ComponentType.ReadOnly<InputData>());
            _queryLogin = GetEntityQuery(ComponentType.ReadOnly<LoginData>());
        }
        protected override void OnStartRunning()
        {
            _inputAction = new InputAction("move", binding: "<Gamepad>/rightStick");
            _inputAction.AddCompositeBinding("Dpad")
                .With("Up", binding: "<Keyboard>/w")
                .With("Down", binding: "<Keyboard>/s")
                .With("Left", binding: "<Keyboard>/a")
                .With("Right", binding: "<Keyboard>/d");

            _inputAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
            _inputAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
            _inputAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
            _inputAction.Enable();

            ///// shooting
            _shootAction = new InputAction("shoot", binding: "<Keyboard>/space");
            _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.Enable();

            // leap Forward            
            _leapForwardAction = new InputAction("leapForward", binding: "<Keyboard>/f");
            _leapForwardAction.performed += context => { _leapForwardInput = context.ReadValue<float>(); };
            _leapForwardAction.started += context => { _leapForwardInput = context.ReadValue<float>(); };
            _leapForwardAction.canceled += context => { _leapForwardInput = context.ReadValue<float>(); };
            _leapForwardAction.Enable();

            Entities.With(_queryLogin).ForEach(
                (Entity entity, LoginButtonComponent loginComponent) =>
                {
                    loginComponent.OnAuthorizated += () => isAuthorized = true;
                });
        }

        protected override void OnStopRunning()
        {
            _inputAction.Disable();
            _shootAction.Disable();
            _leapForwardAction.Disable();

            Entities.With(_queryLogin).ForEach(
               (Entity entity, LoginButtonComponent loginComponent) =>
               {
                   loginComponent.OnAuthorizated -= () => isAuthorized = true;
               });
        }

        protected override void OnUpdate()
        {
            if (!isAuthorized) return;

            Entities.With(_query).ForEach(
                (Entity entity, ref InputData inputData) =>
                {
                    inputData.input = _moveInput;
                    inputData.shoot = _shootInput;
                    inputData.leapForward = _leapForwardInput;
                });
        }


    }
}
