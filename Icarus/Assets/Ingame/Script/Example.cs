using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public AudioClip seClip;
    public AudioClip bgmClip;

    void Start()
    {
        AudioManager.Instance.AddSEClip("ButtonClick", seClip);
        AudioManager.Instance.PlayBGM(bgmClip);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AudioManager.Instance.PlaySE("ButtonClick");
        }
    }
}
