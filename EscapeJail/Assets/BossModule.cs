﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorDirection
{
    Default,
    Up,
    Down,
    Left,
    Right
}


public class BossModule : MapModuleBase
{
    public DoorDirection doorDirection;

    public BossBase bossBase;

    private void Awake()
    {
        normalTileList = new List<Tile>();

        Tile[] tiles = GetComponentsInChildren<Tile>();
        for(int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tileType == TileType.Normal)
                normalTileList.Add(tiles[i]);
            else if (tiles[i].tileType == TileType.Wall)
                tiles[i].transform.gameObject.AddComponent<BoxCollider2D>();
                
        }

        bossBase = GetComponentInChildren<BossBase>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (bossBase != null)
                bossBase.StartBossPattern();
        }
    }

    protected new void OnTriggerStay2D(Collider2D collision)
    {

    }
}
