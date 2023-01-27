using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    Resolution[] resolutions;

    public TMPro.TMP_Dropdown resolutionDropdown;

    public Toggle vSyncTog;

    void Start() {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex  = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
           string option = resolutions[i].width + " x " + resolutions[i].height;
           options.Add(option);

           if (resolutions[i].width == Screen.currentResolution.width  && resolutions[i].height == Screen.currentResolution.height)
           {
            currentResolutionIndex = i;
           }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        if (QualitySettings.vSyncCount == 0)
        {
            vSyncTog.isOn = false;
        }else{
            vSyncTog.isOn = true;
        }
    }

    public void setResolution (int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    [SerializeField] private AudioMixer aMixer;

    public void changeValue(Slider slider){
        switch (slider.value)
        {
            case 0:
                aMixer.SetFloat("Music", -88);
                break;
            case 1:
                aMixer.SetFloat("Music", -40);
                break;
            case 2:
                aMixer.SetFloat("Music", -20);
                break;
            case 3:
                aMixer.SetFloat("Music", -10);
                break;
            case 4:
                aMixer.SetFloat("Music", 0);
                break;
            case 5:
                aMixer.SetFloat("Music", 10);
                break;
        }
    }

    public void changeValueEffects(Slider slider){
        switch (slider.value)
        {
            case 0:
                aMixer.SetFloat("Effects", -88);
                break;
            case 1:
                aMixer.SetFloat("Effects", -40);
                break;
            case 2:
                aMixer.SetFloat("Effects", -20);
                break;
            case 3:
                aMixer.SetFloat("Effects", -10);
                break;
            case 4:
                aMixer.SetFloat("Effects", 0);
                break;
            case 5:
                aMixer.SetFloat("Effects", 10);
                break;
        }
    }

    public void ApplyGraphics(){
        if (vSyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }else{
            QualitySettings.vSyncCount = 0;
        }
    }

    public void setQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }
}
