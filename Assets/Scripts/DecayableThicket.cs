using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Hazard))]
[RequireComponent(typeof(SpriteRenderer))]
public class DecayableThicket : Decayable
{
    public float hazardThreshold = 0.75f;

    private Rigidbody2D _rb2d;
    private SpriteRenderer _spriteRenderer;
    private Hazard _hazard;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb2d = GetComponent<Rigidbody2D>();
        _hazard = GetComponent<Hazard>();
        _hazard.enabled = false;
    }

    protected override void Update()
    {
        base.Update();

        //calculate the color interpolating with decay
        _spriteRenderer.color = new Color(1f, (1f - _decay), (1f - _decay));

        //enable hazard and destroy connections
        if(_decay >= hazardThreshold)
        {
            //enable the hazard
            _hazard.enabled = true;

            //break connections
            if(GetComponent<FixedJoint2D>())
                GetComponent<FixedJoint2D>().enabled = false;
        }
    }
}