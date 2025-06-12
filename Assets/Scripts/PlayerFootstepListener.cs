using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepListener : MonoBehaviour
{
    public FootStepAudioDate footstepAudioDate;
    public AudioSource footstepAudioSource;

    private CharacterController characterController;
    private Transform footstepTransform;
    private float nextPlayTime;

    //如何检测是否发出声音？—— 角色运动幅度较大
    //如何检测玩家是否移动？ —— Physical API检测
    //如何发出对应材质的声音？ —— Physical API检测

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        footstepTransform = transform;
    }

    private void FixedUpdate()
    {
        if (characterController.isGrounded)
        {
            if(characterController.velocity.normalized.magnitude > 0.1f)  //检测是否移动
            {
                nextPlayTime += Time.deltaTime; //累加下次播放音频的时间
                bool isHit = Physics.Linecast(footstepTransform.position,   //从玩家的中心
                    footstepTransform.position + Vector3.down * (characterController.height / 2 + characterController.skinWidth - characterController.center.y),          //向下距离
                    out RaycastHit tmp_HitInfo);                            //检测碰撞体

                //Debug.DrawLine(footstepTransform.position,
                //    footstepTransform.position + Vector3.down * (characterController.height / 2 + characterController.skinWidth - characterController.center.y),
                //    Color.red,
                //    0.25f);

                //从玩家的位置向周围播放移动声音
                if (isHit)
                {
                    //在音频信息中找对应的元素
                    foreach(var tmp_AudioElement in footstepAudioDate.FootstepAudios)
                    {
                        if(tmp_HitInfo.collider.CompareTag(tmp_AudioElement.Tag))   //检测材质类型:如果碰撞体的tag在音频列表中有对应的tag
                        {
                            if (nextPlayTime >= tmp_AudioElement.Delay) //当达到下一次播放音频的延迟时间后才播放,这里还可以改进到Running/Crouching使用不同的Delay(未完待续)
                            {
                                //TODO:播放对应的声音
                                int tmp_AudioCount = tmp_AudioElement.AudioClips.Count; //获取对应tag的Clips数
                                int tmp_AudioIndex = UnityEngine.Random.Range(0, tmp_AudioCount);   //在0-Count数间随机选一个
                                AudioClip tmp_footstepAudioClip = tmp_AudioElement.AudioClips[tmp_AudioIndex];  //获取随机选取的Clip
                                footstepAudioSource.clip = tmp_footstepAudioClip;   //将音频Clip赋值给AudioSource
                                footstepAudioSource.Play(); //播放AudioSource
                                nextPlayTime = 0;   //清空时间计算
                                break;  //跳出播放循环
                            }
                        }
                    }
                }
            }
        }
    }

}
