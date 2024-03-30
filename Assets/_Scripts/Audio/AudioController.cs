using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sFXSource;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sFXSlider;

    [Header("Ingame Music")]
    public AudioClip hotMilk;
    public AudioClip simpleLove;
    public AudioClip summertime;
    public AudioClip bubbleTea;
    public AudioClip halzion;
    public AudioClip wrapMeInPlastic;
    public AudioClip haVuongConNang;
    public AudioClip lanterns;

    [Header("GameMenu Music")]
    public AudioClip vietnam;

    [Header("SFX")]
    public AudioClip coinDrop;
    public AudioClip pickingCoin;
    public AudioClip earningCoin;
    public AudioClip ping;

    private List<AudioClip> musicList;


    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "AudioController (Menu)")
        {
            musicList = new List<AudioClip>() { vietnam };

            if (PlayerPrefs.HasKey("musicVolume") == true)
            {
                LoadMusicVolume();
            }
            else
            {
                musicSlider.value = 0.56f;
                SettingMusicVolume();
            }
        }
        else
        {
            musicList = new List<AudioClip> { hotMilk, simpleLove, summertime, bubbleTea, halzion, wrapMeInPlastic, lanterns, haVuongConNang };
            
            if (PlayerPrefs.HasKey("sFXVolume") == true)
            {
                LoadMusicVolume();
                SFXVolumeLoader();
            }
            else
            {
                sFXSlider.value = 0.38f;
                SettingMusicVolume();
                SettingSFXVolume();
            }
        }
        
        StartCoroutine(PlayMusic(musicList));

    }
    IEnumerator PlayMusic(List<AudioClip> musicList)
    {
    play:
        int ingameMusicSequence = Random.Range(0, musicList.Count);
        musicSource.clip = musicList[ingameMusicSequence];
        musicSource.Play();
        yield return new WaitForSeconds(musicSource.clip.length);
        goto play;
    }

    public void PlayingSFX(AudioClip sfx)
    {
        sFXSource.PlayOneShot(sfx);
    }

    public void SettingSFXVolume()
    {
        float volume = sFXSlider.value;
        audioMixer.SetFloat("sFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sFXVolume", volume);
    }

    public void SettingMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SettingMusicVolume();
    }

    private void SFXVolumeLoader()
    {
        sFXSlider.value = PlayerPrefs.GetFloat("sFXVolume");
        SettingSFXVolume();
    }
}
