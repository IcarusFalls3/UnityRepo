using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/Footstep Audio Data")]     //???创建菜单???
public class FootStepAudioDate : ScriptableObject   //Monobehavior必须挂载到Object上，而ScriptableObject不需要挂在到Object上
{
    public List<FootstepAudio> FootstepAudios = new List<FootstepAudio>();
}

[System.Serializable]       //???序列化???
public class FootstepAudio
{
    public string Tag;  //不同材质的标签
    public List<AudioClip> AudioClips = new List<AudioClip>();  //不同材质上的声音列表
    public float Delay=0.3f; //左右脚先后踩的时间间隔
}