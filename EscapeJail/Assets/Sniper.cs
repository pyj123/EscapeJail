﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : CharacterBase
{
    private bool nowUsingSkill = false;
    private float slowTimeRatio = 0.2f;
    private int snipingLayerMask;
    private int snipingPower = 30;

    private int maxBullet = 5;
    private int nowBullet = 5;

    private float requireTimeforReload = 5f;

    private IEnumerator ReLoadRoutine()
    {
        float count = 0f;
        while (true)
        {
            if (nowUsingSkill == true || AmmoisFull() == true)
            {
                yield return null;
                continue;
            }

            count += Time.deltaTime;

            if (count >= requireTimeforReload)
            {
                count = 0;
                GetBullet(1);
            }

            //ui 업데이트
            if (playerUi != null)
                playerUi.SetSkillButtonProgress(count, requireTimeforReload);

            yield return null;
        }
    }
    private bool CanFire()
    {
        return nowBullet > 0;
    }

    private void UseBullet()
    {
        nowBullet -= 1;
        UpdateSkillUi(nowBullet.ToString());

        if (nowBullet == 0)
            SkillOff();
    }

    private void GetBullet(int num)
    {
        nowBullet += num;
        if (nowBullet >= maxBullet)
            nowBullet = maxBullet;

        UpdateSkillUi(nowBullet.ToString());
    }
    private void UpdateSkillUi(string text)
    {
        if (playerUi != null)
            playerUi.SetSkillButtonText(text);
    }

    private bool AmmoisFull()
    {
        return nowBullet >= maxBullet;
    }

    private void SetBullet(int bulletNum)
    {
        maxBullet = bulletNum;
        nowBullet = bulletNum;
    }



    private new void Awake()
    {
        base.Awake();
        SetHp(10);
        SetBullet(5);

        snipingLayerMask = MyUtils.GetLayerMaskByString("Enemy");
    }

    private new void Start()
    {
        base.Start();
        SetWeapon();
        StartCoroutine(ReLoadRoutine());
        originSpeed = moveSpeed;
        UpdateSkillUi(nowBullet.ToString());
    }

    private new void Update()
    {
        base.Update();

    }

    public IEnumerator SkillRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (CanFire() == true)
                {


#if UNITY_EDITOR
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D rayHit = Physics2D.Raycast(touchPos, Vector2.zero, 0.1f, snipingLayerMask);
                    if (rayHit.collider != null)
                    {
                        CharacterInfo monster = rayHit.collider.gameObject.GetComponent<CharacterInfo>();
                        if (monster != null)
                        {
                            if (monster.IsDead == false)
                            {
                                UseBullet();
                                monster.GetDamage(snipingPower);
                            }
                        }

                        //이펙트 호출
                        ExplosionEffect effect = ObjectManager.Instance.effectPool.GetItem();
                        if (effect != null)
                        {
                            effect.Initilaize(rayHit.transform.position, "SniperAim", 0.5f, 2f);
                            effect.SetAlpha(150f);
                        }

                    }
#else
                    Touch[] touches = Input.touches;
                if (touches != null)
                {
                    for (int i = 0; i < touches.Length; i++)
                    {
                        Vector2 touchPos = Camera.main.ScreenToWorldPoint(touches[i].position);
                        RaycastHit2D rayHit = Physics2D.Raycast(touchPos, Vector2.zero, 0.1f, snipingLayerMask);
                        if (rayHit.collider != null)
                        {
                            CharacterInfo monster = rayHit.collider.gameObject.GetComponent<CharacterInfo>();
                           if (monster != null)
                        {
                            if (monster.IsDead == false)
                            {
                                UseBullet();
                                monster.GetDamage(snipingPower);
                            }
                        }
                            //이펙트 호출
                            ExplosionEffect effect = ObjectManager.Instance.effectPool.GetItem();
                            if (effect != null)
                            {
                                effect.Initilaize(rayHit.transform.position, "SniperAim", 0.5f, 2f);
                                effect.SetAlpha(150f);
                            }
                            break;
                        }
                    }
                }
#endif
                }


            }

            yield return null;
        }
    }

    public override void UseCharacterSkill()
    {
        if (nowUsingSkill == true)
        {
            SkillOff();
        }
        else if (nowUsingSkill == false)
        {
            SkillOn();
        }
    }

    private void SkillOn()
    {
        if (CanFire() == false) return;

        nowUsingSkill = true;
        CameraController.Instance.SniperAimEffectOnOff(true);

        if (weaponHandler != null)
            weaponHandler.gameObject.SetActive(false);

        if (animator != null)
        {
            animator.speed = 2f;
            animator.SetTrigger("FireReadyTrigger");
        }

        if (rb != null)
            rb.velocity = Vector3.zero;

        TimeManager.Instance.BulletTimeOn(slowTimeRatio);

        StartCoroutine("SkillRoutine");

    }
    private void SkillOff()
    {
        nowUsingSkill = false;
        CameraController.Instance.SniperAimEffectOnOff(false);

        if (weaponHandler != null)
            weaponHandler.gameObject.SetActive(true);

        if (animator != null)
        {
            animator.speed = 1f;
            animator.SetTrigger("SkillEnd");
        }

        TimeManager.Instance.BulletTimeOff();
        StopCoroutine("SkillRoutine");
    }

    public override void FireWeapon()
    {
        if (nowUsingSkill == true) return;
        base.FireWeapon();
    }

    protected override void MoveInMobie()
    {
        if (nowUsingSkill == true) return;
        base.MoveInMobie();
    }

    protected override void MoveInPc()
    {
        if (nowUsingSkill == true) return;
        base.MoveInPc();
    }
}