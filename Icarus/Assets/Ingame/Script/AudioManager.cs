using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource seSource;

    // SEのサウンドクリップを格納するための辞書
    private Dictionary<string, AudioClip> seClips = new Dictionary<string, AudioClip>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // SEのサウンドクリップを追加
    public void AddSEClip(string clipName, AudioClip clip)
    {
        if (!seClips.ContainsKey(clipName))
        {
            seClips.Add(clipName, clip);
        }
        else
        {
            Debug.LogWarning("Duplicate entry for SE clip: " + clipName);
        }
    }

    // SEを再生するメソッド
    public void PlaySE(string clipName)
    {
        if (seClips.ContainsKey(clipName))
        {
            seSource.PlayOneShot(seClips[clipName]);
        }
        else
        {
            Debug.LogWarning("SE clip not found: " + clipName);
        }
    }

    // BGMを再生するメソッド
    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    // BGMの再生を停止するメソッド
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}