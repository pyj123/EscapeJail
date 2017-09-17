﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MouseBoss : BossBase
{
    private GameObject mouseHandPrefab;
    private ObjectPool<MouseHand> mouseHandPool;

    //컴포넌트
    private Animator animator;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    //패턴 시간
    private float digPatternLastTime = 5f;
    private float digPatternAttackSpeed = 0.5f;
    private float idleLastTime = 3f;
    private enum Actions
    {
        Dig,
        DigOut,
        Walk,
        PatternEnd,
        Die
    }

    public enum Pattern
    {
        Idle,
        DigPattern,
        RushPattern,
        FirePattern
    }

    private void Action(Actions action)
    {
        switch (action)
        {
            case Actions.Dig:
                {
                    if (animator != null)
                        animator.SetTrigger("Dig");
                }
                break;
            case Actions.DigOut:
                {
                    if (animator != null)
                        animator.SetTrigger("Out");
                }
                break;
            case Actions.Walk:
                {
                    if (animator != null)
                        animator.SetFloat("Speed", 1.0f);
                }
                break;
            case Actions.Die:
                {
                    if (animator != null)
                        animator.SetTrigger("Die");
                }
                break;
        }
    }

    private new void Awake()
    {
        base.Awake();
        LoadPrefab();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LoadPrefab()
    {

        mouseHandPrefab = Resources.Load("Prefabs/Monsters/Boss/Mouse/MouseHand") as GameObject;
        if (mouseHandPrefab != null)
        {
            mouseHandPool = new ObjectPool<MouseHand>(bossModule.transform, mouseHandPrefab, 10);
            // Debug.Log("풀만들어짐");
        }
    }

    public override void StartBossPattern()
    {
        if (isPatternStart == true) return;
        isPatternStart = true;

        Debug.Log("BossPatternStart");

          Action(Actions.Dig);
       // StartFirePattern();


    }

    //애니메이션에 호출 연결되어있음
    public void StartDigPattern()
    {
        StartCoroutine(BossPattern(Pattern.DigPattern));
        HideOn();
    }

    public void StartFirePattern()
    {
        StartCoroutine(BossPattern(Pattern.FirePattern));
    }

    private void HideOn()
    {
        if (boxCollider != null)
            boxCollider.enabled = false;

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;


    }
    private void HideOff()
    {
        if (boxCollider != null)
            boxCollider.enabled = true;

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;
    }

 
    public IEnumerator BossPattern(Pattern pattern)
    {
        float patternCount = 0f;

        switch (pattern)
        {
            case Pattern.Idle:
                {
                    yield return new WaitForSeconds(idleLastTime);
                }
                break;
            #region DigPattern
            case Pattern.DigPattern:
                {
                    while (true)
                    {
                        if (bossModule.NormalTileList != null)
                        {
                            Tile RandomTile = bossModule.NormalTileList[Random.Range(0, bossModule.NormalTileList.Count)];

                            if (mouseHandPool != null)
                            {
                                MouseHand mouseHand = mouseHandPool.GetItem();
                                mouseHand.transform.position = RandomTile.transform.position + Vector3.up * 0.35f;
                            }
                        }
                        patternCount += digPatternAttackSpeed;

                        if (patternCount >= digPatternLastTime)
                        {
                            HideOff();
                            Action(Actions.DigOut);
                            StartFirePattern();
                            break;
                        }

                        yield return new WaitForSeconds(digPatternAttackSpeed);
                    }
                }
                break;
            #endregion
            case Pattern.FirePattern:
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 36; j++)
                        {
                            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
                            if (bullet != null)
                            {
                                bullet.gameObject.SetActive(true);
                                Vector3 fireDIr;

                                if (i % 2 == 0)
                                    fireDIr = Vector3.right;
                                else
                                    fireDIr = Quaternion.Euler(0f, 0f, 5f) * Vector3.right;

                                fireDIr = Quaternion.Euler(0f, 0f, j * 10f) * fireDIr;
                                bullet.Initialize(this.transform.position, fireDIr.normalized, 3f, BulletType.EnemyBullet);
                                bullet.InitializeImage("white", false);
                                bullet.SetEffectName("revolver");
                            }
                        }
                       yield return new WaitForSeconds(0.7f);
                    }

                    Action(Actions.Dig);
                }
                break;
            case Pattern.RushPattern:
                {

                }
                break;

        }

        //랜덤패턴 실행



    }
}
