using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputListener : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Button changeMapBut;
    private MainSystem_actions _inputSystemActions;
    private InputAction inputActionMove;
    private InputAction inputActionJump;
    private void Awake()
    {
        _inputSystemActions = new MainSystem_actions();
        changeMapBut.onClick.AddListener(ChangeMap);
        inputActionMove = _inputSystemActions.FindAction("Move");
        inputActionJump = _inputSystemActions.FindAction("Jump");
    }

    private void ChangeMap()
    {
        if (_inputSystemActions.Main.enabled == true)
         {
            _inputSystemActions.Main.Disable();
            _inputSystemActions.Sub.Enable(); 
        }
        else
        {
            _inputSystemActions.Main.Enable();
            _inputSystemActions.Sub.Disable();
        }
        inputActionMove = _inputSystemActions.FindAction("Move");
    } 

    private void OnEnable()
    {
        Bind();
        _inputSystemActions.Enable();
    }

    private void Bind()
    {
        inputActionJump.performed += OnJump;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = inputActionMove.ReadValue<Vector2>();
        playerMovement.Move(direction); 
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        playerMovement.Jump();
    }

    private void Expose()
    {
        inputActionJump.performed -= OnJump;
    }

    private void OnDisable()
    {
        _inputSystemActions.Disable();
        Expose();
    }
}
