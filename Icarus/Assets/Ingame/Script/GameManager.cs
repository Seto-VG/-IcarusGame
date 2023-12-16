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
    private GameObject pauseImage; // TODO ポーズ中表示されるオブジェクト　子に閉じる用ボタンもつける
    private GameObject tempImage;
    private bool isImageActive = false;
    [Tooltip("表示したいキャンバス")]
    [SerializeField]
    private GameObject _canvas;
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
        Debug.Log("やー");
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
        if (!isGameStop)
        {
            isGameStop = true;
            Time.timeScale = 0; // 時間停止
            _canvas.SetActive(true);
        }
        else
        {
            isGameStop = false;
            Time.timeScale = 1; // 再開
            _canvas.SetActive(false);
        }
    }
    public void OnOffSwitchInstructions() // ゲーム説明のポップアップ制御
    {
        if (!isImageActive)
        {
            isImageActive = true;
            _canvas.SetActive(true);
        }
        else
        {
            isImageActive = false;
            _canvas.SetActive(false);
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
