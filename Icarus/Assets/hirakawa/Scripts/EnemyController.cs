using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float moveDistance;

    private Vector3 EnemyPosition;

    void Start()
    {
        EnemyPosition = transform.position;
    }

    void Update()
    {
        float newPositionX = EnemyPosition.x + Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        transform.position = new Vector3(newPositionX, EnemyPosition.y, EnemyPosition.z);
    }
}
