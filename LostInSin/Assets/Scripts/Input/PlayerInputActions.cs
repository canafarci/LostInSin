//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Input/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace LostInSin.Input
{
    public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""90da9796-83a1-4f99-9637-8f2c45033174"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""f15bcf68-c50c-4776-b541-e0a943cbbfe3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera Movement"",
                    ""type"": ""Value"",
                    ""id"": ""ad08619c-7292-4398-925f-ccfc90305668"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Camera Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""1b049c11-0bcc-4d57-aa72-8ecbea3694ce"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Camera Rotation Start/Stop"",
                    ""type"": ""Value"",
                    ""id"": ""777c3b59-9a31-4e2a-94c8-66497b0a4a33"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Camera Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""cadfbee2-1a7c-40a5-9708-1e8380d1f125"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b693b810-b247-49ce-96b8-6bf711fe47b7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a93ca7f6-389d-4b2b-a3e9-8c189a86caee"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""aa9d8da2-8976-4272-b15f-104c5e3fe301"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f778460c-789f-4600-ba84-b8e5e61b665a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""da69cbaa-9194-4938-ae47-c0626dcc31b0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b20b8288-7462-460b-bf0f-73baa370031c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ce648ba5-bdf7-4fa3-bb97-cfcd10d1a9a1"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aedafafd-a370-4e69-bdd8-45aaad410420"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Rotation Start/Stop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82adadb6-eb75-47a5-adf3-1d48f86dab4e"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_Click = m_Gameplay.FindAction("Click", throwIfNotFound: true);
            m_Gameplay_CameraMovement = m_Gameplay.FindAction("Camera Movement", throwIfNotFound: true);
            m_Gameplay_CameraZoom = m_Gameplay.FindAction("Camera Zoom", throwIfNotFound: true);
            m_Gameplay_CameraRotationStartStop = m_Gameplay.FindAction("Camera Rotation Start/Stop", throwIfNotFound: true);
            m_Gameplay_CameraRotation = m_Gameplay.FindAction("Camera Rotation", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
        private readonly InputAction m_Gameplay_Click;
        private readonly InputAction m_Gameplay_CameraMovement;
        private readonly InputAction m_Gameplay_CameraZoom;
        private readonly InputAction m_Gameplay_CameraRotationStartStop;
        private readonly InputAction m_Gameplay_CameraRotation;
        public struct GameplayActions
        {
            private @PlayerInputActions m_Wrapper;
            public GameplayActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Click => m_Wrapper.m_Gameplay_Click;
            public InputAction @CameraMovement => m_Wrapper.m_Gameplay_CameraMovement;
            public InputAction @CameraZoom => m_Wrapper.m_Gameplay_CameraZoom;
            public InputAction @CameraRotationStartStop => m_Wrapper.m_Gameplay_CameraRotationStartStop;
            public InputAction @CameraRotation => m_Wrapper.m_Gameplay_CameraRotation;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void AddCallbacks(IGameplayActions instance)
            {
                if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @CameraMovement.started += instance.OnCameraMovement;
                @CameraMovement.performed += instance.OnCameraMovement;
                @CameraMovement.canceled += instance.OnCameraMovement;
                @CameraZoom.started += instance.OnCameraZoom;
                @CameraZoom.performed += instance.OnCameraZoom;
                @CameraZoom.canceled += instance.OnCameraZoom;
                @CameraRotationStartStop.started += instance.OnCameraRotationStartStop;
                @CameraRotationStartStop.performed += instance.OnCameraRotationStartStop;
                @CameraRotationStartStop.canceled += instance.OnCameraRotationStartStop;
                @CameraRotation.started += instance.OnCameraRotation;
                @CameraRotation.performed += instance.OnCameraRotation;
                @CameraRotation.canceled += instance.OnCameraRotation;
            }

            private void UnregisterCallbacks(IGameplayActions instance)
            {
                @Click.started -= instance.OnClick;
                @Click.performed -= instance.OnClick;
                @Click.canceled -= instance.OnClick;
                @CameraMovement.started -= instance.OnCameraMovement;
                @CameraMovement.performed -= instance.OnCameraMovement;
                @CameraMovement.canceled -= instance.OnCameraMovement;
                @CameraZoom.started -= instance.OnCameraZoom;
                @CameraZoom.performed -= instance.OnCameraZoom;
                @CameraZoom.canceled -= instance.OnCameraZoom;
                @CameraRotationStartStop.started -= instance.OnCameraRotationStartStop;
                @CameraRotationStartStop.performed -= instance.OnCameraRotationStartStop;
                @CameraRotationStartStop.canceled -= instance.OnCameraRotationStartStop;
                @CameraRotation.started -= instance.OnCameraRotation;
                @CameraRotation.performed -= instance.OnCameraRotation;
                @CameraRotation.canceled -= instance.OnCameraRotation;
            }

            public void RemoveCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IGameplayActions instance)
            {
                foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);
        public interface IGameplayActions
        {
            void OnClick(InputAction.CallbackContext context);
            void OnCameraMovement(InputAction.CallbackContext context);
            void OnCameraZoom(InputAction.CallbackContext context);
            void OnCameraRotationStartStop(InputAction.CallbackContext context);
            void OnCameraRotation(InputAction.CallbackContext context);
        }
    }
}
