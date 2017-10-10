﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState
{
    FollowPlayer,
    Shake
}

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private CameraState cameraState;

    private Transform target;


    //흔들기전용
    private Vector3 originPosit;
    private float shakeIntensity;
    private float shakeTime;
    private float shakeCount;


    [SerializeField]
    private float followSpeed = 2;

    private void Awake()
    {
        Instance = this;
    }

    private void ChangeCameraMode(CameraState cameraState)
    {
        this.cameraState = cameraState;

        if (cameraState == CameraState.Shake)
            originPosit = this.transform.position;
    }

    private void LinkPlayer()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    public void ShakeCamera(float intensity, float shakeTime)
    {
        ChangeCameraMode(CameraState.Shake);

        shakeIntensity = intensity;
        this.shakeTime = shakeTime;
        shakeCount = 0f;
    }

    private void Start()
    {

        LinkPlayer();

        ChangeCameraMode(CameraState.FollowPlayer);
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        switch (cameraState)
        {
            case CameraState.FollowPlayer:
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, target.position, Time.fixedDeltaTime * followSpeed);
                } break;
            case CameraState.Shake:
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, target.position, Time.fixedDeltaTime * followSpeed);
                    this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + (Vector3)Random.insideUnitCircle*shakeIntensity, Time.deltaTime);

                    //타이머

                    shakeCount += Time.fixedDeltaTime;
                    if (shakeCount >= shakeTime)
                    {
                        ChangeCameraMode(CameraState.FollowPlayer);
                    }

                }
                break;
        }

      
    }



    

}