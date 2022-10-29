using System.Collections;
using UnityEngine;

public class BlockerProjectile : MonoBehaviour
{
    public float initialDisable = 0.1f;
    [SerializeField]
    private LayerMask playerLayer;

    private Rigidbody2D _rb2d;
    private Collider2D _collider;
    private float _timeAlive = 0f;
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    public void Fire(Vector2 initialVelocity)
    {
        _rb2d.velocity = initialVelocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if hit player then remain static at position
        if (Utils.InLayerMask(playerLayer, collision.gameObject.layer))
        {
            collision.gameObject.GetComponent<PlayerController>().Kill();
        }
        _rb2d.velocity = Vector2.zero;
        _rb2d.bodyType = RigidbodyType2D.Static;
    }

    private void Update()
    {
        _timeAlive += Time.deltaTime;
        _collider.enabled = _timeAlive > initialDisable;
    }
}