using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hazard : MonoBehaviour
{
    //player layers
    public LayerMask playerLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Utils.InLayerMask(playerLayer, collision.gameObject.layer))
        {
            //touched player (MURDER)
            collision.gameObject.GetComponent<PlayerController>().Kill();
        }
    }
}