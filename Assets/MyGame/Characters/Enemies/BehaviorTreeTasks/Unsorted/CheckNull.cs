﻿using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace MyGame.Characters.Enemies.BehaviorTreeTasks
{
    [TaskCategory("ZombieAi/Unsorted")]
    public class CheckNull : Conditional
    {
        public SharedTransform checkedTransform;
        public Condition condition;
        public enum Condition
        {
            Equal,
            NotEqual
        }
        
        public override TaskStatus OnUpdate()
        {
            if (condition == Condition.Equal && checkedTransform.Value == null)
            {
                return TaskStatus.Success;
            }

            if (condition == Condition.NotEqual && checkedTransform.Value != null)
            {
                return TaskStatus.Success;
            }
            
            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            checkedTransform = null;
            condition = Condition.Equal;
        }
    }
}