using System.Collections;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public AudioMixer masterMixer;
    public Slider sfxSlider;
    public Slider musicSlider;

    private void Start()
    {
        sfxSlider.value = GameManager.instance.GetSFXVolume();
        musicSlider.value = GameManager.instance.GetMusicVolume();
        sfxSlider.enabled = true;
        musicSlider.enabled = true;
    }

    public void NewGame()
    {
        GameManager.instance.ResetSave();
        Continue();
    }

    public void Continue()
    {
        GameManager.instance.Continue();
    }

    public void SetSFXVol(float percent)
    {
        GameManager.instance.SetSFXVolume(percent);
    }

    public void SetMusicVol(float percent)
    {
        GameManager.instance.SetMusicVolume(percent);
    }
}