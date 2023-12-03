using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    GameObject Player;
    private int currentHP = 1;
    void Start()
    {
        Player = this.gameObject;
    }
    void Update()
    {
        // 左右移動
        float inputX = Input.GetAxis("Horizontal");
        _rigidBody.velocity = new Vector2(inputX * _speed, _rigidBody.velocity.y);

        if (currentHP <= 0)
        {
            Debug.Log("死亡フラグ");
            //ChageDeathFlag();
            GameManager.instance.Death();
        }
    }

    void ChageDeathFlag()
    {
        GameManager gameManager;
        GameObject GameManager_obj = GameObject.Find("GameManager");
        gameManager = GameManager_obj.GetComponent<GameManager>();
        //gameManager._isDeath = true;
    }

    void OnTriggerStay(Collider other)
    {
        // 敵への当たり判定
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("ダメージ");
            currentHP = currentHP - 1;
        }
    }

}
