using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Core.AI.Task
{
    public class FreezeTime : EnemyAction
    {
        public SharedFloat Duration = 0.1f;

        public override TaskStatus OnUpdate()
        {
            GameManager.Instance.FreezeTime(Duration.Value);
            return TaskStatus.Success;
        }
    }
}