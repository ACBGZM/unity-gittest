using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance; // singleton

    public AudioSource audioSource;

    [SerializeField]
    private AudioClip jumpAudio, hurtAudio, getCherryAudio, getGemAudio;


    private void Awake() {
        instance = this;
    }

    public void PlayJumpAudio() {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }
    public void PlayHurtAudio() {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }
    public void PlayGetCherryAudio() {
        audioSource.clip = getCherryAudio;
        audioSource.Play();
    }
    public void PlayGetGemAudio() {
        audioSource.clip = getGemAudio;
        audioSource.Play();
    }


}
