using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FollowObj : MonoBehaviour
{
    public Transform FollowTarget;

    private Vector3 originPos;

    private void Awake()
    {
        originPos = transform.localPosition;
    }

    private void Update()
    {
        if (FollowTarget != null)
        {
            transform.position = FollowTarget.position;
            transform.localPosition += originPos;
            transform.position -= new Vector3(0, 0, 0.01f);
            transform.rotation = FollowTarget.rotation;
        }
    }
}
