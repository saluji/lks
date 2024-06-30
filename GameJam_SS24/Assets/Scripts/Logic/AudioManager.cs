using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sFXSource;
    
    [Header("Audio Clip")]
    public AudioClip backgroundMusic;
    public AudioClip walk;
    public AudioClip run;
    public AudioClip jump;
    public AudioClip death; 
    public AudioClip whistle; 
    public AudioClip scream; 

    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sFXSource.PlayOneShot(clip);
    }
}
