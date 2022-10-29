using System.Collections;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AudioMixer masterMixer;

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
        percent = Mathf.Max(percent, 0.001f);
        masterMixer.SetFloat("SFXVol", Mathf.Log10(percent) * 20);
    }

    public void SetMusicVol(float percent)
    {
        percent = Mathf.Max(percent, 0.001f);
        masterMixer.SetFloat("MusicVol", Mathf.Log10(percent) * 20);
    }
}