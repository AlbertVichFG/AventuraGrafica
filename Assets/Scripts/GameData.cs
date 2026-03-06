using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{

    public List<Objeto> objetos = new List<Objeto>();



    public bool doorUnlocked;
    public bool talkedToGuard;

    [SerializeField]
    private int saveScene;



    public int SaveScene
    {
        get { return saveScene; }
        set { saveScene = value; }
    }
}
