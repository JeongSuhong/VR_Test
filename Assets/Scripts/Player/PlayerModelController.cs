using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelController : MonoBehaviour
{
    public Transform HeadCamera;
    public Transform HeadModel;


    private void Update()
    {
        Vector3 rot = HeadModel.rotation.eulerAngles;
        rot += HeadCamera.rotation.eulerAngles * 0.1f;

        HeadModel.rotation = Quaternion.Euler(rot);
    }
}
