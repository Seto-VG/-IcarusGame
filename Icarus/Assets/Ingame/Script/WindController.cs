using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WindController : MonoBehaviour
{
    float cos;
    [SerializeField]
    float forceY = 13.0f;
    Vector3 _force;
    private float gra = -9.81f;
    public float scale = 0.1f;
    public float switchTime = 0.25f;
    float _time;
    private double _elapsedTime = 0.0d;
    private bool _isEnableBlow = true;
    void Start()
    {
        _force = new Vector3(0, -(gra - scale), 0);
        _time = switchTime;
    }
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        //Debug.Log(_elapsedTime);
        if (_elapsedTime >= _time)
        {
            _elapsedTime = 0;
            _isEnableBlow = !_isEnableBlow;
            _force = _isEnableBlow ? new Vector3(0, -(gra - scale), 0) : new Vector3(0, -scale, 0);
            //_time = _isEnableBlow ? switchTime * 5 : switchTime;
            Debug.Log(_isEnableBlow);
            Debug.Log(_force);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.attachedRigidbody.AddForce(_force);
        }
    }
}
