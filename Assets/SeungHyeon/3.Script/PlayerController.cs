using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveDirection;

    public float defaultSpeed = 20f;

    [SerializeField] private Slider StaminaSlider;
    [SerializeField] private GameObject GameOver_UI;

    [SerializeField] public float currentSpeed;
    [SerializeField] private float Maxstamina = 100f;
    [SerializeField] private float currentstamina;
    [SerializeField] private bool isRunning = false;
    [SerializeField] public bool isDie = false;

    private Rigidbody player_rb;

    private Animator player_anim;

    private TileAction Tiletype;
    private RaycastHit hit;

    public InputAction move;
    public InputAction run;

    private void OnEnable()
	{
		move.performed += OnMovePerformed;
        move.canceled += OnMoveCanceled;
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
        move.canceled -= OnMoveCanceled;
        run.performed -= OnRunPerformed;
		run.canceled -= OnRunCanceled;
    }

	private void OnMovePerformed(InputAction.CallbackContext context)
	{
		var v = context.ReadValue<Vector2>();
		moveDirection = new Vector3(v.x, 0f, v.y);
	}

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveDirection = Vector3.zero;
    }

    private void OnRunPerformed(InputAction.CallbackContext context)
	{
		var isRun = context.ReadValueAsButton();
		if (isRun && currentstamina >= 0)
			isRunning = true;
		currentSpeed = defaultSpeed * 2f;

        Run(isRun);
    }

	private void OnRunCanceled(InputAction.CallbackContext context)
	{
		currentSpeed = defaultSpeed;
		isRunning = false;
	}

    public void TriggerVibration(Explosion explosion)
	{
        if (currentVibration != null)
            StopCoroutine(currentVibration);

        currentVibration = Vibrate(explosion);
        StartCoroutine(currentVibration);
	}

    private IEnumerator currentVibration;
    private IEnumerator Vibrate(Explosion explosion)
	{
        float elapsedTime = 0f;
		while (elapsedTime < explosion.Duration)
		{
            elapsedTime += Time.deltaTime;
            float intensity = explosion.IntensityCurve.Evaluate(elapsedTime / explosion.Duration);
            Gamepad.current.SetMotorSpeeds(intensity, intensity);
            yield return null;
        }
        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }

    private void Awake()
    {
        isDie = false;
        StaminaSlider = GameObject.FindObjectOfType<SliderControl>().GetComponent<Slider>();
        TryGetComponent(out player_anim);
        currentSpeed = defaultSpeed;
        currentstamina = Maxstamina;
        TryGetComponent(out player_rb);
    }
    private void Start()
    {
        GameOver_UI = GameObject.FindObjectOfType<Canvas>().transform.GetChild(4).gameObject;
    }
    private void FixedUpdate()
    {
        if (isDie)
            return;

		//if(Input.GetKeyDown(KeyCode.H))
		//{
		//    Die();
		//}
		//Debug.DrawRay(transform.position, Vector3.down, new Color(0, 1, 0));
		//if (Physics.Raycast(transform.position, Vector3.down, out hit))
		//{
		//    Tiletype = hit.transform.GetComponent<TileAction>();
		//    Debug.Log(Tiletype.currentTile);
		//}

        bool hasControl = (moveDirection != Vector3.zero);
        if (hasControl)
        {
            transform.forward = moveDirection;
            if (currentstamina <= 0)
            {
                currentSpeed = defaultSpeed;
            }
            player_rb.AddForce(moveDirection.normalized * currentSpeed, ForceMode.Force);
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
        Run(value.isPressed);
    }

    private void Run(bool flag)
	{
        if (flag)
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
    public void Die()
    {
        isDie = true;
        GameOver_UI.SetActive(true);
    }
}
