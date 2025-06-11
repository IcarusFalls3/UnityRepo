using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;   //移动速度
    public Vector3 moveDirection;   //移动向量
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()     //与Roll a Ball使用刚体运动不同，本FPS_demo使用CharacterController来控制移动(自带刚体)
    {
        float h = Input.GetAxis("Horizontal");  //水平x轴
        float v = Input.GetAxis("Vertical");    //竖直y轴

        moveDirection = new Vector3(h, 0, v).normalized;

        characterController.Move(moveDirection * speed * Time.deltaTime);
    }

}