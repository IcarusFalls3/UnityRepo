using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCharaterControllerMovement : MonoBehaviour
{
    public float speed=10f;             //移动速度
    public float walkspeed = 10f;   //行走速度
    public float walkspeedwhileCrouched = 5;    //蹲下时的行走速度
    public float runspeed = 20f;    //奔跑速度
    private float Gravity = 9.8f;   //重力
    private CharacterController characterController;    //玩家控制
    private Transform characterTransform;               //玩家transform
    private Vector3 movementDirection;                  //运动矢量
    private float JumpHeight = 2f;                      //跳跃力
    private float CrouchHeight = 1f;                    //下蹲高度
    private float StandHeight;                          //站立高度
    private float TargetHeight;                         //目标高度
    private bool isCrouched;                            //是否下蹲
    private Animator characterAnimator;                 //动画
    private float velocity;                             //动画的速度参数

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterTransform = transform;
        isCrouched = false;
        StandHeight = characterController.height;
        characterAnimator = GetComponentInChildren<Animator>(); //注意这里是获取的子对象中的Animator
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
            if (isCrouched)
            {
                speed = walkspeedwhileCrouched;
            }
            else
            {
                speed = Input.GetKey(KeyCode.LeftShift) ? runspeed : walkspeed; //三元运算符，判断是否按下Shift键来改变速度
            }

            if(Input.GetKeyDown(KeyCode.C))
            {
                TargetHeight = isCrouched ? StandHeight : CrouchHeight;
                StartCoroutine(DoCrouch(TargetHeight));
                isCrouched = !isCrouched;
            }

        }
        movementDirection.y -= Gravity * Time.deltaTime;    //添加y轴的重力，注意重力是一个积累值，类似物理公式F=1/2*g*t^2
        characterController.Move( movementDirection * speed * Time.deltaTime);    //用CharacterController自带的Move控制玩家移动

        //Debug.Log(characterController.velocity.magnitude);    //行走输出是10，奔跑是20
        //velocity = characterController.velocity.magnitude;
        Vector3 tmp_velocity = characterController.velocity;    //获取角色速度(3个方向)
        tmp_velocity.y = 0; //去掉Y轴，防止蹲下和站起的速度也被算进来
        velocity = tmp_velocity.magnitude;  //magnitude返回该向量的长度
        characterAnimator.SetFloat("Velocity", velocity, 0.25f, Time.deltaTime);   //给Unity中设置的参数Velocity赋值velocity,第三个参数是阻尼值(使动作更加顺滑，不加的话是突变的）
    }

    private IEnumerator DoCrouch(float target_height)
    {
        float tmp_CurrentHeight = 0;
        while(Mathf.Abs(characterController.height - target_height) > 0.1f) //如果和目标高度有差异
        {
            yield return null;  //等到这一帧执行完后再执行yield之后的语句
            characterController.height = Mathf.SmoothDamp(characterController.height, target_height, ref tmp_CurrentHeight, Time.deltaTime * 5);
        }
    }
}
