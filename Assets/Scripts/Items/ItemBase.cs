using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    protected Transform parent;

    protected bool isGrab;

    protected void Awake()
    {
        parent = transform.parent;
    }

    public virtual void Grab(Transform hand)
    {
        transform.parent = hand;

        Rigidbody rig = GetComponent<Rigidbody>();

        if (rig != null)
        {
            rig.useGravity = false;
            rig.constraints = RigidbodyConstraints.FreezeAll;
        }

        isGrab = true;
    }

    public virtual void NonGrab(Vector3 velocity, Vector3 angularVelocity)
    {
        isGrab = false;

        transform.parent = parent;

        Rigidbody rig = GetComponent<Rigidbody>();

        if (rig != null)
        {
            rig.constraints = RigidbodyConstraints.None;
            rig.useGravity = true;
            rig.velocity = velocity;
            rig.angularVelocity = angularVelocity;

        }
    }

}
