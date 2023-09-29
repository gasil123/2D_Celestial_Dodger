using UnityEngine;
using UnityEngine.Audio;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager Instance;

    public AudioMixer _mixer;
    [SerializeField] AudioSource _bgm;
    [SerializeField] AudioSource _gameOver;
    public AudioSource _playerHurt;
    public AudioSource _turretFire;
    public AudioSource _turretHurt;
    public AudioSource _planeBlast;

    string bgmVol = "_BGMParameter";
    string sfxVol = "_SfxParameter";

    public void SetMusicVolume(float musicVolume)
    {
        PlayerPrefs.SetFloat(bgmVol, musicVolume);
        _mixer.SetFloat(bgmVol, Mathf.Log10(musicVolume) * 20);
    }
    public void SetSfxVolume(float sfxVolume)
    {
        PlayerPrefs.SetFloat(sfxVol, sfxVolume);
        _mixer.SetFloat(sfxVol, Mathf.Log10(sfxVolume) * 20);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        _bgm.playOnAwake = true;
    }
    public void Mute()
    {
        MuteUnmute(false);
    }
    public void UnMute()
    {
        MuteUnmute(true);
    }

    public void playBGM()
    {
        _bgm.Play();
    }
    public void stopBGM()
    {
        _bgm.Stop();
    }
    public void PlayGameOver()
    {
        stopBGM();
        _gameOver.Play();
    }
    public void StopGameOver()
    {
        _gameOver.Stop();
    }

    public void MuteUnmute(bool state)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<AudioSource>().enabled = state;
        }
    }
}
