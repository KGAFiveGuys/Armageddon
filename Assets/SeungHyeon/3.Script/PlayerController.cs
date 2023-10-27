using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveDirection;
    public float defaultSpeed = 4f;
    [SerializeField] public float currentSpeed;
    private Animator player_anim;

    public InputAction move;
    public InputAction run;

    private void OnEnable()
    {
        move.performed += OnMovePerformed;
        run.performed += OnRunPerformed;
        run.canceled += OnRunCanceled;

        move.Enable();
        run.Enable();
    }

    private void OnDestroy()
    {
        move.Disable();
        run.Disable();

        move.performed -= OnMovePerformed;
        run.performed -= OnRunPerformed;
        run.canceled -= OnRunCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        var v = context.ReadValue<Vector2>();
        moveDirection = new Vector3(v.x, 0f, v.y);
    }

    private void OnRunPerformed(InputAction.CallbackContext context)
    {
        var isRun = context.ReadValueAsButton();
        if (isRun)
            currentSpeed = defaultSpeed * 2f;
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        currentSpeed = defaultSpeed;
    }

    private void Awake()
    {
        TryGetComponent(out player_anim);
        currentSpeed = defaultSpeed;
    }
    private void Update()
    {
        bool hasControl = (moveDirection != Vector3.zero);
        if(hasControl)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            player_anim.SetInteger("AnimationPar", 1);
        }
        else
        {
            player_anim.SetInteger("AnimationPar", 0);
        }
    }
    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        if(input != null)
        {
            moveDirection = new Vector3(input.x, 0f, input.y);
        }
    }
    private void OnRun(InputValue value)
    {
        if (value.isPressed)
            currentSpeed = defaultSpeed * 2f;
        else
            currentSpeed = defaultSpeed;
    }
}
