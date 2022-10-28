using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2;

    [SerializeField, TooltipAttribute("移動速度変更")]
    float moveSpeed;


    Rigidbody playerRigidbody;
    float moveX;
    float moveZ;
    bool isMoveX;
    bool isMoveZ;

 

    Vector3 vec3;

    bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = this.gameObject.GetComponent<Rigidbody>();
 
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);
        //Raycast(rayの開始地点,rayの向き(この場合(0, -1, 0)), rayの発射距離)
        Debug.Log(isGrounded);

        movePermission();
    }

    private void FixedUpdate()
    {
        moveExecution();
    }
    void movePermission()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");


        if (moveX != 0f)
        {
            isMoveX = true;
        }

        if (moveZ != 0f)
        {
            isMoveZ = true;
        }



    }

    void moveExecution()
    {
        if (isMoveX || isMoveZ)
        {
            vec3 = new Vector3(moveX, 0, moveZ);

            if (vec3.magnitude > 1)
            {

                playerRigidbody.velocity = transform.rotation * vec3.normalized * moveSpeed * Time.deltaTime * 100;
            }
            else
            {
                //1フレームの時間に左右させないようデルタタイムをかけ
                //プレイヤーの向きに応じてｘ軸ｚ軸に移動する。
                playerRigidbody.velocity = transform.rotation * vec3 * moveSpeed * Time.deltaTime * 100;
            }
   
            isMoveX = false;
            isMoveZ = false;

        }
        else
        {
            playerRigidbody.velocity = Vector3.zero;
        }
    }
}
