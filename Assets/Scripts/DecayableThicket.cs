using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Hazard))]
[RequireComponent(typeof(SpriteRenderer))]
public class DecayableThicket : Decayable
{
    public float hazardThreshold = 0.75f;
    [SerializeField]
    private SpriteRenderer backRenderer;
    [SerializeField]
    private AudioSource thornSound;

    private SpriteRenderer _spriteRenderer;
    private Hazard _hazard;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _hazard = GetComponent<Hazard>();
        _hazard.enabled = false;
    }

    protected override void Update()
    {
        base.Update();

        //calculate the color interpolating with decay
        _spriteRenderer.color = new Color(1f, 1f, 1f, 1f - _decay);
        backRenderer.color = new Color(1f, 1f, 1f, _decay);

        //enable hazard and destroy connections
        if (_decay >= hazardThreshold && !_hazard.enabled)
        {
            //enable the hazard
            _hazard.enabled = true;

            //play spike sound
            thornSound.Play();

            //break connections
            if(GetComponent<FixedJoint2D>())
                GetComponent<FixedJoint2D>().enabled = false;
        }
    }
}