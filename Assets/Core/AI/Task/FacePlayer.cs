﻿using BehaviorDesigner.Runtime.Tasks;

namespace Core.AI.Task
{
    public class FacePlayer : EnemyAction
    {
        private float baseScaleX;
        public override void OnAwake()
        {
            base.OnAwake();
            baseScaleX = transform.localScale.x;
        }

        public override TaskStatus OnUpdate()
        {
            if (player)
            {
                var scale = transform.localScale;
                scale.x = transform.position.x > player.transform.position.x ? -baseScaleX : baseScaleX;
                transform.localScale = scale;
            }
            return TaskStatus.Success;
        }
    }
}