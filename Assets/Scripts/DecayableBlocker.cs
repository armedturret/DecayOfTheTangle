using System.Collections;

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DecayableBlocker : Decayable
{
    public float firingThreshold = 0.75f;
    public float projectileSpeed = 20f;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private LayerMask checkLayers;
    [SerializeField]
    private GameObject blockerPrefab;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private SpriteRenderer turretRenderer;
    [SerializeField]
    private Sprite corruptedTurret;
    [SerializeField]
    private AudioSource fireSound;

    private SpriteRenderer _spriteRenderer;
    private GameObject _blocker;
    private bool _fired;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        //calculate the color interpolating with decay
        _spriteRenderer.color = new Color(1f, 1f, 1f, 1f - _decay);

        //check if the player hit the blocker wall
        if(_blocker != null)
        {
            var hit = Physics2D.Raycast(transform.position, _blocker.transform.position - transform.position, Vector2.Distance(transform.position, _blocker.transform.position), checkLayers);
            if(hit && Utils.InLayerMask(playerLayer, hit.collider.gameObject.layer))
            {
                //hit the player
                hit.collider.gameObject.GetComponent<PlayerController>().Kill();
            }
        }

        //draw line renderer to connect to the projectile
        if (_blocker != null)
            lineRenderer.SetPosition(1, _blocker.transform.localPosition);

        //fire the blocker
        if (_decay >= firingThreshold && !_fired)
        {
            _fired = true;

            fireSound.Play();

            turretRenderer.sprite = corruptedTurret;

            _blocker = Instantiate(blockerPrefab, transform);
            _blocker.transform.position = transform.position;
            _blocker.transform.rotation = transform.rotation;
            _blocker.GetComponent<BlockerProjectile>().Fire(transform.up * projectileSpeed);
        }
    }
}