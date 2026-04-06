using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Источники звука")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Звуковые клипы")]
    public AudioClip backgroundMusic;
    public AudioClip coinSound;
    public AudioClip loseSound;
    public AudioClip bonusSound;

    private bool isBackgroundStopped = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isBackgroundStopped = false;
        sfxSource.Stop();

        PlayBackgroundMusic();
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayCoinSFX()
    {
        sfxSource.PlayOneShot(coinSound);
    }

    public void PlayBackgroundMusic()
    {
        if (isBackgroundStopped) return; 
        if (musicSource.clip == backgroundMusic && musicSource.isPlaying) return;

        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlayLoseSound()
    {
        musicSource.Stop();
        isBackgroundStopped = true;

        sfxSource.PlayOneShot(loseSound);
    }

    public void PlayBonusSFX()
    {
        StartCoroutine(PlayCutSound(bonusSound, 0f, 1f));
    }

    private IEnumerator PlayCutSound(AudioClip clip, float startTime, float duration)
    {
        sfxSource.clip = clip;
        sfxSource.time = startTime;
        sfxSource.Play();

        yield return new WaitForSeconds(duration);

        sfxSource.Stop();
    }
}