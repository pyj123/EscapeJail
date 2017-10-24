﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScientistBoss
{
    public enum Actions
    {
        FireStart,
        FireEnd,
        HideStart,
        HideEnd,
        Dead
    }

    public class ScientistBoss : BossBase
    {

        //인스펙터에서 할당
        [SerializeField]
        public List<ChildComputer> childList;


        private int childHp = 10;
    

        private new void Awake()
        {
            base.Awake();
            SetHp(30);
            RegistPatternToQueue();
        }

        public override void StartBossPattern()
        {
            StartChild();    

            if (bossEventQueue != null)
                bossEventQueue.StartEventQueue();
  
        }
        public void ChildDead()
        {
            if (isChildAlive() == false)
            {
                StartRealBoss();
            }
        }

        //자식들 다죽었을때 들어옴
        private void StartRealBoss()
        {
            base.StartBossPattern();
            bossEventQueue.RemoveAllEvent();
            bossEventQueue.AddEvent("ParentPattern");
        }

        private void StartChild()
        {
            if (childList == null) return;

            for (int i = 0; i < childList.Count; i++)
            {
                childList[i].StartPattern(this);
                childList[i].SetHp(childHp);
            }
        }
        public bool isChildAlive()
        {
            if (childList == null) return false;

            for(int i=0;i< childList.Count; i++)
            {
                if (childList[i].isDead == false) return true;
            }

            return false;
        }

        public override void GetDamage(int damage)
        {

            if (isChildAlive() == true)
            {
                ShieldEffectOn();
                return;
            }

            base.GetDamage(damage);

        }

        //임시코드//////////////////////////
        private bool isShieldOn = false;
        private void ShieldEffectOn()
        {
            if (isShieldOn == true) return;
            isShieldOn = true;
            GameObject target = spriteRenderer.gameObject;
            iTween.ColorTo(target, iTween.Hash("loopType", "pingPong", "Time", 0.05f, "Color", Color.blue));

            Invoke("ShieldEffectOff", 1f);

        }

        private void ShieldEffectOff()
        {
            isShieldOn = false;
            GameObject target = spriteRenderer.gameObject;
            iTween.ColorTo(target, Color.white, 0.1f);
        }

        //임시코드//////////////////////////////



        private void Action(Actions action)
        {
            switch (action)
            {
                case Actions.FireStart:
                    {
                        if (animator != null)
                            animator.SetTrigger("FireTrigger");
                    }
                    break;
                case Actions.FireEnd:
                    {
                        if (animator != null)
                            animator.SetTrigger("FireEndTrigger");
                    }
                    break;
                case Actions.HideStart:
                    {
                        if (animator != null)
                            animator.SetTrigger("HideTrigger");

                    }
                    break;
                case Actions.HideEnd:
                    {
                        if (animator != null)
                            animator.SetTrigger("HideEndTrigger");
                    }
                    break;
                case Actions.Dead:
                    {
                        if (animator != null)
                            animator.SetTrigger("DeadTrigger");
                    }
                    break;

            }
        }



        private void RegistPatternToQueue()
        {

            bossEventQueue.Initialize(this, EventOrder.InOrder);
            bossEventQueue.AddEvent("ChildPattern");

        }


        IEnumerator ChildPattern()
        {
            List<ChildComputer> RandomMonster = new List<ChildComputer>();

            //안죽은애들 넣어줌
            for(int i = 0; i < childList.Count; i++)
            {
                if (childList[i].isDead == false)
                    RandomMonster.Add(childList[i]);
            }

            RandomMonster.ListShuffle();

            for (int i = 0; i < RandomMonster.Count; i++)
            {
                RandomMonster[i].FireStart();

                //나머지는 숨기
                for(int j=0;j< childList.Count; j++)
                {
                    if (childList[j] == RandomMonster[i]|| childList[j].isDead==true) continue;
                    
                        childList[j].HideOnOff(true);
                    
                }

                yield return new WaitForSeconds(2.0f);

                RandomMonster[i].FireEnd();

                //숨은애들 올라옴
                for (int j = 0; j < childList.Count; j++)
                {
                    if (childList[j] == RandomMonster[i] || childList[j].isDead == true) continue;
                    
                        childList[j].HideOnOff(false);
                    
                }
                yield return new WaitForSeconds(1.0f);
            }
                     
            //리소스 해제
            RandomMonster.Clear();
            RandomMonster = null;

            yield break;


        }

        IEnumerator ParentPattern()
        {
            Action(Actions.FireStart);
            yield return new WaitForSeconds(3.0f);
            Action(Actions.FireEnd);
            yield return new WaitForSeconds(1.0f);
        }

        public void Fire1()
        {
            Vector3 PlayerPos = GamePlayerManager.Instance.player.transform.position;
            Vector3 fireDIr = PlayerPos - this.transform.position;

            for (int i = 0; i < 3; i++)
            {
                Vector3 fireDirection = Quaternion.Euler(0f, 0f, -7.5f + i * 7.5f) * fireDIr;

                for (int j = 0; j < 3; j++)
                {
                    Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                    if (bullet != null)
                    {
                        bullet.gameObject.SetActive(true);
                        bullet.Initialize(this.transform.position, fireDirection.normalized, 11f - (float)j, BulletType.EnemyBullet);
                        bullet.InitializeImage("white", false);
                        bullet.SetEffectName("revolver");
                    }
                }
            }
        }




    }
}