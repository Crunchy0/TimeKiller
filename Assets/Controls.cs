//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Controls.inputactions
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

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""80bab539-c003-4032-9944-14f4aeb70da1"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""572f7539-dac0-4d3f-8365-816ad36a4428"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""Value"",
                    ""id"": ""7fc70b2a-9577-4f32-8731-b5b97c73e799"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7da65954-81b4-42ed-b789-4043220a58a3"",
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
                    ""id"": ""9de28016-6e23-4fc5-bb13-ba29c876b5d0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cd2fa235-8523-446a-9194-2b2c049fcb66"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d1d2b61e-6505-4524-b32e-4a4fa5c0a4e2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b01c560d-5b87-4eca-88fd-5545e112c990"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c294bc6b-4e30-4c8d-be6d-cb8d94d7527e"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interactions"",
            ""id"": ""73dd37bf-24bb-4531-ac56-7fb37c5b3bf9"",
            ""actions"": [
                {
                    ""name"": ""PimaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""12417503-055b-48f7-ab04-6fd7fb2af354"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""8f7567c3-21b4-40c6-b1b6-a05c7dd80cbd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Equip"",
                    ""type"": ""Button"",
                    ""id"": ""d8a48dde-d3af-4bdc-9e9a-480bdc073edc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DetachFromEntity"",
                    ""type"": ""Button"",
                    ""id"": ""74dbda24-246f-4100-a5ac-f014e4e99535"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8368af9d-45d9-4cb4-960f-ff36c746f642"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""PimaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c89c8a80-01ab-4ec2-bd8e-8a4ec892a2bf"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""SecondaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6881ce96-40cc-43c3-9751-185a5c9d2dda"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Equip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb4c38c0-5410-412d-bf55-3a4e7fe59e5d"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Equip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08e2aa70-1006-4355-9174-3a3ad67574db"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Equip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8910c56f-198f-41f8-adcb-e2dd3aaeb386"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""DetachFromEntity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
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
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Move = m_Movement.FindAction("Move", throwIfNotFound: true);
        m_Movement_MouseLook = m_Movement.FindAction("MouseLook", throwIfNotFound: true);
        // Interactions
        m_Interactions = asset.FindActionMap("Interactions", throwIfNotFound: true);
        m_Interactions_PimaryFire = m_Interactions.FindAction("PimaryFire", throwIfNotFound: true);
        m_Interactions_SecondaryFire = m_Interactions.FindAction("SecondaryFire", throwIfNotFound: true);
        m_Interactions_Equip = m_Interactions.FindAction("Equip", throwIfNotFound: true);
        m_Interactions_DetachFromEntity = m_Interactions.FindAction("DetachFromEntity", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private List<IMovementActions> m_MovementActionsCallbackInterfaces = new List<IMovementActions>();
    private readonly InputAction m_Movement_Move;
    private readonly InputAction m_Movement_MouseLook;
    public struct MovementActions
    {
        private @Controls m_Wrapper;
        public MovementActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Movement_Move;
        public InputAction @MouseLook => m_Wrapper.m_Movement_MouseLook;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void AddCallbacks(IMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_MovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MovementActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @MouseLook.started += instance.OnMouseLook;
            @MouseLook.performed += instance.OnMouseLook;
            @MouseLook.canceled += instance.OnMouseLook;
        }

        private void UnregisterCallbacks(IMovementActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @MouseLook.started -= instance.OnMouseLook;
            @MouseLook.performed -= instance.OnMouseLook;
            @MouseLook.canceled -= instance.OnMouseLook;
        }

        public void RemoveCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_MovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // Interactions
    private readonly InputActionMap m_Interactions;
    private List<IInteractionsActions> m_InteractionsActionsCallbackInterfaces = new List<IInteractionsActions>();
    private readonly InputAction m_Interactions_PimaryFire;
    private readonly InputAction m_Interactions_SecondaryFire;
    private readonly InputAction m_Interactions_Equip;
    private readonly InputAction m_Interactions_DetachFromEntity;
    public struct InteractionsActions
    {
        private @Controls m_Wrapper;
        public InteractionsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PimaryFire => m_Wrapper.m_Interactions_PimaryFire;
        public InputAction @SecondaryFire => m_Wrapper.m_Interactions_SecondaryFire;
        public InputAction @Equip => m_Wrapper.m_Interactions_Equip;
        public InputAction @DetachFromEntity => m_Wrapper.m_Interactions_DetachFromEntity;
        public InputActionMap Get() { return m_Wrapper.m_Interactions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionsActions set) { return set.Get(); }
        public void AddCallbacks(IInteractionsActions instance)
        {
            if (instance == null || m_Wrapper.m_InteractionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InteractionsActionsCallbackInterfaces.Add(instance);
            @PimaryFire.started += instance.OnPimaryFire;
            @PimaryFire.performed += instance.OnPimaryFire;
            @PimaryFire.canceled += instance.OnPimaryFire;
            @SecondaryFire.started += instance.OnSecondaryFire;
            @SecondaryFire.performed += instance.OnSecondaryFire;
            @SecondaryFire.canceled += instance.OnSecondaryFire;
            @Equip.started += instance.OnEquip;
            @Equip.performed += instance.OnEquip;
            @Equip.canceled += instance.OnEquip;
            @DetachFromEntity.started += instance.OnDetachFromEntity;
            @DetachFromEntity.performed += instance.OnDetachFromEntity;
            @DetachFromEntity.canceled += instance.OnDetachFromEntity;
        }

        private void UnregisterCallbacks(IInteractionsActions instance)
        {
            @PimaryFire.started -= instance.OnPimaryFire;
            @PimaryFire.performed -= instance.OnPimaryFire;
            @PimaryFire.canceled -= instance.OnPimaryFire;
            @SecondaryFire.started -= instance.OnSecondaryFire;
            @SecondaryFire.performed -= instance.OnSecondaryFire;
            @SecondaryFire.canceled -= instance.OnSecondaryFire;
            @Equip.started -= instance.OnEquip;
            @Equip.performed -= instance.OnEquip;
            @Equip.canceled -= instance.OnEquip;
            @DetachFromEntity.started -= instance.OnDetachFromEntity;
            @DetachFromEntity.performed -= instance.OnDetachFromEntity;
            @DetachFromEntity.canceled -= instance.OnDetachFromEntity;
        }

        public void RemoveCallbacks(IInteractionsActions instance)
        {
            if (m_Wrapper.m_InteractionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInteractionsActions instance)
        {
            foreach (var item in m_Wrapper.m_InteractionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InteractionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InteractionsActions @Interactions => new InteractionsActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnMouseLook(InputAction.CallbackContext context);
    }
    public interface IInteractionsActions
    {
        void OnPimaryFire(InputAction.CallbackContext context);
        void OnSecondaryFire(InputAction.CallbackContext context);
        void OnEquip(InputAction.CallbackContext context);
        void OnDetachFromEntity(InputAction.CallbackContext context);
    }
}
