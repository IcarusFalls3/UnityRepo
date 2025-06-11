using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Gravity = 9.8f;
    public float speed = 10f;
    public float jumpHeight = 2f;
    private Rigidbody characterRigidBody;
    private Transform characterTransform;
    private bool isGrounded;
    private void Start()
    {
        characterTransform = transform; //初始化transform并缓存
        characterRigidBody = GetComponent<Rigidbody>(); //初始化刚体
    }
    private void FixedUpdate()
    {
        if(isGrounded)
        {
            float tmp_Horizontal = Input.GetAxis("Horizontal");   //水平AD
            float tmp_Vertical = Input.GetAxis("Vertical");       //垂直WS

            Vector3 tmp_CurrentDirection = new Vector3(tmp_Horizontal, 0, tmp_Vertical);
            tmp_CurrentDirection = characterTransform.TransformDirection(tmp_CurrentDirection); //从自身坐标轴转换成世界坐标轴
            tmp_CurrentDirection *= speed;  //方向*移动速度

            //如果不计算差值，则按一下W会一直往前走
            Vector3 tmp_CurrentVelocity = characterRigidBody.velocity;  //获取当前刚体速度
            Vector3 tmp_VelocityChange = tmp_CurrentDirection - tmp_CurrentVelocity;    //施加的速度 - 刚体速度
            tmp_VelocityChange.y = 0;   //防止发生y轴的移动
            characterRigidBody.AddForce(tmp_VelocityChange, ForceMode.VelocityChange);    //沿着第一个参数的方向施加连续的力，第二个参数为力的类型

            if(Input.GetButtonDown("Jump"))
            {   
                //改变刚体的Y轴速度
                characterRigidBody.velocity = new Vector3(tmp_CurrentVelocity.x, Mathf.Sqrt(2 * jumpHeight * Gravity), tmp_CurrentVelocity.z);
            }
        }
        //当离开地面时对玩家刚体施加重力（初始状态重力是被取消的）
        characterRigidBody.AddForce(new Vector3(0, -Gravity, 0));    //在FixedUpdate()中可以不用乘Time.deltatime,因为该方法是固定0.02s执行一次，在Update()里必须乘
    }

    //一直与碰撞体挨着时触发
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    //离开碰撞体时触发
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}