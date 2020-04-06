using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetMasterLv(float volMaster)
    {
        masterMixer.SetFloat("volMaster", volMaster);
    }

    public void SetSfxLv(float volSFXs)
    {
        masterMixer.SetFloat("volSFXs", volSFXs);
    }

    public void SetAmbienceLv(float volAmbience)
    {
        masterMixer.SetFloat("volAmbience", volAmbience);
    }

}
