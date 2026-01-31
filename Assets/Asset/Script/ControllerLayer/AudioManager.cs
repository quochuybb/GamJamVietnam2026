using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    private void Awake()
    {
        // Nếu quên không kéo vào Inspector, code sẽ tự tìm AudioSource trên cùng Object
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }
    public void PlaySoundTalk(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
    
    public void PlayButtonOpen()
    {
        audioSource.PlayOneShot(audioClips[3]);
    }

    public void PlayButtonClose()
    {
        audioSource.PlayOneShot(audioClips[1]);
    }

    public void PlayButtonTick()
    {
        audioSource.PlayOneShot(audioClips[4]);
    }
    public void PlayButtonCross()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }


}
