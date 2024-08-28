using System;
using Core.Character;
using Core.Combat.Projectiles;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Combat
{
    public class BaseGun : MonoBehaviour
    {
        private float timer = 0;
        public float rate = 10;//子弹发射速率
        public Transform bulletPoint;//子弹发射点
        public float offset = 0.5f;
        public Bullet bulletPrefab;

        private void Update()
        {
            if(Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        public void Shoot()
        {
            timer += Time.deltaTime;//计时器计时
            if (timer > 1f / rate)//如果计时大于子弹的发射速率（rate每秒几颗子弹）
            {
                var bullet = ObjectPoolManager.Instance.SpawnObject<Bullet>(bulletPrefab.gameObject,
                    bulletPoint.position, Quaternion.identity,ObjectPoolManager.PoolType.Gameobject);
                bullet.SetForce(new Vector2(transform.localScale.x,0));
                bullet.Shooter = this.gameObject;
                CameraController.Instance.ShakeCamera(0.05f, 0.5f);
                timer = 0;//时间清零
                
            }
            
        }
    }
}