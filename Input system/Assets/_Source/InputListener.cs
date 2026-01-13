using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputListener : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Button changeMapBut;
    private MainSystem_actions _inputSystemActions;

    private void Awake()
    {
        _inputSystemActions = new MainSystem_actions();
        changeMapBut.onClick.AddListener(ChangeMap);
        _inputSystemActions.Sub.Disable();
    }

    private void ChangeMap()
    {
        _inputSystemActions.Main.Disable();
        _inputSystemActions.Sub.Enable();
    } 

    private void OnEnable()
    {
        Bind();
        _inputSystemActions.Enable();
    }

    private void Bind()
    {
        _inputSystemActions.Main.Jump.performed += OnJump;
        _inputSystemActions.Sub.Jump.performed += OnJump;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = _inputSystemActions.Main.Move.ReadValue<Vector2>();
        direction = _inputSystemActions.Sub.Move.ReadValue<Vector2>();
        playerMovement.Move(direction);
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        playerMovement.Jump();
    }

    private void Expose()
    {
        _inputSystemActions.Main.Jump.performed -= OnJump;
        _inputSystemActions.Sub.Jump.performed -= OnJump;
    }

    private void OnDisable()
    {
        _inputSystemActions.Disable();
        Expose();
    }
}
