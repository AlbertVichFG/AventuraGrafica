using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] private List<GameObject> objetos;

    private void Start()
    {
        //objetos = GameManager.instance.GetGameData.objetos;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddObjeto(GameObject obj)
    {
        if (!objetos.Contains(obj))
        {
            objetos.Add(obj);
            obj.SetActive(false);
        }        
    }

    public void RemoveObjeto(GameObject obj)
    {
        if (objetos.Contains(obj))
            objetos.Remove(obj);
    }

    public bool TieneObjeto(GameObject obj)
    {
        return objetos.Contains(obj);
    }
}
