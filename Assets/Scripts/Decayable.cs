using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Decayable : MonoBehaviour
{
    public LayerMask decayableLayer;
    public float decayRate = 0.25f;
    public float decayThreshold = 0.5f;
    public float decaySpreadRadius = 1f;

    protected float _decay = 0f;
    public bool _decaying = false;

    public void StartDecay()
    {
        _decaying = true;
    }

    public void AddDecay(float amount)
    {
        _decay += amount;
    }

    protected virtual void Update()
    {
        //start decay tick
        if (_decaying)
        {
            _decay += Time.deltaTime * decayRate;
            _decay = Mathf.Clamp(_decay, 0f, 1f);
        }

        //check if past threshold (perform OnDecayed and check for infection)
        if(_decay >= decayThreshold)
        {
            OnDecayed();

            //decay spread check
            Collider2D[] detected = Physics2D.OverlapCircleAll(transform.position, decaySpreadRadius, decayableLayer);
            foreach(var other in detected)
            {
                other.gameObject.GetComponent<Decayable>().StartDecay();
            }
        }
    }

    protected virtual void OnDecayed()
    {

    }
}