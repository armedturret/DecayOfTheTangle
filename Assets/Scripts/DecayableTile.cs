using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DecayableTile : Decayable
{
    public float physicsThreshold = 0.75f;

    [SerializeField]
    private AudioSource fallAudio;

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

        //calculate the color interpolating with decay
        _spriteRenderer.color = new Color(1f, 1f, 1f, 1f - _decay);

        //delete if completely decayed
        if (_decay >= 1f)
            Destroy(gameObject);

        //enable phsyics and destroy connections
        if(_decay >= physicsThreshold && _rb2d.bodyType != RigidbodyType2D.Dynamic)
        {
            //make this object dynamic
            _rb2d.bodyType = RigidbodyType2D.Dynamic;

            //fall audio
            fallAudio.Play();

            //break connections
            if(GetComponent<FixedJoint2D>())
                GetComponent<FixedJoint2D>().enabled = false;
        }
    }
}