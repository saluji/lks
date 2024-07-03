using System.Collections;
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
    public AudioClip sneak;
    public AudioClip button;
    // bool isPlaying;

    void Awake()
    {
        // isPlaying = false;
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public IEnumerator PlaySFX(AudioClip clip)
    {
        sFXSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        StartCoroutine(PlaySFX(clip));
        //     isPlaying = true;
        //     if (isPlaying)
        //     {
        //         isPlaying = false;
        //         StartCoroutine(ClipLength(clip.length));
        //         isPlaying = true;
    }
    // }

    // IEnumerator ClipLength(float clipLength)
    // {
    //}
}
