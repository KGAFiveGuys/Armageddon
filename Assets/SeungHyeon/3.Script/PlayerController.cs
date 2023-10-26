using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveDirection;
    [SerializeField] private float moveSpeed = 4f;
    private void Update()
    {
        bool hasControl = (moveDirection != Vector3.zero);
        if(hasControl)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        if(input != null)
        {
            moveDirection = new Vector3(input.x, 0f, input.y);
            Debug.Log($"Sand_MESSAGE : {input.magnitude}");
        }
    }
}
