using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private SphereCollider _groundSensor;
    private bool _onGround;
    private bool _isJump = false;
    [SerializeField]
    private float _jumpForce = 5.0f; // ジャンプ力
    private float _switchTime = 0.25f; // ジャンプした後のラグ
    private double _elapsedTime = 0.0d;
    [SerializeField]
    private float _jetPackForce = 300.0f; // ジェットの力
    [SerializeField]
    private float _tankCapacity = 1.5f; // ジェットのタンク容量

    void Start()
    {

    }
    void Update()
    {
        //Debug.Log(_onGround);
        // 左右移動
        float inputX = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(inputX * _speed, _rb.velocity.y);
        // ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && _onGround && !_isJump)
        {
            _isJump = true;
            _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }
        if (_isJump)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _switchTime)
            {
                _elapsedTime = 0;
                _isJump = false;
            }
        }
        // ジェットパック
        if (Input.GetKey(KeyCode.Space) && !_onGround && _tankCapacity > 0)
        {
            if (_isJump) { return; }
            _tankCapacity -= Time.deltaTime;
            _rb.AddForce(transform.up * _jumpForce);
        }
    }
    void OnTriggerEnter(Collider _groundSensor)
    {
        if (_groundSensor.CompareTag("Ground"))
        {
            // ジャンプしたときのラグ
            if (_isJump) { return; }
            _onGround = true;
            //TODO 回復するまでのラグの処理
            _tankCapacity = 1.5f;
        }
    }
    void OnTriggerExit(Collider _groundSensor)
    {
        if (_groundSensor.CompareTag("Ground"))
        {
            _onGround = false;
        }
    }
}
