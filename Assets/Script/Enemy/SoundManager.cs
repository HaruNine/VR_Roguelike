using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClipAggro1;
    public AudioClip audioClipAggro2;
    public AudioClip audioClipAtk1;
    public AudioClip audioClipAtk2;
    public AudioClip audioClipHurt1;
    public AudioClip audioClipHurt2;
    public AudioClip audioClipDeath1;
    public AudioClip audioClipDeath2;
    public AudioClip audioClipWalk;

    public int RandomInt;
    public int Aggro = 0;

    

    public void PlayWalkSound()
    {
        audioSource.PlayOneShot(audioClipWalk);
        Aggro = 1;
    }

    public void PlayAggroSound()
    {
        RandomInt = Random.Range(0, 1);

        if (RandomInt == 0)
        {
            audioSource.PlayOneShot(audioClipAggro1);
        }
        else
        {
            audioSource.PlayOneShot(audioClipAggro2);
        }
    }

    public void PlayAtkSound()
    {
        RandomInt = Random.Range(0, 1);

        if (RandomInt == 0)
        {
            audioSource.PlayOneShot(audioClipAtk1);
        }
        else
        {
            audioSource.PlayOneShot(audioClipAtk2);
        }
    }

    public void PlayHurtSound()
    {
        RandomInt = Random.Range(0, 1);

        if (RandomInt == 0)
        {
            audioSource.PlayOneShot(audioClipHurt1);
        }
        else
        {
            audioSource.PlayOneShot(audioClipHurt2);
        }
    }

    public void PlayDeathSound()
    {
        RandomInt = Random.Range(0, 1);

        if (RandomInt == 0)
        {
            audioSource.PlayOneShot(audioClipDeath1);
        }
        else
        {
            audioSource.PlayOneShot(audioClipDeath2);
        }
    }
}