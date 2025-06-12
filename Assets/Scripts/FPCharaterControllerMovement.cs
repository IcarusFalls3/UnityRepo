using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCharaterControllerMovement : MonoBehaviour
{
    public float speed=10f;             //�ƶ��ٶ�
    public float walkspeed = 10f;   //�����ٶ�
    public float walkspeedwhileCrouched = 5;    //����ʱ�������ٶ�
    public float runspeed = 20f;    //�����ٶ�
    private float Gravity = 9.8f;   //����
    private CharacterController characterController;    //��ҿ���
    private Transform characterTransform;               //���transform
    private Vector3 movementDirection;                  //�˶�ʸ��
    private float JumpHeight = 2f;                      //��Ծ��
    private float CrouchHeight = 1f;                    //�¶׸߶�
    private float StandHeight;                          //վ���߶�
    private float TargetHeight;                         //Ŀ��߶�
    private bool isCrouched;                            //�Ƿ��¶�
    private Animator characterAnimator;                 //����
    private float velocity;                             //�������ٶȲ���

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterTransform = transform;
        isCrouched = false;
        StandHeight = characterController.height;
        characterAnimator = GetComponentInChildren<Animator>(); //ע�������ǻ�ȡ���Ӷ����е�Animator
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            float X = Input.GetAxis("Horizontal");  //��ȡˮƽx�������ֵ,Horizontal = A/D
            float Y = Input.GetAxis("Vertical");    //��ȡ��ֱy�������ֵ,Vertical = W/S

            // λ�Ʒ���
            // ����ֱ��new Vector3(h, 0, v).normalized����ʱ�ο�ϵ����������ϵ
            // movementDirection = (characterTransform.right * X + characterTransform.forward * Y); //��CharacterController������ϵΪ׼,
            movementDirection = characterTransform.TransformDirection(X, 0, Y); //�ڶ��ַ�������API����������ϵת��Ϊ��CharacterController������ϵ

            if(Input.GetButtonDown("Jump")) //Ĭ������Jump = �ո��
            {
                movementDirection.y = JumpHeight;   //����y�������Ծ�߶�(��Ծ����,��ͬ���ܲ�ʱ���ٶ�
            }
            if (isCrouched)
            {
                speed = walkspeedwhileCrouched;
            }
            else
            {
                speed = Input.GetKey(KeyCode.LeftShift) ? runspeed : walkspeed; //��Ԫ��������ж��Ƿ���Shift�����ı��ٶ�
            }

            if(Input.GetKeyDown(KeyCode.C))
            {
                TargetHeight = isCrouched ? StandHeight : CrouchHeight;
                StartCoroutine(DoCrouch(TargetHeight));
                isCrouched = !isCrouched;
            }

        }
        movementDirection.y -= Gravity * Time.deltaTime;    //���y���������ע��������һ������ֵ����������ʽF=1/2*g*t^2
        characterController.Move( movementDirection * speed * Time.deltaTime);    //��CharacterController�Դ���Move��������ƶ�

        //Debug.Log(characterController.velocity.magnitude);    //���������10��������20
        //velocity = characterController.velocity.magnitude;
        Vector3 tmp_velocity = characterController.velocity;    //��ȡ��ɫ�ٶ�(3������)
        tmp_velocity.y = 0; //ȥ��Y�ᣬ��ֹ���º�վ����ٶ�Ҳ�������
        velocity = tmp_velocity.magnitude;  //magnitude���ظ������ĳ���
        characterAnimator.SetFloat("Velocity", velocity, 0.25f, Time.deltaTime);   //��Unity�����õĲ���Velocity��ֵvelocity,����������������ֵ(ʹ��������˳�������ӵĻ���ͻ��ģ�
    }

    private IEnumerator DoCrouch(float target_height)
    {
        float tmp_CurrentHeight = 0;
        while(Mathf.Abs(characterController.height - target_height) > 0.1f) //�����Ŀ��߶��в���
        {
            yield return null;  //�ȵ���һִ֡�������ִ��yield֮������
            characterController.height = Mathf.SmoothDamp(characterController.height, target_height, ref tmp_CurrentHeight, Time.deltaTime * 5);
        }
    }
}
