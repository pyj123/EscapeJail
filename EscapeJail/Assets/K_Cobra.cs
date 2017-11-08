﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace weapon
{
    public class K_Cobra : Weapon
    {

        //리볼버 반동
        private float reBoundValue = 5f;

        public K_Cobra()
        {
            weapontype = WeaponType.K_Cobra;
            bulletSpeed = 12f;
            fireDelay = 1f;
            SetAmmo(100);
            needBulletToFire = 1;
            damage = 3;

        }

        public override void FireBullet(Vector3 firePos, Vector3 fireDirection)
        {
            if (canFire() == false) return;

            FireDelayOn();
            PlayFireAnim();
            useBullet();
            Bullet bullet = ObjectManager.Instance.bulletPool.GetItem();
            if (bullet != null)
            {

                Vector3 fireDir = fireDirection;
                fireDir = Quaternion.Euler(0f, 0f, Random.Range(-reBoundValue, reBoundValue)) * fireDir;
                bullet.Initialize(firePos, fireDir.normalized, bulletSpeed, BulletType.PlayerBullet, 0.5f, damage);
                bullet.InitializeImage("white", false);
                bullet.SetEffectName("revolver");
                bullet.SetBloom(true, Color.yellow);



            }

        }
    }
}