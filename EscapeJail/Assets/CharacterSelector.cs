﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject characterSlotPrefab;

    [SerializeField]
    private GridLayoutGroup grid;

    private RectTransform rectTr;

    private void Awake()
    {
        rectTr = grid.GetComponent<RectTransform>();
    }

    private void Start()
    {
        MakeCharacterSlots();
    }

    private void MakeCharacterSlots()
    {
        if (characterSlotPrefab == null) return;

        for(int i = 0; i < (int)CharacterType.CharacterEnd; i++)
        {
            GameObject makeObj = GameObject.Instantiate(characterSlotPrefab,this.transform);
            if (makeObj != null)
            {
                CharacterSlot_Ui slot = makeObj.GetComponent<CharacterSlot_Ui>();
                if (slot != null)
                {
                    slot.Initialize((CharacterType)i);
                }
            }
        }

        float eachDistance = grid.cellSize.y + grid.spacing.y;
        if (rectTr != null)
        {
            rectTr.sizeDelta = new Vector2(eachDistance * ((int)CharacterType.CharacterEnd), 100f);
        }

    }

    private void ChangeScene()
    {    
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)SceneName.GameScene);

    }
}
