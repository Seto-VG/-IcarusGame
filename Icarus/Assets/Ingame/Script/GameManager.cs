using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    public bool isComplete = false;
    public bool isDeath = false;
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
}
