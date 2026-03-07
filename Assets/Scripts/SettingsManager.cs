using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider volumenSlider;
    public Dropdown resolucionDropdown;
    public Toggle pantallaCompletaToggle;

    Resolution[] resoluciones;

    void Start()
    {
        resoluciones = Screen.resolutions;
        resolucionDropdown.ClearOptions();

        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            resolucionDropdown.options.Add(new Dropdown.OptionData(opcion));

            if (resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }

        resolucionDropdown.value = resolucionActual;
        resolucionDropdown.RefreshShownValue();

        pantallaCompletaToggle.isOn = Screen.fullScreen;
    }

    public void CambiarResolucion(int index)
    {
        Resolution resolucion = resoluciones[index];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void CambiarVolumen(float volumen)
    {
        AudioListener.volume = volumen;
    }
}