using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // シングルトンインスタンス
    private static SoundManager instance;

    // オーディオソース
    private AudioSource bgmAudioSource;
    private AudioSource seAudioSource;

    // BGMとSEの音量
    [Range(0f, 1f)]
    public float bgmVolume = 1f;
    [Range(0f, 1f)]
    public float seVolume = 1f;

    // シングルトンパターンの実装
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("SoundManager");
                    instance = obj.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        // シングルトンの確認
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // オーディオソースの初期化
            bgmAudioSource = gameObject.AddComponent<AudioSource>();
            seAudioSource = gameObject.AddComponent<AudioSource>();

            // ループ再生を許可
            bgmAudioSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM再生
    public void PlayBGM(AudioClip bgmClip)
    {
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.Play();
    }

    // BGM停止
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    // SE再生
    public void PlaySE(AudioClip seClip)
    {
        seAudioSource.PlayOneShot(seClip, seVolume);
    }

    // SE停止
    public void StopSE()
    {
        seAudioSource.Stop();
    }

    // BGMの音量設定
    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmAudioSource.volume = bgmVolume;
    }

    // SEの音量設定
    public void SetSEVolume(float volume)
    {
        seVolume = Mathf.Clamp01(volume);
    }
}