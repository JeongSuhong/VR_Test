using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerHandAction : MonoBehaviour
{

    public GameObject collisionItem;
    public bool IsViewLaser;
    public float defaultLaserDistance;

    private LineRenderer lineRender;

    private void Awake()
    {
        lineRender = GetComponentInChildren<LineRenderer>();
    }


    private void Update()
    {
        if(IsViewLaser)
        {
            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);

            float distance;

            if(bHit && !string.Equals(hit.collider.tag, "Player"))
            {
                distance = hit.distance;

                if (string.Equals(hit.collider.tag, "UI"))
                {
                    PlayerManager.Instance.ConnectUI = hit.collider.gameObject;
                }
                else
                    PlayerManager.Instance.ConnectUI = null;

            }
            else
            {
                distance = defaultLaserDistance;
            }

            Vector3 endPos = transform.position + (transform.forward * distance);

            lineRender.SetPosition(0, transform.position);
            lineRender.SetPosition(1, endPos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItemBase>() == null)
            return;

        collisionItem = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ItemBase>() == null)
            return;

        if (collisionItem == other.gameObject)
            collisionItem = null;
    }
}
