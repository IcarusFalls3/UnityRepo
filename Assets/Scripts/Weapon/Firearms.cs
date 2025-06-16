using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public abstract class Firearms : MonoBehaviour, IWeapon     //抽象类，多种枪
    {
        public Transform MuzzlePoint;   //枪口位置
        public Transform CasingPoint;   //弹壳抛出位置

        public ParticleSystem MuzzleParticle;   //用粒子系统做枪焰
        public ParticleSystem CasingParticle;   //用粒子系统做弹壳

        public int AmmoInMag = 30;    //单个弹夹容量
        public int MaxAmmoCarried = 120;    //总子弹容量

        protected int CurrentAmmo;  //当前弹夹容量
        protected int CureentMaxAmmoCarried;    //当前总子弹数量

        private float FireRate; //射速
        private float lastFireTime; //上次开枪时间

        protected Animator GunAnimator;    //开枪动画

        protected virtual void Start()  //虚函数，方便子类重写
        {
            CurrentAmmo = AmmoInMag;
            CureentMaxAmmoCarried = MaxAmmoCarried;
        }

        public void DoAttack()
        {
            if (CurrentAmmo <= 0) return;
            CurrentAmmo -= 1;
            Shootting();
            lastFireTime = Time.time;
        }

        protected abstract void Shootting();    //加上abstract意思是抽象类，有多种开枪方式
        protected abstract void Reload();   //装弹类

        private bool IsAllowShootting() //射速/开枪间隔
        {
            //AK-47射速：715发/min -> 715/60 = 11.7 发/s -> 1s/11.7发 = 射速
            return Time.time - lastFireTime > 1/FireRate;   //是否达到下次开火时间
        }
        
    }
}
