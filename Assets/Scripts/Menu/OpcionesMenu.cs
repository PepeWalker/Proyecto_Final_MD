using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

using UnityEngine.UI;

public class OpcionesMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown qualityDropDown;
    public TMP_Dropdown resolutionDropDown;
    Resolution[] resolutions;

    private void Start()
    {
        //Establecer la calidad del juego por defecto a hight, no hay cambios configuramos mas que los por defecto
        qualityDropDown.value = 4;
        qualityDropDown.RefreshShownValue();


        resolutions = Screen.resolutions;

        //Limpiamos la lista de resoluciones
        resolutionDropDown.ClearOptions();

        //Generamos lista de string con las resoluciones y la rellenamos con las resoluciones que nos da unity
        // estaria bien poder decirle que resoluciones queremos aportar, para que no genere lista tan grande.
        List<string> opciones = new List<string>();

        int resolucionActualIndex = 0;

        //bucle para rellenar el array de resoluciones
        for (int i = 0; i < resolutions.Length; i++)
        {
            // formato para uqe se vea bonito
            string opcion = resolutions[i].width + " x " + resolutions[i].height;
            opciones.Add(opcion);

            //Miramos si la resolucion del a pantalla donde se ejecuta 
            // y establecemos esa resolucion como predeterminada
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolucionActualIndex = i;
            }
        }

        //Metemos la lista de resoluciones
        resolutionDropDown.AddOptions(opciones);

        //establecemos la resolucion de la pantalla como valor seleccionado por defecto
        resolutionDropDown.value = resolucionActualIndex;
        //refrescmos el boton para que muestre la resolucion seleccionada
        resolutionDropDown.RefreshShownValue();
    }

    public void SetSFXVolumen(float sfxVolumen )
    {
        audioMixer.SetFloat("SFXVolumen", sfxVolumen) ;

    }
    public void SetMusicVolumen(float musicVolumen)
    {
        audioMixer.SetFloat("MusicVolumen", musicVolumen);

    }
    public void SetGeneralVolumen(float generalVolumen)
    {
        audioMixer.SetFloat("GeneralVolumen", generalVolumen);

    }


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height, Screen.fullScreen );
    }


}
