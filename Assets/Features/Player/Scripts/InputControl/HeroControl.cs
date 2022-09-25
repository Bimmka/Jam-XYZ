//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Features/Player/Scripts/InputControl/HeroControl.inputactions
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

namespace InputControl
{
    public partial class @HeroControl : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @HeroControl()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""HeroControl"",
    ""maps"": [
        {
            ""name"": ""Hero"",
            ""id"": ""d23f2734-01ce-4c9e-b45b-ea2965ad2505"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""fd967480-a53a-4716-9f6c-b0cce688e585"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interuct"",
                    ""type"": ""Button"",
                    ""id"": ""e6ddebbe-6723-49a9-b6cd-37004f55cbfc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Special Action"",
                    ""type"": ""Button"",
                    ""id"": ""2452d875-c0a0-4fb7-8568-ebfa706a54e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause Menu"",
                    ""type"": ""Button"",
                    ""id"": ""2d2c826b-84e4-4fec-af84-cdaee2b85fea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3d01a5b2-f6bf-4198-8935-2c80b2540783"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Interuct"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard WASD"",
                    ""id"": ""94c670fd-4f8d-419e-aa7d-0fec03f28cca"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5fe0531c-eb40-42f1-9f07-c84e9c675db4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7f8c43f5-63e9-4b7e-b5c2-3e0e191214fe"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""919ba52d-efaa-4965-906e-84711cdcf626"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""33b46722-ce75-480c-98cf-f5391af2ba51"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard Arrows"",
                    ""id"": ""2cb429f1-ec79-4fac-97a3-1fa8a727b7ab"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d03e7cf7-3d25-40ac-b7e3-2590fa5a7d6d"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1b1a11e1-2d7f-436c-950e-2b20a462e061"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""52e67155-2376-458c-bffc-82d3246ccbe6"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b8a9a614-1e0e-4190-83a8-91a167138a75"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2c782c8d-f5c1-413d-881d-32c62af7c05e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Special Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2df5e40-ae3a-4783-95f0-bc16d837645e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Pause Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard And Mouse"",
            ""bindingGroup"": ""Keyboard And Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Hero
            m_Hero = asset.FindActionMap("Hero", throwIfNotFound: true);
            m_Hero_Move = m_Hero.FindAction("Move", throwIfNotFound: true);
            m_Hero_Interuct = m_Hero.FindAction("Interuct", throwIfNotFound: true);
            m_Hero_SpecialAction = m_Hero.FindAction("Special Action", throwIfNotFound: true);
            m_Hero_PauseMenu = m_Hero.FindAction("Pause Menu", throwIfNotFound: true);
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

        // Hero
        private readonly InputActionMap m_Hero;
        private IHeroActions m_HeroActionsCallbackInterface;
        private readonly InputAction m_Hero_Move;
        private readonly InputAction m_Hero_Interuct;
        private readonly InputAction m_Hero_SpecialAction;
        private readonly InputAction m_Hero_PauseMenu;
        public struct HeroActions
        {
            private @HeroControl m_Wrapper;
            public HeroActions(@HeroControl wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Hero_Move;
            public InputAction @Interuct => m_Wrapper.m_Hero_Interuct;
            public InputAction @SpecialAction => m_Wrapper.m_Hero_SpecialAction;
            public InputAction @PauseMenu => m_Wrapper.m_Hero_PauseMenu;
            public InputActionMap Get() { return m_Wrapper.m_Hero; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(HeroActions set) { return set.Get(); }
            public void SetCallbacks(IHeroActions instance)
            {
                if (m_Wrapper.m_HeroActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnMove;
                    @Interuct.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteruct;
                    @Interuct.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteruct;
                    @Interuct.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteruct;
                    @SpecialAction.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnSpecialAction;
                    @SpecialAction.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnSpecialAction;
                    @SpecialAction.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnSpecialAction;
                    @PauseMenu.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnPauseMenu;
                    @PauseMenu.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnPauseMenu;
                    @PauseMenu.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnPauseMenu;
                }
                m_Wrapper.m_HeroActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Interuct.started += instance.OnInteruct;
                    @Interuct.performed += instance.OnInteruct;
                    @Interuct.canceled += instance.OnInteruct;
                    @SpecialAction.started += instance.OnSpecialAction;
                    @SpecialAction.performed += instance.OnSpecialAction;
                    @SpecialAction.canceled += instance.OnSpecialAction;
                    @PauseMenu.started += instance.OnPauseMenu;
                    @PauseMenu.performed += instance.OnPauseMenu;
                    @PauseMenu.canceled += instance.OnPauseMenu;
                }
            }
        }
        public HeroActions @Hero => new HeroActions(this);
        private int m_KeyboardAndMouseSchemeIndex = -1;
        public InputControlScheme KeyboardAndMouseScheme
        {
            get
            {
                if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard And Mouse");
                return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
            }
        }
        public interface IHeroActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnInteruct(InputAction.CallbackContext context);
            void OnSpecialAction(InputAction.CallbackContext context);
            void OnPauseMenu(InputAction.CallbackContext context);
        }
    }
}
