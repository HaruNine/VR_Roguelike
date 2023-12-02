using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip1; // walk
    public AudioClip audioClip2; // run
    public AudioClip audioClip3; // atk 1
    public AudioClip audioClip4; // atk 2
    public AudioClip audioClip5; // atk 3
    public AudioClip audioClip6; // hurt
    public AudioClip audioClip7; // death
    public AudioClip audioClip8; // door
    public AudioClip audioClip9; // buff
    public AudioClip audioClip10; // nerf

    public int RandomInt;
    public int Move;

    public void PlayMoveSound()
    {
        if (Move == 1)
        {
            audioSource.PlayOneShot(audioClip1);
        }
        if (Move == 2)
        {
            audioSource.PlayOneShot(audioClip2);
        }
    }

    public void PlayAtkSound()
    {
        RandomInt = Random.Range(0, 2);

        if (RandomInt == 0)
        {
            audioSource.PlayOneShot(audioClip3);
        }
        if (RandomInt == 1)
        {
            audioSource.PlayOneShot(audioClip4);
        }
        if (RandomInt == 2)
        {
            audioSource.PlayOneShot(audioClip5);
        }
    }

    public void PlayHurtSound()
    {
        audioSource.PlayOneShot(audioClip6);
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(audioClip7);
    }

    public void PlayDoorSound()
    {
        audioSource.PlayOneShot(audioClip8);
    }

    public void PlayBuffSound()
    {
        audioSource.PlayOneShot(audioClip9);
    }

    public void PlayNerfSound()
    {
        audioSource.PlayOneShot(audioClip10);
    }
}