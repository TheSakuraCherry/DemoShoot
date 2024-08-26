using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI.Task
{
    public class GotoNextStage : EnemyAction
    {
        public SharedInt CurrentStage;

        public override TaskStatus OnUpdate()
        {
            CurrentStage.Value++;
            return TaskStatus.Success;
        }
    }
}