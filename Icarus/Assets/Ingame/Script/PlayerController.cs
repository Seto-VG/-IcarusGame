using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
//using System.Numerics;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private SphereCollider _groundSensor;
    private bool _onGround = true;
    private bool _isJump = false;
    [SerializeField]
    private float _jumpForce = 5.0f; // ジャンプ力
    private float _switchTime = 0.25f; // ジャンプした後のラグ
    private double _elapsedTime = 0.0d;
    [SerializeField]
    private float _jetPackForce = 300.0f; // ジェットの力
    [SerializeField]
    private float _tankCapacity = 1.5f; // ジェットのタンク容量
    [SerializeField]
    private float _knockBackPower = 5;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private int life = 1;
    [SerializeField]
    private float _invincibleTime = 2.0f;
    [SerializeField]
    private bool _isInvincible = false;
    [SerializeField]
    private Animator _playerAnimator;
    private Vector3 _playerSize;

    void Start()
    {
        _playerSize = transform.localScale;
    }
    void Update()
    {
        if (GameManager.instance.isDeath) { return; }
        // 左右移動
        float inputX = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(inputX * _speed, _rb.velocity.y);

        // プレイヤーのアニメーション
        if(inputX > 0)
        {
            transform.localScale = new Vector3(_playerSize.x, _playerSize.y, _playerSize.z);
            _playerAnimator.SetBool("movement", true);
        }else if(inputX < 0)
        {
            transform.localScale = new Vector3(-_playerSize.x, _playerSize.y, _playerSize.z);
            _playerAnimator.SetBool("movement", true);
        }else
        {
            _playerAnimator.SetBool("movement", false);   
        }

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
            _playerAnimator.SetBool("jetPack", true);
        }else
        {
            _playerAnimator.SetBool("jetPack", false);
        }
        // 死亡した時の処理
        if (life <= 0)
        {
            _playerAnimator.SetBool("death", true);
            GameManager.instance.Death();
            Material[] materials = _spriteRenderer.materials;
            foreach (Material material in materials)
            {
                Color32 color = material.color;
                Color32 aColor = color;
                aColor.a = 0;
                Sequence seq = DOTween.Sequence().SetAutoKill(false).Pause();
                seq.Append(material.DOColor(endValue: aColor, duration: 0.1f));
                seq.Append(material.DOColor(endValue: color, duration: 0.1f));
                seq.Append(material.DOColor(endValue: aColor, duration: 0.1f));
                seq.Append(material.DOColor(endValue: color, duration: 0.1f));
                seq.SetLoops(-1);
                seq.Play();
            }
            _rb.velocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        // ジャンプと下降のアニメーション
        _playerAnimator.SetBool("jump", _isJump);
        _playerAnimator.SetBool("ground", _onGround);
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
    void OnTriggerStay(Collider other)
    {
        // 敵への当たり判定
        if(other.CompareTag("Enemy") && !_isInvincible)
        {
            Debug.Log("ダメージ");
            life -= 1;
            if (life > 0)
            {
                _playerAnimator.SetBool("damage", true);
                _rb.AddForce(-transform.right * _knockBackPower, ForceMode.VelocityChange);
            }
            StartCoroutine(Unbeatable()); // 無敵化
        }else{
            _playerAnimator.SetBool("damage", false);
        }
        // ゴールへの当たり判定
        if(other.CompareTag("Goal"))
        {
            GameManager.instance.CompleteStage();
        }
    }

    private IEnumerator Unbeatable()
    {
        if(life > 0)
        {
            Material[] materials = _spriteRenderer.materials;
            foreach (Material material in materials)
            {
                Color32 color = material.color;
                Color32 aColor = color;
                aColor.a = 0;
                Sequence seq = DOTween.Sequence().SetAutoKill(false).Pause();
                seq.Append(material.DOColor(endValue: aColor, duration: 0.1f));
                seq.Append(material.DOColor(endValue: color, duration: 0.1f));
                seq.Append(material.DOColor(endValue: aColor, duration: 0.1f));
                seq.Append(material.DOColor(endValue: color, duration: 0.2f));
                //seq.AppendCallback(() => seq.Kill());
                seq.SetLoops(3);
                seq.Play();
            }
        }

        _isInvincible = true;

        // 秒待って次の処理
        yield return new WaitForSeconds(_invincibleTime);

        _isInvincible = false;
    }
}
