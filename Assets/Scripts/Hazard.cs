using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hazard : MonoBehaviour
{
    //player layers
    public LayerMask playerLayer;

    private PlayerController _player;

    private void Update()
    {
        if (_player != null)
            _player.Kill();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Utils.InLayerMask(playerLayer, collision.gameObject.layer))
        {
            _player = collision.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (Utils.InLayerMask(playerLayer, collision.gameObject.layer))
        {
            _player = null;
        }
    }
}