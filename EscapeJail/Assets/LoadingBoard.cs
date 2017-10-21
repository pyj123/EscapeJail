﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingBoard : MonoBehaviour
{
    public static LoadingBoard Instance;
    [SerializeField]
    private Image backGround;
    [SerializeField]
    private Text creatingText;
    [SerializeField]
    private Text percentText;

    float fadeTime = 1f;


    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start()
    {

        iTween.FadeTo(this.gameObject, 0f, 5f);
    }

    public void LoadingEnd()
    {
        StartCoroutine(fadeRoutine());
    }

    public void LoadingStart()
    {
        ImageAndTextOnOff(true);
        SetAlphaToImage(1f);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadingStart();
        }

            //임시코드
            if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadingEnd();
        }
    }

    IEnumerator fadeRoutine()
    {
        float count = 0f;
        float alphaValue = 1f;
        while (true)
        {
            alphaValue = Mathf.Lerp(1f, 0f, count / fadeTime);         

            SetAlphaToImage(alphaValue);

             count += Time.deltaTime;

            if (count > fadeTime)
            {
                SetAlphaToImage(1f);
                ImageAndTextOnOff(false);
                yield break;
            }

            yield return null;

        }


    }

    private void SetAlphaToImage(float alpha)
    {
        Color color = new Color(1f, 1f, 1f, alpha);

         if (backGround != null)
            backGround.color = color;

        if (creatingText != null)
            creatingText.color = color;

        if (percentText != null)
            percentText.color = color;
    }



    private void ImageAndTextOnOff(bool OnOff)
    {
        if (backGround != null)
            backGround.gameObject.SetActive(OnOff);

        if (creatingText != null)
            creatingText.gameObject.SetActive(OnOff);

        if (percentText != null)
            percentText.gameObject.SetActive(OnOff);

    }


}