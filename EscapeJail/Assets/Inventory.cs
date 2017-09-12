﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using weapon;
public class Inventory 
{
    private List<Weapon> weaponList = new List<Weapon>();
    private int weaponIndex = -1;
   

    public Weapon GetWeapon()
    {
        if (weaponList == null) return null;
        if (weaponList.Count == 0) return null;

        weaponIndex++;

        if (weaponIndex >= weaponList.Count)
            weaponIndex = 0;

        return weaponList[weaponIndex];
    }
    // Use this for initialization

    public void AddWeapon(Weapon weapon)
    {
        if (weaponList != null && weapon != null)
            weaponList.Add(weapon);
    }

    ~Inventory()
    {

    }
}