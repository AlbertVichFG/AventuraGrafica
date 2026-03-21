using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public Slider volumenSlider;
    public TMP_Dropdown resolucionDropdown;
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
            resolucionDropdown.options.Add(new TMP_Dropdown.OptionData(opcion));

            if (resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }

        resolucionDropdown.value = PlayerPrefs.GetInt("Resolucion", resolucionActual);
        resolucionDropdown.RefreshShownValue();

        volumenSlider.value = PlayerPrefs.GetFloat("Volumen", 1f);
        AudioListener.volume = volumenSlider.value;

        pantallaCompletaToggle.isOn = PlayerPrefs.GetInt("PantallaCompleta", 1) == 1;
        Screen.fullScreen = pantallaCompletaToggle.isOn;
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

    public void GuardarAjustes()
    {
        PlayerPrefs.SetFloat("Volumen", volumenSlider.value);
        PlayerPrefs.SetInt("Resolucion", resolucionDropdown.value);
        PlayerPrefs.SetInt("PantallaCompleta", pantallaCompletaToggle.isOn ? 1 : 0);

        PlayerPrefs.Save();

        Debug.Log("Ajustes guardados");
    }
}