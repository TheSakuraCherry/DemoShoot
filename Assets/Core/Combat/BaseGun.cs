using System;
using Core.Character;
using Core.Combat.Projectiles;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Core.Combat
{
    public class BaseGun : MonoBehaviour
    {
        private float timer = 0;
        public float rate = 10;//子弹发射速率
        public Transform bulletPoint;//子弹发射点
        public float offset = 0.5f;
        public Bullet bulletPrefab;
        public ParticleSystem Muzzle;
        public bool CanShoot;
        public Animator GunAnim;
        public float probabilityThreshold = 0.2f; // 设定的概率阈值（例如50%的概率）

        private void Awake()
        {
            Muzzle.Stop();
            CanShoot = true;
        }

        private void Update()
        {
            if (!CanShoot)return;
            if(Input.GetMouseButton(0))
            {
                Shoot();
                Muzzle.transform.localScale = transform.localScale;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Muzzle.Stop();
                GunAnim.SetBool("Shooting",false);
            }
        }
        public void Shoot()
        {
            timer += Time.deltaTime; // 计时器计时
            if (timer > 1f / rate) // 如果计时大于子弹的发射速率（rate每秒几颗子弹）
            {
                // 生成一个随机的Z轴旋转角度
                float randomChance = Random.value;
                Quaternion randomRotation = Quaternion.identity;
                if (randomChance < probabilityThreshold)
                {
                    float randomZRotation = Random.Range(-5f, 5f);
                    randomRotation = Quaternion.Euler(0, 0, randomZRotation);
                }
                var bullet = ObjectPoolManager.Instance.SpawnObject<Bullet>(
                    bulletPrefab.gameObject,
                    bulletPoint.position,
                    randomRotation, // 使用随机旋转角度
                    ObjectPoolManager.PoolType.Gameobject
                );
                if (transform.localScale.x < 0)
                {
                    bullet.SetForce(-bullet.transform.right);
                }
                else
                {
                    bullet.SetForce(bullet.transform.right);
                }
                bullet.Shooter = this.gameObject;
                CameraController.Instance.ShakeCamera(0.05f, 0.5f);
                transform.position -= new Vector3(transform.localScale.x * 0.05f, 0, 0);
                if (!Muzzle.isPlaying)
                {
                    Muzzle.Play();
                }
                GunAnim.SetBool("Shooting", true);
                timer = 0; // 时间清零
            }
        }
    }
}