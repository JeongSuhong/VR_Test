using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerHandAction : MonoBehaviour
{
    public SteamVR_Input_Sources InputSources;

    public GameObject collisionItem;
    public bool IsViewLaser;
    public float defaultLaserDistance;

    private LineRenderer lineRender;

    public Action<SteamVR_Input_Sources, DataDefine.HAND_INPUT_ACTION> handInputCallback;

    private void Awake()
    {
        lineRender = GetComponentInChildren<LineRenderer>();
    }


    private void Update()
    {
        //현재는 기능별로 Input Name을 정했지만 차후는 Input별로 Name 변경후 , 어느 기능이던 Input 대응되도록 수정필요
        SteamVR_Action_Boolean grabAction = SteamVR_Input.GetBooleanAction(DataDefine.HAND_INPUT_ACTION.GRAB.ToString().ToLower());

        if (grabAction.GetStateDown(InputSources))
            handInputCallback?.Invoke(InputSources, DataDefine.HAND_INPUT_ACTION.GRAB);
        else if (grabAction.GetStateUp(InputSources))
            handInputCallback?.Invoke(InputSources, DataDefine.HAND_INPUT_ACTION.NON_GRAB);

        SteamVR_Action_Boolean captureAction = SteamVR_Input.GetBooleanAction(DataDefine.HAND_INPUT_ACTION.CAPTURE.ToString().ToLower());

        if (captureAction.GetStateUp(InputSources))
            handInputCallback?.Invoke(InputSources, DataDefine.HAND_INPUT_ACTION.CAPTURE);


        // Select UI
        if (IsViewLaser)
        {
            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);

            float distance;

            if (bHit && !string.Equals(hit.collider.tag, "Player"))
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
