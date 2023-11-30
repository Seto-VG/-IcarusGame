using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    public GameObject player;
    public bool is_complete = false;
    public bool is_death = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(is_complete)
        {
            Debug.Log("ゴール");
        }
        
        if(is_death)
        {
            Debug.Log("死亡");
            Destroy(player);
        }
    }
}
