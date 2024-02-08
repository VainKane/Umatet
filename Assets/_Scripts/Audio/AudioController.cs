using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sFXSource;

    [Header("Ingame Music")]
    public AudioClip cook;
    public AudioClip hotMilk;
    public AudioClip simpleLove;
    public AudioClip summertime;
    public AudioClip bubbleTea;

    [Header("SFX")]
    public AudioClip coinDrop;
    public AudioClip pickingCoin;
    public AudioClip earningCoin;
    public AudioClip ping;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayingRandomInGameMusic(new List<AudioClip> { cook, hotMilk, simpleLove, summertime, bubbleTea }));

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PlayingRandomInGameMusic(List<AudioClip> musicList)
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
}
