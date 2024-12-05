using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickHandlerScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private bool isPlaying = false; 
    public void OnButtonClickEntrance()
    {
        Debug.Log("Entrance button clicked");

        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void OnButtonClickRedwoodGrove()
    {
        Debug.Log("Redwood grove button clicked");

        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    public void OnButtonClickPortrait()
    {
        Debug.Log("Portrait button clicked");

        audioSource.clip = audioClips[2];
        audioSource.Play();
    }

    public void OnButtonClickToolWall()
    {
        Debug.Log("Tool wall button clicked");

        audioSource.clip = audioClips[3];
        audioSource.Play();
    }

    public void OnButtonClickGoldenBoy()
    {
        Debug.Log("Golden boy button clicked");

        audioSource.clip = audioClips[4];
        audioSource.Play();
    }

    public void OnButtonClickComicSketches()
    {
        Debug.Log("Comic sketches button clicked");

        if (!isPlaying)
        {
            StartCoroutine(PlayAudioClipsComicSketches());
        }
    }

    private System.Collections.IEnumerator PlayAudioClipsComicSketches()
    {
        isPlaying = true;

        for (int i = 5; i <= 7; i++)
        {
            var clip = audioClips[i];
            // Set the clip to the AudioSource and play it
            audioSource.clip = clip;
            audioSource.Play();

            // Wait until the current clip finishes playing
            yield return new WaitForSeconds(clip.length);
        }

        isPlaying = false; // Reset the flag once playback is complete
    }

    public void OnButtonClickJawbonePaintings()
    {
        Debug.Log("Jawbone paintings button clicked");

        if (!isPlaying)
        {
            StartCoroutine(PlayAudioClipsJawbonePaintings());
        }
    }

    private System.Collections.IEnumerator PlayAudioClipsJawbonePaintings()
    {
        isPlaying = true;

        for (int i = 8; i <= 9; i++)
        {
            var clip = audioClips[i];
            // Set the clip to the AudioSource and play it
            audioSource.clip = clip;
            audioSource.Play();

            // Wait until the current clip finishes playing
            yield return new WaitForSeconds(clip.length);
        }

        isPlaying = false; // Reset the flag once playback is complete
    }

    public void OnButtonClickUntitledPictures()
    {
        Debug.Log("Untitled pictures button clicked");

        audioSource.clip = audioClips[10];
        audioSource.Play();
    }

    public void OnButtonClickBackCorner()
    {
        Debug.Log("Back corner button clicked");

        if (!isPlaying)
        {
            StartCoroutine(PlayAudioClipsBackCorner());
        }
    }

    private System.Collections.IEnumerator PlayAudioClipsBackCorner()
    {
        isPlaying = true;

        for (int i = 11; i <= 12; i++)
        {
            var clip = audioClips[i];
            // Set the clip to the AudioSource and play it
            audioSource.clip = clip;
            audioSource.Play();

            // Wait until the current clip finishes playing
            yield return new WaitForSeconds(clip.length);
        }

        isPlaying = false; // Reset the flag once playback is complete
    }

    public void OnButtonClickTwelvePassOne()
    {
        Debug.Log("Twelve pass one button clicked");

        audioSource.clip = audioClips[13];
        audioSource.Play();
    }

    public void OnButtonClickRockWall()
    {
        Debug.Log("Rock wall button clicked");

        audioSource.clip = audioClips[10];
        audioSource.Play();
    }

    public void OnButtonClickBackHallway()
    {
        Debug.Log("Back hallway button clicked");

        if (!isPlaying)
        {
            StartCoroutine(PlayAudioClipsBackHallway());
        }
    }

    private System.Collections.IEnumerator PlayAudioClipsBackHallway()
    {
        isPlaying = true;

        for (int i = 15; i <= 16; i++)
        {
            var clip = audioClips[i];
            // Set the clip to the AudioSource and play it
            audioSource.clip = clip;
            audioSource.Play();

            // Wait until the current clip finishes playing
            yield return new WaitForSeconds(clip.length);
        }

        isPlaying = false; // Reset the flag once playback is complete
    }
}
