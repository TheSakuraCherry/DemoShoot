using BehaviorDesigner.Runtime.Tasks;
using Core.Character;
using Core.Util;
using DG.Tweening;
using UnityEngine;

namespace Core.AI.Task
{
    public class DestroyBoss : EnemyAction
    {
        public float bleeTime = 2.0f;
        public ParticleSystem bleedEffect;
        public ParticleSystem explodeEffect;

        private bool isDestoryed;

        public override void OnStart()
        {
            EffectManager.Instance.PlayOneShot(bleedEffect,transform.position);
            DOVirtual.DelayedCall(bleeTime, () =>
            {
                EffectManager.Instance.PlayOneShot(explodeEffect, transform.position);
                CameraController.Instance.ShakeCamera(0.7f);
                isDestoryed = true;
                Object.Destroy(gameObject);
            }, false);
        }

        public override TaskStatus OnUpdate()
        {
            return isDestoryed ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}