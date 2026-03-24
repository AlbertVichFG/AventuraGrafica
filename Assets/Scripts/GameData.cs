using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public string lastSaveDate;
    public float totalPlayTime;
    public string sceneName;

    [SerializeField]
    private int saveScene;

    public int SaveScene
    {
        get { return saveScene; }
        set { saveScene = value; }
    }

    public float posX, posY, posZ;

    public bool doorUnlocked;
    public bool talkedToGuard;

    public List<string> inventoryItems = new List<string>();
    public List<string> pickedUpItems = new List<string>();

    public GameData()
    {
        lastSaveDate = "";
        totalPlayTime = 0f;
        sceneName = "";
        saveScene = 1;
        posX = posY = posZ = 0f;
        doorUnlocked = false;
        talkedToGuard = false;
        inventoryItems = new List<string>();
        pickedUpItems = new List<string>();
    }
}
