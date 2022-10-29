using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SpikeProjectile : MonoBehaviour
{

    public float lifetime = 5f;
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
    }

    public void Fire(Vector2 initialVelocity)
    {
        _rb2d.velocity = initialVelocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if hit player then destroy self
        if (Utils.InLayerMask(playerLayer, collision.gameObject.layer))
        {
            collision.gameObject.GetComponent<PlayerController>().Kill();
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        _timeAlive += Time.deltaTime;
        _collider.enabled = _timeAlive > initialDisable;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
            Destroy(gameObject);
    }
}