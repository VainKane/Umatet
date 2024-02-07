using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sFXSource;

    [Header("Ingame Music")]
    [SerializeField] private AudioClip cook;
    [SerializeField] private AudioClip hotMilk;
    [SerializeField] private AudioClip simpleLove;
    [SerializeField] private AudioClip summertime;
    [SerializeField] private AudioClip bubbleTea;


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
}
