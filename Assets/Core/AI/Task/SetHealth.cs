﻿using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Core.AI.Task
{
    public class SetHealth : EnemyAction
    {
        public SharedInt Health;

        public override TaskStatus OnUpdate()
        {
            destructable.CurrentHealth = Health.Value;
            return TaskStatus.Success;
        }
    }
}