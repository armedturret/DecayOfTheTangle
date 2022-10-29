using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DecayableShooter : Decayable
{
    public float firingThreshold = 0.75f;
    public float firingCooldown = 0.5f;
    public float projectileSpeed = 1f;
    [SerializeField]
    private GameObject spikePrefab;

    private Rigidbody2D _rb2d;
    private SpriteRenderer _spriteRenderer;
    private float _projectileCooldown;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb2d = GetComponent<Rigidbody2D>();
        _projectileCooldown = firingCooldown;
    }

    protected override void Update()
    {
        base.Update();

        //calculate the color interpolating with decay
        _spriteRenderer.color = new Color((1f - _decay), 1f, (1f - _decay));

        //enable firing of projectiles
        if(_decay >= firingThreshold)
        {
            _projectileCooldown -= Time.deltaTime;

            if(_projectileCooldown <= 0f)
            {
                GameObject spike = Instantiate(spikePrefab);
                spike.transform.position = transform.position;
                spike.GetComponent<SpikeProjectile>().Fire(transform.up * projectileSpeed);
                _projectileCooldown = firingCooldown;
            }
        }
    }
}