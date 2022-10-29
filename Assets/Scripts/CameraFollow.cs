using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //attach this script to the player to get cam to follow it
    public enum FOLLOW_TYPE { Static, Horizonal, Vertical, Both};

    public FOLLOW_TYPE followType = FOLLOW_TYPE.Static;


    private void Update()
    {
        Vector3 targetPos = Camera.main.transform.position;
        switch (followType)
        {
            case FOLLOW_TYPE.Static:
                return;
            case FOLLOW_TYPE.Horizonal:
                targetPos.x = transform.position.x;
                break;
            case FOLLOW_TYPE.Vertical:
                targetPos.y = transform.position.y;
                break;
            case FOLLOW_TYPE.Both:
                targetPos.x = transform.position.x;
                targetPos.y = transform.position.y;
                break;
        }

        Camera.main.transform.position = targetPos;
    }
}