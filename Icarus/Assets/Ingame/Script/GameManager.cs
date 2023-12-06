using System;
using System.Collections;
using System.Collections.Generic;
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
        if(isDeath)
        {
            Debug.Log("死亡");
        }
        if(isComplete)
        {
            Debug.Log("ゴール");
        }
    }
    public void Death(){
        isDeath = true;
        //TODO 死亡アニメーション
    }
    public void OnCompleteStage()
    {
        isComplete = true;
    }
    public void ChgScene()
    {
        Initiate.Fade(scene, loadToColor, 1.0f);
    }
}
