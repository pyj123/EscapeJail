﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool
{
    private Dictionary<MonsterName, ObjectPool<MonsterBase>> pool ;

    private Transform parent;

    private StageData nowStageData;
    public MonsterPool(Transform monsterParent, StageData stageData)
    {
        pool = new Dictionary<MonsterName, ObjectPool<MonsterBase>>();
        parent = monsterParent;
        nowStageData = stageData;

        Initialize();
    }

    public void ReleaseMonsterPool()
    {
        if (pool == null) return;

        foreach(KeyValuePair<MonsterName,ObjectPool<MonsterBase>> data in pool)
        {
            data.Value.ReleasePool();
        }
        pool.Clear();
        pool = null;
    }

    public void Initialize()
    {
    
        if (nowStageData == null) return;

        if (nowStageData.spawnEnemyList == null) return;

        for (int i = 0; i < nowStageData.spawnEnemyList.Count; i++)
        {
            MonsterName monsterName = nowStageData.spawnEnemyList[i];
            ObjectPool<MonsterBase> monsterPool = null;

            GameObject obj = (GameObject)Resources.Load("Prefabs/Monsters/" + monsterName.ToString());

            if (obj == null) return;

            monsterPool = new ObjectPool<MonsterBase>(parent, obj, 1);

            if (monsterPool != null)
                pool.Add(monsterName, monsterPool);
        }
    }

    public MonsterBase GetRandomMonster()
    {
        if (pool == null) return null;

        List<MonsterName> keyList = new List<MonsterName>(pool.Keys);
        MonsterName RandomKey = keyList[UnityEngine.Random.Range(0, keyList.Count)];
        return pool[RandomKey].GetItem();
    }




}
