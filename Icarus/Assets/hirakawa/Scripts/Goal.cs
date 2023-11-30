using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChageCompleteFlag()
    {
        GameManager gameManager;
        GameObject GameManager_obj = GameObject.Find("GameManager");
        gameManager = GameManager_obj.GetComponent<GameManager>();
        gameManager.is_complete = true;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.name == "Player")
        {
            ChageCompleteFlag();
        }
    }
}
