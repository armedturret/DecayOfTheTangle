using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 1f;
    public float friction = 5f;
    public float moveAcceleration = 0.1f;

    [Header("References")]
    [SerializeField]
    private Transform groundedCheck;
    [SerializeField]
    private LayerMask groundLayers;

    private Rigidbody2D _rb2d;
    private float _horizontalMovement;
    private bool _jump = false;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        _horizontalMovement = value.Get<Vector2>().x;
    }

    public void OnJump(InputValue value)
    {
        if(IsGrounded())
            _jump = true;
    }

    private void FixedUpdate()
    {
        //handle player movement
        Vector2 currentVelocity = _rb2d.velocity;

        //add horizontal movement
        if (Mathf.Abs(_horizontalMovement) > 0.1f)
        {
            currentVelocity.x += _horizontalMovement * moveAcceleration * Time.fixedDeltaTime;
        }
        else
        {
            float delta = friction * Time.fixedDeltaTime;
            currentVelocity.x = currentVelocity.normalized.x * Mathf.Min(0f, Mathf.Abs(currentVelocity.x) - delta);
        }
        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -moveSpeed, moveSpeed);

        //add jump velocity
        if (_jump)
        {
            currentVelocity.y = jumpForce;
            _jump = false;
        }

        _rb2d.velocity = currentVelocity;
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundedCheck.position, 0.15f, groundLayers);
    }
}
