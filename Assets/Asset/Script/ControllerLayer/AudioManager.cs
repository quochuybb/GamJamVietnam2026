using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    [Header("Menu Music")]
    [SerializeField] private AudioClip[] menuWaitingSongs;

    [Header("SFX Clips")]
    [SerializeField] private AudioClip[] audioClips;

    private void Awake() {
        // Tự động thiết lập nếu quên kéo vào Inspector
        if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();
        if (bgmSource == null) {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true; // Nhạc nền luôn lặp lại
        }
    }

    // --- PHẦN NHẠC NỀN (BGM) ---

    public void PlayRandomMenuMusic() {
        if (menuWaitingSongs.Length == 0) return;
        
        int randomIndex = Random.Range(0, menuWaitingSongs.Length);
        bgmSource.clip = menuWaitingSongs[randomIndex];
        bgmSource.Play();
    }

    public void PlayPatientTheme(AudioClip clip) {
        if (clip == null) return;
        
        // Chỉ đổi nhạc nếu nhạc mới khác nhạc đang chạy
        if (bgmSource.clip == clip) return;

        bgmSource.clip = clip;
        bgmSource.Play();
    }
    public void PlayBossTheme() {
        bgmSource.clip = audioClips[5];
        bgmSource.Play();
    }

    public void StopMusic() {
        bgmSource.Stop();
    }
    public void PlaySoundTalk(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(audioClips[0]);
    }
    
    public void PlayButtonOpen()
    {
        sfxSource.PlayOneShot(audioClips[3]);
    }

    public void PlayButtonClose()
    {
        sfxSource.PlayOneShot(audioClips[1]);
    }

    public void PlayButtonTick()
    {
        sfxSource.PlayOneShot(audioClips[4]);
    }
    public void PlayButtonCross()
    {
        sfxSource.PlayOneShot(audioClips[2]);
    }


}
