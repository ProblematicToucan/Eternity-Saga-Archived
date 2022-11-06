//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.1
//     from Assets/GarammStudio/Input/GlobalControls.inputactions
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

namespace GarammStudio.Script.Input
{
    public partial class @GlobalControls : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GlobalControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GlobalControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""8d9020eb-be43-4b87-bd8d-990fb942e1eb"",
            ""actions"": [
                {
                    ""name"": ""CameraLook"",
                    ""type"": ""Value"",
                    ""id"": ""621a2084-bfa1-445a-a59c-9cbb5ee68591"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d1389996-09eb-46d6-a149-aa0ceb55ab91"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f736a217-c42d-4305-b77c-e37e063efa45"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""e0c4f565-ff22-40e4-ad54-38d4aecf28c0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e57c88c0-0fec-4208-bfda-196bbf9745c3"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""103d2728-b388-4b07-b7a1-ca5bac0c3994"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""d228ea6d-5fab-401e-b566-d80c1b06094b"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""f23872b1-20e9-45c7-b6d6-4c76b7c48140"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""9cab82cf-c2ab-42f9-b420-fcf954db0d0d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fdf5e2ef-cce0-4244-8cec-22e628a0d1b2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4820e6dc-d149-487c-b313-b844275a708d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1dd1ce3c-2ba1-4557-9de7-817aea16e93e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0b93de3c-6209-4752-8e8f-ed5b7bc2082d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d9d62660-3aa2-4240-bc2c-cd6ec32a16c9"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a45a6bfb-34ed-4a9b-9d8a-9b88f852d30b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c0f6d4a-b435-4329-974d-b41fc7833de7"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""NonGameplay"",
            ""id"": ""cf79c306-4209-4f91-b0a3-ce83677fa8c3"",
            ""actions"": [
                {
                    ""name"": ""ScreenPosition"",
                    ""type"": ""Value"",
                    ""id"": ""0ba83ba3-8b49-44e1-8f13-5a6415052c5f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ab196af7-f390-44a7-8e26-0534b82656db"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScreenPosition"",
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
            m_Gameplay_CameraLook = m_Gameplay.FindAction("CameraLook", throwIfNotFound: true);
            m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
            m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
            m_Gameplay_Dodge = m_Gameplay.FindAction("Dodge", throwIfNotFound: true);
            // NonGameplay
            m_NonGameplay = asset.FindActionMap("NonGameplay", throwIfNotFound: true);
            m_NonGameplay_ScreenPosition = m_NonGameplay.FindAction("ScreenPosition", throwIfNotFound: true);
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
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_CameraLook;
        private readonly InputAction m_Gameplay_Movement;
        private readonly InputAction m_Gameplay_Jump;
        private readonly InputAction m_Gameplay_Dodge;
        public struct GameplayActions
        {
            private @GlobalControls m_Wrapper;
            public GameplayActions(@GlobalControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @CameraLook => m_Wrapper.m_Gameplay_CameraLook;
            public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
            public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
            public InputAction @Dodge => m_Wrapper.m_Gameplay_Dodge;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @CameraLook.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraLook;
                    @CameraLook.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraLook;
                    @CameraLook.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraLook;
                    @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Dodge.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDodge;
                    @Dodge.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDodge;
                    @Dodge.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDodge;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @CameraLook.started += instance.OnCameraLook;
                    @CameraLook.performed += instance.OnCameraLook;
                    @CameraLook.canceled += instance.OnCameraLook;
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Dodge.started += instance.OnDodge;
                    @Dodge.performed += instance.OnDodge;
                    @Dodge.canceled += instance.OnDodge;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);

        // NonGameplay
        private readonly InputActionMap m_NonGameplay;
        private INonGameplayActions m_NonGameplayActionsCallbackInterface;
        private readonly InputAction m_NonGameplay_ScreenPosition;
        public struct NonGameplayActions
        {
            private @GlobalControls m_Wrapper;
            public NonGameplayActions(@GlobalControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @ScreenPosition => m_Wrapper.m_NonGameplay_ScreenPosition;
            public InputActionMap Get() { return m_Wrapper.m_NonGameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(NonGameplayActions set) { return set.Get(); }
            public void SetCallbacks(INonGameplayActions instance)
            {
                if (m_Wrapper.m_NonGameplayActionsCallbackInterface != null)
                {
                    @ScreenPosition.started -= m_Wrapper.m_NonGameplayActionsCallbackInterface.OnScreenPosition;
                    @ScreenPosition.performed -= m_Wrapper.m_NonGameplayActionsCallbackInterface.OnScreenPosition;
                    @ScreenPosition.canceled -= m_Wrapper.m_NonGameplayActionsCallbackInterface.OnScreenPosition;
                }
                m_Wrapper.m_NonGameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ScreenPosition.started += instance.OnScreenPosition;
                    @ScreenPosition.performed += instance.OnScreenPosition;
                    @ScreenPosition.canceled += instance.OnScreenPosition;
                }
            }
        }
        public NonGameplayActions @NonGameplay => new NonGameplayActions(this);
        public interface IGameplayActions
        {
            void OnCameraLook(InputAction.CallbackContext context);
            void OnMovement(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnDodge(InputAction.CallbackContext context);
        }
        public interface INonGameplayActions
        {
            void OnScreenPosition(InputAction.CallbackContext context);
        }
    }
}
