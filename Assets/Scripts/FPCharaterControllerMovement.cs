using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCharaterControllerMovement : MonoBehaviour
{
    public float speed;             //移动速度
    public float walkspeed = 10f;   //行走速度
    public float runspeed = 20f;    //奔跑速度
    private float Gravity = 9.8f;   //重力
    private CharacterController characterController;    //玩家控制
    private Transform characterTransform;               //玩家transform
    private Vector3 movementDirection;                  //运动矢量
    private float JumpHeight = 2f;                      //跳跃力
    private float CrouchHeight = 1f;                     //下蹲高度

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            float X = Input.GetAxis("Horizontal");  //获取水平x轴的输入值,Horizontal = A/D
            float Y = Input.GetAxis("Vertical");    //获取竖直y轴的输入值,Vertical = W/S

            // 位移方向
            // 不能直接new Vector3(h, 0, v).normalized，此时参考系是世界坐标系
            // movementDirection = (characterTransform.right * X + characterTransform.forward * Y); //以CharacterController的坐标系为准,
            movementDirection = characterTransform.TransformDirection(X, 0, Y); //第二种方法：用API把世界坐标系转换为以CharacterController的坐标系

            if(Input.GetButtonDown("Jump")) //默认设置Jump = 空格键
            {
                movementDirection.y = JumpHeight;   //设置y方向的跳跃高度(跳跃力）,等同于跑步时的速度
            }
            speed = Input.GetKey(KeyCode.LeftShift) ? runspeed : walkspeed; //三元运算符，判断是否按下Shift键来改变速度

            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                //为什么这里要用协程??? —— 因为跳是给了一个向量，用移动函数，但是蹲考虑到了碰撞体的改变，而碰撞体不能用向量改变大小
                //协程是什么???
                StartCoroutine(DoCrouch());
            }

        }
        movementDirection.y -= Gravity * Time.deltaTime;    //添加y轴的重力，注意重力是一个积累值，类似物理公式F=1/2*g*t^2
        characterController.Move( movementDirection * speed * Time.deltaTime);    //用CharacterController自带的Move控制玩家移动
    }

    private IEnumerator DoCrouch()
    {
        while(true) //10:00
        {

        }
    }
}
