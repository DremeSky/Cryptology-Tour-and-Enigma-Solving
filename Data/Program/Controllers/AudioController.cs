using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource musicSource ;
    public AudioSource soundSource ;

    public void PlayAudio(AudioClip music ,AudioClip sound)
    {
        if(sound!=null)
        {
            soundSource.clip =sound ;
            soundSource.Play() ;
        }

        if(music!=null && musicSource.clip!=music)
        {
            StartCoroutine(SwitchMusic(music)) ;
        }
    }

    private IEnumerator SwitchMusic(AudioClip music)
    {
        if(musicSource.clip!=null)
        {
            while(musicSource.volume >0)
            {
                musicSource.volume-=0.05f ;
                yield return new WaitForSeconds(0.05f) ;
            }
        }
        else
        {
            musicSource.volume =0 ;
        }

        musicSource.clip =music ;
        musicSource.Play() ;

        while(musicSource.volume <0.5)
        {
            musicSource.volume+=0.05f ;
            yield return new WaitForSeconds(0.05f) ;
        }
    }
}
