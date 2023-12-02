using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jump : MonoBehaviour
{
   
    private Rigidbody rb;
    [SerializeField]
    private bool isGround;
    [SerializeField]
    private bool jump = false;
    private float jumpPower = 5.0f; //ジャンプ力
    [SerializeField]
    private float JetpackPower = 300.0f; //ジャンプ力
    [SerializeField]
    private float  Jetpack = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // スペースが押されたらジャンプさせる
        if (Input.GetKeyDown(KeyCode.Space)&& isGround)
        {
            rb.AddForce(transform.up * jumpPower,ForceMode.Impulse);
            
            // jump = true;
            // isGround = false;
            //rb.AddForce(transform.up * jumpPower);
        }

        // if (jump == true){
            
        //     jump = false;
        // }

        
            if (Input.GetKey(KeyCode.Space)&& !isGround&&Jetpack>0)
            {
                rb.AddForce(transform.up * jumpPower);
                
                Jetpack -=Time.deltaTime;
                
                
            }
            // if (Input.GetKeyDown(KeyCode.Space)&& !isGround){
            //     rb.AddForce(transform.up * jumpPower);
            //     Jetpack = (Jetpack - 1)*Time.deltaTime;
            // }
        
        
    }
    void OnTriggerEnter(Collider other)
    {
        //Planeについていたら地面判定にする
        // if (other.gameObject.name == "Plane" )
        // {
        //     Jetpack=1.5f;
        //     //Debug.Log("地面に衝突");
        //    isGround = true;
        // }
        
        if(other.CompareTag("Ground"))
        {
            Jetpack=1.5f;
            //Debug.Log("地面に衝突");
           isGround = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Plane" )
           isGround = false;
    }
}
