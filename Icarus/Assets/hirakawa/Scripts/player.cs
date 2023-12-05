using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private Rigidbody _rigidBody;
    GameObject Player;
    private int currentHP = 5;
    bool _isInvincible = false;
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
            GameManager.instance.Death();
        }
    }

    void OnTriggerStay(Collider other)
    {
        // 敵への当たり判定
        if(other.CompareTag("Enemy"))
        {
            if (!_isInvincible)
            {
                currentHP--;
                Debug.Log("ダメージ");
                StartCoroutine(Unbeatable()); // 無敵化
            }
        }
    }

    private IEnumerator Unbeatable()
    {
        _isInvincible = true;

        // 秒待って次の処理
        yield return new WaitForSeconds(1.0f);

        _isInvincible = false;
    }

}
