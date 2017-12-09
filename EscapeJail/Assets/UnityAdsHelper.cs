﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsHelper : MonoBehaviour
{
                                        //고유 아이디
    private const string android_game_id = "1613609";

    private const string rewarded_video_id = "rewardedVideo";

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
#if UNITY_ANDROID
        Advertisement.Initialize(android_game_id);
#endif
    }

    //광고 송출
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady(rewarded_video_id))
        {                                                            //콜백 등록
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(rewarded_video_id, options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {             

                   
                    // 광고 시청이 완료되었을 때 처리

                    break;
                }
            case ShowResult.Skipped:
                {
                

                 
                    // 광고가 스킵되었을 때 처리

                    break;
                }
            case ShowResult.Failed:
                {
            

                  
                    // 광고 시청에 실패했을 때 처리

                    break;
                }
        }
    }


}
