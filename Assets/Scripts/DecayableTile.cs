using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DecayableTile : Decayable
{
    private Rigidbody2D _rb2d;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDecayed()
    {
        //make this object dynamic
        _rb2d.bodyType = RigidbodyType2D.Dynamic;

        //calculate the color interpolating with decay
        _spriteRenderer.color = new Color(1f - _decay, 1f - _decay, 1f - _decay);

        //delete if completely decayed
        if (_decay >= 1f)
            Destroy(gameObject);
    }
}