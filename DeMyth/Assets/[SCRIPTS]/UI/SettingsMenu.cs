using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    [SerializeField] private Slider audioSlider;
    [SerializeField] private Slider musicSlider;

    public GameObject masterEnabled, masterDisabled, musicEnabled, musicDisabled;

    void Start(){
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && 
               resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        Load();
    }
    public void Load(){     //Load() is its own function so it can be called anytime it needs to be, rather than the code just being in Start()
        audioMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("masterVolume"));
        musicMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("musicVolume"));
        audioSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        if(PlayerPrefs.GetString("masterMute") == "true"){
            MuteVolume(true);
            masterEnabled.SetActive(false);
            masterDisabled.SetActive(true);
        }
        if(PlayerPrefs.GetString("musicMute") == "true"){
            MuteMusic(true);
            musicEnabled.SetActive(false);
            musicDisabled.SetActive(true);
        }
    }


    public void SetVolume (float volume)
    {
        if(volume <= -39){
            audioMixer.SetFloat("MasterVol", -80);
            PlayerPrefs.SetFloat("masterVolume", -80);}
        else{
            audioMixer.SetFloat("MasterVol", volume);
            PlayerPrefs.SetFloat("masterVolume", volume);}

    }
    public void SetMusic (float volume)
    {
        if(volume <= -39){ //This IF/Else statement clips the operating volume control to -40dB instead of the full -80. This could be replaced with a logarithmic function however is done this way to allow the player to have better feeling control over the volume. 
            musicMixer.SetFloat("MusicVol", -80);
            PlayerPrefs.SetFloat("musicVolume", -80);}
        else{
            musicMixer.SetFloat("MusicVol", volume);
            PlayerPrefs.SetFloat("musicVolume", volume);}
    }

    public void MuteVolume(bool boolMute){
        if(boolMute == true){
            audioMixer.SetFloat("MasterVol", -80);
            PlayerPrefs.SetString("masterMute", "true");
        }
        if(boolMute == false){
            audioMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("masterVolume"));
            PlayerPrefs.SetString("masterMute", "false");
        }
    }
    public void MuteMusic(bool boolMute){
        if(boolMute == true){
            musicMixer.SetFloat("MusicVol", -80);
            PlayerPrefs.SetString("musicMute", "true");
            }
        if(boolMute == false){
            musicMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("musicVolume"));
            PlayerPrefs.SetString("musicMute", "false");
            }
    }

    public void SetFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution (int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); 
    }

}
