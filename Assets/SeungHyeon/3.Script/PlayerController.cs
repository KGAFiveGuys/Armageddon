using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveDirection;

    public float defaultSpeed = 4f;

    [SerializeField] private Slider StaminaSlider;

    [SerializeField] public float currentSpeed;
    [SerializeField] private float Maxstamina = 100f;
    [SerializeField] private float currentstamina;
    [SerializeField] private bool isRunning = false;

    private Rigidbody player_rb;

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
        if (isRun && currentstamina >= 0)
            isRunning = true;
            currentSpeed = defaultSpeed * 2f;
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        currentSpeed = defaultSpeed;
        isRunning = false;
    }

    private void Awake()
    {
        StaminaSlider = StaminaSlider.GetComponent<Slider>();
        TryGetComponent(out player_anim);
        currentSpeed = defaultSpeed;
        currentstamina = Maxstamina;
        TryGetComponent(out player_rb);
    }
    private void FixedUpdate()
    {
        bool hasControl = (moveDirection != Vector3.zero);
        if(hasControl)
        {
            transform.forward = moveDirection;
            if(currentstamina <= 0)
            {
                currentSpeed = defaultSpeed;
            }
            //player_rb.MovePosition(player_rb.position + moveDirection * currentSpeed * Time.deltaTime);
            player_rb.AddForce(moveDirection * currentSpeed);
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
        {
            Debug.Log("½¬ÇÁÆ® ´©¸§");
            StopCoroutine(FillStamina());
            StartCoroutine(UseStamina());
            currentSpeed = defaultSpeed * 2f;
        }
        else
        {
            Debug.Log("½¬ÇÁÆ® ¶¼Áü");
            StopCoroutine(UseStamina());
            StartCoroutine(FillStamina());
            currentSpeed = defaultSpeed;
        }
    }
    private IEnumerator UseStamina()
    {
        isRunning = true;
        while (currentstamina > 0 && isRunning)
        {
            currentstamina -= 10f * Time.deltaTime;
            StaminaSlider.value = currentstamina;
            yield return null;
        }
        if (currentstamina <= 0)
        {
            currentstamina = 0;
        }
    }
    private IEnumerator FillStamina()
    {
        isRunning = false;
        yield return new WaitForSeconds(1);
        while(currentstamina <= Maxstamina && !isRunning)
        {
            currentstamina += 5f * Time.deltaTime;
            StaminaSlider.value = currentstamina;
            yield return null;
        }
        if(currentstamina >= Maxstamina)
        {
            currentstamina = Maxstamina;
        }
    }
}
