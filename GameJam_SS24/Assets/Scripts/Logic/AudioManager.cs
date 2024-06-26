using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("Audio Clip")]
    public AudioClip BackgroundMusic;
    public AudioClip Walk;
    public AudioClip Run;
    public AudioClip Jump;
    public AudioClip death;
    void Start()
    {
        MusicSource.clip = BackgroundMusic;
        MusicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
