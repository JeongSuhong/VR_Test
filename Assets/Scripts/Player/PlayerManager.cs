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

    public GameObject ConnectUI;

    private PlayerHandAction[] hands;

    private ItemBase[] grabItem;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hands = GetComponentsInChildren<PlayerHandAction>();

        for (int i = 0; i < hands.Length; i++)
            hands[i].handInputCallback = HandInputAction;

        grabItem = new ItemBase[2];
    }

    private void HandInputAction(SteamVR_Input_Sources inputSources, DataDefine.HAND_INPUT_ACTION actionType)
    {
        int targetHandIndex = inputSources == SteamVR_Input_Sources.LeftHand ? 0 : 1;

        switch (actionType)
        {
            case DataDefine.HAND_INPUT_ACTION.GRAB:
                if (ConnectUI != null && hands[targetHandIndex].IsViewLaser)
                    ConnectUI.GetComponent<Button>().onClick.Invoke();
                else if (hands[targetHandIndex].collisionItem != null)
                {
                    grabItem[targetHandIndex] = hands[targetHandIndex].collisionItem.GetComponent<ItemBase>();
                    grabItem[targetHandIndex].Grab(hands[targetHandIndex].gameObject.transform);
                }
                break;

            case DataDefine.HAND_INPUT_ACTION.NON_GRAB:
                if (grabItem[targetHandIndex] != null)
                {
                    SteamVR_Action_Pose parnet = SteamVR_Input.GetPoseAction("pose");
                    grabItem[targetHandIndex].NonGrab(parnet.GetVelocity(inputSources), parnet.GetAngularVelocity(inputSources));
                    grabItem[targetHandIndex] = null;
                }
                break;

            case DataDefine.HAND_INPUT_ACTION.CAPTURE:
                if (grabItem[targetHandIndex] != null)
                    grabItem[targetHandIndex].GetComponent<ItemCamera>().Capture();
                break;
        }
    }
}
