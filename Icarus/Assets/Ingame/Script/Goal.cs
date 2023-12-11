using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("player"))
        {
            GameManager.instance.OnCompleteStage();
        }
    }
}
