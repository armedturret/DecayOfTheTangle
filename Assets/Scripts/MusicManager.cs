using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioClip menuMusic;
    public AudioClip mainMusic;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            _audioSource = GetComponent<AudioSource>();
        }
    }

    public void MenuMusic()
    {
        if (_audioSource.clip == menuMusic)
            return;

        _audioSource.Stop();
        _audioSource.clip = menuMusic;
        _audioSource.Play();
    }

    public void MainMusic()
    {
        if (_audioSource.clip == mainMusic)
            return;

        _audioSource.Stop();
        _audioSource.clip = mainMusic;
        _audioSource.Play();
    }

}