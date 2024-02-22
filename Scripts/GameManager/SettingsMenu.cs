using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider EffectSlider;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] TMP_Dropdown resolution;
    [SerializeField] TMP_Dropdown Graphics;


    List<Resolution> resolutions = new ();

    private void Awake()
    {
        Resolution[] resolutionsTemp = Screen.resolutions;
        List<string> resolutionNames = new();
        resolution.ClearOptions();

        for (int i = 0; i < resolutionsTemp.Length; i++) 
        {
            string option = resolutionsTemp[i].width + "x" + resolutionsTemp[i].height;

            try 
            {
                if (resolutionsTemp[i].width != resolutionsTemp[i + 1].width || resolutionsTemp[i].height != resolutionsTemp[i + 1].height)
                {
                    resolutionNames.Add(option);
                    resolutions.Add(resolutionsTemp[i]);
                }
            }
            catch 
            {
                resolutionNames.Add(option);
            }


            if (Screen.currentResolution.width == resolutionsTemp[i].width && Screen.currentResolution.height == resolutionsTemp[i].height)
            {
                PlayerPrefs.SetInt("Resolution", resolutionNames.Count);
            }
        }
        resolution.AddOptions(resolutionNames);
        resolution.RefreshShownValue();

        loadSettings();

        SetResolution(PlayerPrefs.GetInt("Resolution"));  
    }

    public void SetEffectsVolume() 
    {
        audioMixer.SetFloat("Effects", Mathf.Log10(EffectSlider.value) * 20);
        PlayerPrefs.SetFloat("Effects", EffectSlider.value);
    }
    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Music", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("Music", musicSlider.value);
    }

    public void SetQuality(int qualityIndex) 
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityIndex", qualityIndex);
    }

    public void SetFullScrren(bool isFullscreen) 
    {
        Screen.fullScreen = isFullscreen;

        if (isFullscreen)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else 
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
    }

    public void SetResolution(int resolutionIndex)
    {

        try 
        {
            Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
            PlayerPrefs.SetInt("Resolution", resolutionIndex);
        }
        catch 
        {
            try 
            {
                Screen.SetResolution(resolutions[resolutionIndex - 1].width, resolutions[resolutionIndex - 1].height, Screen.fullScreen);
                PlayerPrefs.SetInt("Resolution", resolutionIndex);
            }
            catch 
            {
                Screen.SetResolution(resolutions[0].width, resolutions[0].height, Screen.fullScreen);
                PlayerPrefs.SetInt("Resolution", resolutionIndex);
            }

        }      
        resolution.value = resolutionIndex;
    }

    void loadSettings()
    {

       // audioMixer.SetFloat("Effects", PlayerPrefs.GetFloat("Effects"));
        EffectSlider.value = PlayerPrefs.GetFloat("Effects");
        SetEffectsVolume();
       // audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("Music"));
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        SetMusicVolume();

        SetQuality(PlayerPrefs.GetInt("QualityIndex"));
        Graphics.value = PlayerPrefs.GetInt("QualityIndex");

        if (PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            SetFullScrren(true);
            fullscreenToggle.isOn = true;
        }
        else
        {
            SetFullScrren(false);
            fullscreenToggle.isOn = false;
        }

    }
}
