using UnityEngine;
using UnityEngine.UI;

public class Objeto : MonoBehaviour
{
    [SerializeField] private Image logoInventario;
    [SerializeField] private Image bordelogo;
    [SerializeField] private string descripcion;

    [SerializeField] private bool inRange;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (inRange == true && Input.GetKeyDown(KeyCode.E))
        {
            Inventory.Instance.AddObjeto(gameObject);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
