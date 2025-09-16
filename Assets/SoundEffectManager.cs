using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {

    [Header("--- Audio Sources ---")]
    [SerializeField] AudioSource src;
    [SerializeField] AudioSource musicSrc;

    [Header("--- Audio Clips ---")]
    public AudioClip sfx_bounce1;
    public AudioClip sfx_bounce2;
    public AudioClip sfx_hitsound;
    public AudioClip sfx_collectfuel1;
    public AudioClip sfx_collectfuel2;
    public AudioClip sfx_collectgem;
    public AudioClip sfx_deathsound;
    public AudioClip sfx_gamemusic;
    public AudioClip sfx_menumusic;

    public double loopTimeGameMusic;
    public double durationGameMusic;

    public void jump1SFX() {
        src.clip = sfx_bounce1;
        src.PlayOneShot(sfx_bounce1);
    }
    
    public void jump2SFX() {
        src.clip = sfx_bounce2;
        src.PlayOneShot(sfx_bounce2);
    }

    public void hitSFX() {
        src.clip = sfx_hitsound;
        src.PlayOneShot(sfx_hitsound);
    }

    public void collectFuel1SFX() {
        src.clip = sfx_collectfuel1;
        src.PlayOneShot(sfx_collectfuel1);
    }

    public void collectFuel2SFX() {
        src.clip = sfx_collectfuel2;
        src.PlayOneShot(sfx_collectfuel2);
    }

    public void collectGemSFX() {
        src.clip = sfx_collectgem;
        src.PlayOneShot(sfx_collectgem);
    }

    public void deathSoundSFX() {
        src.clip = sfx_deathsound;
        src.PlayOneShot(sfx_deathsound);
    }

    public void menuMusic() {
        musicSrc.clip = sfx_menumusic;
        musicSrc.PlayOneShot(sfx_menumusic);
    }

    public void gameMusic() {
        musicSrc.clip = sfx_gamemusic;
        musicSrc.PlayOneShot(sfx_gamemusic);
    }
}
