using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using UnityEngine;

public class IsHealthUnder : EnemyConditional
{
   public SharedInt HealthTreshold;
   public override TaskStatus OnUpdate()
   {
      return destructable.CurrentHealth < HealthTreshold.Value ? TaskStatus.Success : TaskStatus.Failure;
   }
}
