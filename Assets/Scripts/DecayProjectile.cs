using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class DecayProjectile : MonoBehaviour
{
    public float explosionRadius = 2f;
    public float lifetime = 5f;
    public float decayIncrease = 0.2f;
    [SerializeField]
    private LayerMask decayableLayer;

    private Rigidbody2D _rb2d;
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 initialVelocity)
    {
        _rb2d.velocity = initialVelocity;
    }

    private void Explode()
    {
        //decay spread check
        Collider2D[] detected = Physics2D.OverlapCircleAll(transform.position, explosionRadius, decayableLayer);
        foreach (var other in detected)
        {
            other.gameObject.GetComponent<Decayable>().StartDecay();
            other.gameObject.GetComponent<Decayable>().AddDecay(decayIncrease);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //explode (KABLOOEY)
        Explode();
        
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
            Explode();
    }
}