using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{

    public List<GameObject> objetos = new List<GameObject>();

    [SerializeField]
    private float playerLife;
    [SerializeField]
    private float playerMaxLife;
    [SerializeField]
    private int saveScene;

    public float PlayerLife
    {
        get { return playerLife; }
        set { playerLife = value; }
    }

    public float PlayerMaxLife
    {
        get { return playerMaxLife; }
        set { playerMaxLife = value; }
    }

    public int SaveScene
    {
        get { return saveScene; }
        set { saveScene = value; }
    }
}
