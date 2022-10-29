using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Weapon")]
    public float weaponCooldown = 2f;
    public float firingVelocity = 20f;

    [Header("Movement")]
    public float jumpForce = 10f;
    public float jumpDampen = 2f;
    public float moveSpeed = 1f;
    public float friction = 5f;
    public float moveAcceleration = 0.1f;

    [Header("References")]
    [SerializeField]
    private Transform groundedCheck;
    [SerializeField]
    private LayerMask groundLayers;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private GameObject gunObject;
    [SerializeField]
    private GameObject headObject;
    [SerializeField]
    private Animator bodyAnimator;

    private Rigidbody2D _rb2d;
    private float _horizontalMovement;
    private float _cooldownPeriod;
    private Vector2 _cursorPosition;
    private bool _jump = false;
    private bool _jumpReleased = false;
    private bool _fire = false;

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
        if (IsGrounded() && value.Get<float>() > 0f)
            _jump = true;
        else if (value.Get<float>() <= 0f)
            _jumpReleased = true;
    }

    public void OnFire(InputValue value)
    {
        if(_cooldownPeriod <= 0f)
            _fire = true;
    }

    public void OnAim(InputValue value)
    {
        _cursorPosition = value.Get<Vector2>();
    }

    public void Kill()
    {
        GameManager.instance.ReloadLevel();
    }

    private void Update()
    {
        //handle aiming
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 aimVec = _cursorPosition - new Vector2(playerPos.x, playerPos.y);

        //show weapon aiming
        float targetZ = Vector2.SignedAngle(Vector2.right, aimVec);

        //flip gun if Abs > 90
        gunObject.GetComponent<SpriteRenderer>().flipY = Mathf.Abs(targetZ) > 90f;
        headObject.GetComponent<SpriteRenderer>().flipX = Mathf.Abs(targetZ) > 90f;

        gunObject.transform.eulerAngles = new Vector3(0f, 0f, targetZ);

        if (_cooldownPeriod > 0f)
            _cooldownPeriod -= Time.deltaTime;

        //handle firing
        if (_fire)
        {
            _fire = false;
            _cooldownPeriod = weaponCooldown;
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.position;
            projectile.GetComponent<DecayProjectile>().Fire(aimVec.normalized * firingVelocity);
        }

        //handle goofy animations
        bodyAnimator.SetBool("Mid Air", !IsGrounded());
        bodyAnimator.SetBool("Walking", Mathf.Abs(_rb2d.velocity.x) > 0.5f);
        if(_rb2d.velocity.x < 0f)
        {
            bodyAnimator.GetComponent<SpriteRenderer>().flipX = true;
        }else if(_rb2d.velocity.x > 0f)
        {
            bodyAnimator.GetComponent<SpriteRenderer>().flipX = false;
        }

        //handle player movement
        Vector2 currentVelocity = _rb2d.velocity;

        //add horizontal movement
        if (Mathf.Abs(_horizontalMovement) > 0.1f)
        {
            currentVelocity.x += _horizontalMovement * moveAcceleration * Time.deltaTime;
        }
        else if (Mathf.Abs(currentVelocity.x) > 0.1f)
        {
            float delta = friction * Time.deltaTime;
            currentVelocity.x = currentVelocity.normalized.x * Mathf.Max(0f, Mathf.Abs(currentVelocity.x) - delta);
        }
        else
        {
            currentVelocity.x = 0f;
        }

        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -moveSpeed, moveSpeed);

        //add jump velocity
        if (_jump)
        {
            currentVelocity.y = jumpForce;
            _jump = false;
        }

        //add dampen if jump released
        if (_jumpReleased)
        {
            _jumpReleased = false;
            if (currentVelocity.y > 0f)
                currentVelocity.y /= jumpDampen;
        }

        _rb2d.velocity = currentVelocity;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundedCheck.position, 0.15f, groundLayers);
    }
}
