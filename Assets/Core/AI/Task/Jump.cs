﻿using BehaviorDesigner.Runtime.Tasks;
using Core.Character;
using DG.Tweening;
using UnityEngine;

namespace Core.AI.Task
{
    public class Jump : EnemyAction
    {
        public float horizontalForce = 5.0f;
        public float jumpForce = 10.0f;

        public float buildupTime;
        public float jumpTime;

        public string animationTriggerName;
        public bool shakeCameraOnLanding;

        private Tween buildupTween;
        private Tween jumpTween;

        private bool hasLanded;

        public override void OnStart()
        {
           buildupTween =  DOVirtual.DelayedCall(buildupTime, StartJump, false);
            animator.SetTrigger(animationTriggerName);
        }

        public override TaskStatus OnUpdate()
        {

            return hasLanded ? TaskStatus.Success : TaskStatus.Running;
        }

        private void StartJump()
        {
            var direction = player.transform.position.x < transform.position.x ? -1 : 1;
            body.AddForce(new Vector2(horizontalForce * direction,jumpForce),ForceMode2D.Impulse);
           jumpTween =  DOVirtual.DelayedCall(jumpTime, () =>
            {
                hasLanded = true;
                if(shakeCameraOnLanding)
                CameraController.Instance.ShakeCamera(0.5f);
            }, false);
        }

        public override void OnEnd()
        {
            buildupTween?.Kill();
            jumpTween?.Kill();
            hasLanded = false;
        }
    }
}