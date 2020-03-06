using UnityEngine;
using System.Collections;
using System;

public class Music : MonoBehaviour
{
    public AudioSource audioSource;
    public enum Type { None, Main_1, Main_2, Main_3 };
    public static Music instance;

    [HideInInspector]
    public AudioClip[] musicClips;

    private Type currentType = Type.None;

    private void Awake()
    {
        instance = this;
    }

    public bool IsMuted()
    {
        return !IsEnabled();
    }

    public bool IsEnabled()
    {
        return CUtils.GetBool("music_enabled", true);
    }

    public void SetEnabled(bool enabled, bool updateMusic = false)
    {
        CUtils.SetBool("music_enabled", enabled);
        if (updateMusic)
            UpdateSetting();
    }

    public void Play(Type type)
    {
        if (type == Type.None) return;
        if (currentType != type || !audioSource.isPlaying)
        {
            StartCoroutine(PlayNewMusic(type));
        }
    }

    public void setupMusic(bool setBool)
    {
        if(setBool)
        {
            if(!audioSource.isPlaying&&!GameController.instance.isGameComplete&&!GameController.instance.isGameOver)
                PlayAMusic();
        }
        else
        {
            Pause();
        }
    }

    public void Play()
    {
        Play(currentType);
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    private IEnumerator PlayNewMusic(Type type)
    {
        while (audioSource.volume >= 0.1f)
        {
            audioSource.volume -= 0.2f;
            yield return new WaitForSeconds(0.1f);
        }

        audioSource.Stop();
        currentType = type;
        audioSource.clip = musicClips[(int)type];

        if (IsEnabled())
        {
            audioSource.Play();
        }
        audioSource.volume = 1;
    }

    private void UpdateSetting()
    {
        if (audioSource == null) return;
        if (IsEnabled())
            Play();
        else
            audioSource.Stop();
    }

    private float lastMusicTime = int.MinValue;
    private int lastMusicIndex = -1;
    public void PlayAMusic()
    {
        if (Time.time - lastMusicTime < 60)
        {
            audioSource.UnPause();
            return;
        }

        int length = Enum.GetNames(typeof(Type)).Length - 1;

        lastMusicIndex = (lastMusicIndex + 1) % length;
        lastMusicTime = Time.time;

        Type music = (Type)(lastMusicIndex + 1);

        //if(Prefs.OnMusic)
            Play(music);
    }
}
