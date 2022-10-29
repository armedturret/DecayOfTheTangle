using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Decayable : MonoBehaviour
{
    public LayerMask decayableLayer;
    public float decayRate = 0.25f;
    public float decaySpreadThreshold = 0.5f;
    public float decaySpreadRadius = 1f;
    public float randomDecayRange = 0.2f;
    protected float _decay = 0f;
    private bool _decaying = false;

    public void StartDecay()
    {
        if (_decaying) return;

        _decaying = true;
        //give a random start decay to look nice
        _decay = Random.Range(0f, randomDecayRange);
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
        if(_decay >= decaySpreadThreshold)
        {
            //decay spread check
            Collider2D[] detected = Physics2D.OverlapCircleAll(transform.position, decaySpreadRadius, decayableLayer);
            foreach(var other in detected)
            {
                other.gameObject.GetComponent<Decayable>().StartDecay();
            }
        }
    }
}