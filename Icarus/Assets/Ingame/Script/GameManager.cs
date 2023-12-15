using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehavior<GameManager>
{
    [NonSerialized]
    public bool isComplete = false;
    [NonSerialized]
    public bool isDeath = false;
    public string scene;
    public Color loadToColor = Color.white;
    void Start()
    {

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
        //TODO 死亡アニメーション
        PlayerController.instance.DeathAnimation();
        //TODO 死亡SE
        Delay(2.0f);
        //TODO 復活地点から復活
    }
    public void Respawn()
    {
        isDeath = false;
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
    public void IndicationGuid()
    {
        UiControl.instance.GuidDisplay();
    }
    public void HideGuid()
    {
        UiControl.instance.GuidHidden();
    }
    async void Delay(float waitTime)
    {
        var token = this.GetCancellationTokenOnDestroy();
        await UniTask.Delay(TimeSpan.FromSeconds(waitTime),
        cancellationToken: token);
    }
}
