using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private static WorldManager instance;
    public static WorldManager Instance
    {
        get
        {
            if (instance == null)
                instance = new WorldManager();

            return instance;
        }
    }

    public GameObject[] PlayObjects;
    private int playIndex;

    private void Awake()
    {
        instance = this;
    }

    public void OnClickPlayObject(int index)
    {
        PlayObjects[playIndex].SetActive(false);

        PlayObjects[index].SetActive(true);
        playIndex = index;

    }



}
