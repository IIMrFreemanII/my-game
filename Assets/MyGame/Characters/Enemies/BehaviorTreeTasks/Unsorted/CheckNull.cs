using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace MyGame.Characters.Enemies.BehaviorTreeTasks
{
    [TaskCategory("ZombieAi/Unsorted")]
    public class CheckNull : Conditional
    {
        public SharedObject checkedObject;
        public Condition condition;
        public enum Condition
        {
            Equal,
            NotEqual
        }
        
        public override TaskStatus OnUpdate()
        {
            if (condition == Condition.Equal && checkedObject == null)
            {
                return TaskStatus.Success;
            }

            if (condition == Condition.NotEqual && checkedObject != null)
            {
                return TaskStatus.Success;
            }
            
            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            checkedObject = null;
            condition = Condition.Equal;
        }
    }
}