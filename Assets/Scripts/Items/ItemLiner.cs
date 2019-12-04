using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLiner  : ItemBase
{
    public override void Grab(Transform hand)
    {
        base.Grab(hand);

        StartCoroutine(CoSketchLine());
    }

    private IEnumerator CoSketchLine()
    {
        LineRenderer line = GetComponentInChildren<LineRenderer>();
        List<Vector3> positions = new List<Vector3>();

        while (isGrab)
        {
            positions.Add(line.transform.position);
            line.positionCount = positions.Count;
            line.SetPositions(positions.ToArray());

            yield return null;
        }
    }
}
