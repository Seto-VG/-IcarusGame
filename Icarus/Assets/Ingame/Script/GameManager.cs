using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehavior<GameManager>
{
    [NonSerialized]
    public bool isComplete = false;
    [NonSerialized]
    public bool isDeath = false;
    [SerializeField]
    private string scene;
    [SerializeField]
    public Color loadToColor = Color.white;
    [NonSerialized]
    public bool isGameStop = false;
    [SerializeField]
    private GameObject pauseImage;
    [SerializeField]
    private GameObject tempImage;
    private bool isImageActive = false;
    void Start()
    {
        //pauseImage.SetActive(false);
    }
    void Update()
    {
        if (isDeath)
        {
            Debug.Log("死亡");
        }
        if (isComplete)
        {
            Debug.Log("ゴール");
            ChgScene();
        }
    }
    public void Death()
    {
        isDeath = true;
        PlayerController.instance.DeathAnimation();
        //TODO 死亡SE
        Delay(2.0f);
    }
    public void Respawn() // TODO あとから消す
    {
        isDeath = false;
    }
    public void OnOffSwitchPause() // ポーズ機能の制御
    {
        if (pauseImage != null)
        {
            if (!isGameStop)
            {
                isGameStop = true;
                Time.timeScale = 0; // 時間停止
                pauseImage.SetActive(true);
            }
            else
            {
                isGameStop = false;
                Time.timeScale = 1; // 再開
                pauseImage.SetActive(false);
            }
        }   
    }
    public void OnOffSwitchInstructions() // ゲーム説明のポップアップ制御
    {
        if (tempImage != null)
        {
            if (!isImageActive)
            {
                isImageActive = true;
                tempImage.SetActive(true);
            }
            else
            {
                isImageActive = false;
                tempImage.SetActive(false);
            }
        }
    }
    public void ReturnToTitle()
    {
        isGameStop = false;
        Time.timeScale = 1; // 再開
        Initiate.Fade("Title", loadToColor, 1.0f);
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void CompleteStage()
    {
        isComplete = true;
        PlayerController.instance.Complete();
    }
    public void ChgScene()
    {
        Initiate.Fade(scene, loadToColor, 1.0f);
    }
    public async void Delay(float waitTime)
    {
        var token = this.GetCancellationTokenOnDestroy();
        await UniTask.Delay(TimeSpan.FromSeconds(waitTime),
        cancellationToken: token);
    }
}
