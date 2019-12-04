using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
                instance = new PlayerManager();

            return instance;
        }
    }

    public PlayerHandAction leftHand;
    public PlayerHandAction rightHand;

    public GameObject ConnectUI;

    private ItemBase grabItem;



    public int isGrab;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        isGrab = 0;
        SteamVR_Action_Boolean grabAction = SteamVR_Input.GetBooleanAction("grab");

        if (grabAction.GetState(SteamVR_Input_Sources.LeftHand))
            isGrab = 1;
        else if (grabAction.GetState(SteamVR_Input_Sources.RightHand))
            isGrab = 2;

        if (isGrab != 0 && grabItem == null)
        {
            if (isGrab == 1)
            {
                if (leftHand.collisionItem != null)
                {
                    if (grabItem != null)
                        grabItem.NonGrab(Vector3.zero, Vector3.zero);

                    grabItem = leftHand.collisionItem.GetComponent<ItemBase>();
                    grabItem.Grab(leftHand.transform);
                }
                else if (ConnectUI != null)
                    ConnectUI.GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                if (rightHand.collisionItem != null)
                {
                    if (grabItem != null)
                        grabItem.NonGrab(Vector3.zero, Vector3.zero);

                    grabItem = rightHand.collisionItem.GetComponent<ItemBase>();
                    grabItem.Grab(rightHand.transform);
                }
                else if (ConnectUI != null)
                    ConnectUI.GetComponent<Button>().onClick.Invoke();
            }
        }
        else if (isGrab == 0 && grabItem != null)
        {
            SteamVR_Action_Pose parnet = SteamVR_Input.GetPoseAction("pose");
            grabItem.NonGrab(parnet.GetVelocity(SteamVR_Input_Sources.RightHand), parnet.GetAngularVelocity(SteamVR_Input_Sources.RightHand));
            grabItem = null;
        }

        if (grabItem != null && grabItem.GetComponent<ItemCamera>() != null)
        {
            SteamVR_Action_Boolean captureAction = SteamVR_Input.GetBooleanAction("capture");

            if (captureAction.GetStateDown(SteamVR_Input_Sources.LeftHand) || captureAction.GetStateDown(SteamVR_Input_Sources.RightHand))
            {
                grabItem.GetComponent<ItemCamera>().Capture();
            }
        }
    }
}
