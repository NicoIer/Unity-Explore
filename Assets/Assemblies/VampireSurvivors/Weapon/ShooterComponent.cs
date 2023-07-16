using System;
using Cysharp.Threading.Tasks;
using Nico;
using UnityEngine;

namespace VampireSurvivors
{
    public class ShooterComponent
    {
        private readonly ShooterSetting _setting;
        private bool _canShoot;
        private readonly Func<Transform> _findTarget;
        public ShooterComponent(ShooterSetting setting,Func<Transform> findTarget)
        {
            this._setting = setting;
            this._findTarget = findTarget;
            _canShoot = true;
        }
        public async void Shoot(Vector3 start)
        {
            if (!_canShoot)
            {
                return;
            }
            Transform tar = _findTarget();
            GameObject bulletObj = ObjectPoolManager.Get(_setting.bulletName);
            bulletObj.transform.position = start;
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.Init(tar);
            _canShoot = false;
            await UniTask.Delay(TimeSpan.FromSeconds(_setting.coolDown));
            _canShoot = true;
        }
    }
}