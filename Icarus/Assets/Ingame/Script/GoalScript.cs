using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.CompleteStage();
        }
    }
}
