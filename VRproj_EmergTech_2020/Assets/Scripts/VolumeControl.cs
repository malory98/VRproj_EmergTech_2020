using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    // (PT-BR) TEM QUE ESCREVER OS NOMES DAS VARIAVEIS DOS PARAMETROS EXPOSTOS EXAMATEMENTE COMO ELAS SE CHAMAM
    // (EN) YOU HAVE TO WRITE THE EXPOSED-PARAMETERS VARIABLES' NAMES EXACTLY AS THEY ARE CALLED

    public AudioMixer masterMixer;

    public void SetMasterLv(float volMaster)
    {
        masterMixer.SetFloat("volMaster", volMaster);
    }

    public void SetSfxLv(float volSFXs)
    {
        masterMixer.SetFloat("volSFXs", volSFXs);
    }

    public void SetMusicLv(float volAmbience)
    {
        masterMixer.SetFloat("volAmbience", volAmbience);
    }

}
