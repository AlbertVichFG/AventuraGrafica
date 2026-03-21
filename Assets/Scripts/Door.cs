using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [Header("Requisito")]
    [Tooltip("El objeto f�sico de la escena que el jugador debe tener.")]
    [SerializeField] 
    private GameObject objetoRequerido;
    [SerializeField] 
    private bool consumirAlUsar = true;

    [Header("Configuraci�n de Tecla")]
    [SerializeField] 
    private KeyCode teclaInteraccion = KeyCode.E;

    [Header("Eventos")]
    public UnityEvent alActivar;
    public UnityEvent alFallar;

    [SerializeField] 
    private bool jugadorEstaCerca = false;

    [SerializeField] 
    private GameObject bloqueo;

    private void Update()
    {
        // El jugador est� dentro del Trigger
        if (jugadorEstaCerca && Input.GetKeyDown(teclaInteraccion))
        {
            EjecutarAccion();
        }
    }

    private void EjecutarAccion()
    {
        // Usamos el Singleton del inventario para buscar el objeto exacto
        if (Inventory.Instance.TieneObjeto(objetoRequerido))
        {
            if (consumirAlUsar)
            {
                bloqueo.SetActive(false);
                Inventory.Instance.RemoveObjeto(objetoRequerido);
            }

            alActivar.Invoke();
            Debug.Log("Acci�n realizada con �xito.");
        }
        else
        {
            alFallar.Invoke();
            Debug.Log("No tienes el objeto requerido en el inventario.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEstaCerca = true;
            Debug.Log("Presiona E para usar el objeto.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEstaCerca = false;
        }
    }
}