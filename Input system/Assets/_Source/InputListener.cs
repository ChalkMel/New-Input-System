using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputListener : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Button changeMapBut;
    private MainSystem_actions _inputSystemActions;
    private PlayerInput playerInput;
    private void Awake()
    {
        _inputSystemActions = new MainSystem_actions();
        changeMapBut.onClick.AddListener(ChangeMap);
        playerInput = GetComponent<PlayerInput>();
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
    } 

    private void OnEnable()
    {
        Bind();
        _inputSystemActions.Enable();
    }

    private void Bind()
    {
        _inputSystemActions.FindAction("Jump").performed += OnJump;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = _inputSystemActions.FindAction("Move").ReadValue<Vector2>();
        playerMovement.Move(direction);
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        playerMovement.Jump();
    }

    private void Expose()
    {
        _inputSystemActions.FindAction("Jump").performed -= OnJump;
    }

    private void OnDisable()
    {
        _inputSystemActions.Disable();
        Expose();
    }
}
