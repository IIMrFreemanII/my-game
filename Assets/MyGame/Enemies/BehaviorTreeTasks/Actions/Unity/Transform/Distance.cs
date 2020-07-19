using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace MyGame.Enemies.BehaviorTreeTasks.Actions.Unity.Transform
{
    [TaskCategory("Unity/Transform")]
    [TaskDescription("Compares distance between two transforms by condition. if true returns success.")]
    public class Distance : Conditional
    {
        public enum Condition
        {
            LessOrEqual,
            MoreOrEqual,
        }
        
        public SharedTransform transform1;
        public SharedTransform transform2;

        public Condition condition;

        public SharedFloat distance;

        public override TaskStatus OnUpdate()
        {
            if (transform1.Value && transform2.Value)
            {
                switch (condition)
                {
                    case Condition.LessOrEqual:
                    {
                        if (Vector3.Distance(transform1.Value.position, transform2.Value.position) <= distance.Value)
                        {
                            return TaskStatus.Success;
                        }
                        break;
                    }
                    case Condition.MoreOrEqual:
                    {
                        if (Vector3.Distance(transform1.Value.position, transform2.Value.position) >= distance.Value)
                        {
                            return TaskStatus.Success;
                        }
                        break;
                    }
                    default:
                    {
                        return TaskStatus.Failure;
                    }
                }
            }

            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            transform1 = null;
            transform2 = null;
            distance = 0;
            condition = Condition.LessOrEqual;
        }
    }
}