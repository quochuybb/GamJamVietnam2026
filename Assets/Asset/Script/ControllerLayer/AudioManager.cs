using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource audioSource;

    public void PlaySoundTalk(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
    
    
}
