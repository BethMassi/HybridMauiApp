using System.Diagnostics;

namespace HybridMauiApp
{
    public class TodoDataStore
    {
        private readonly List<TodoTask> _taskData =
        [
            new(){ id="todo-0", title="Eat", is_completed=true  },
            new(){ id="todo-1", title="Sleep", is_completed=false  },
            new(){ id="todo-2", title="Repeat", is_completed=false  },
        ];

        public event EventHandler? TaskDataChanged;

        private void OnTaskDataChanged()
        {
            TaskDataChanged?.Invoke(this, EventArgs.Empty);
        }

        public List<TodoTask> GetData() => _taskData;

        public void SetData(TodoTask[] tasks)
        {
            Debug.WriteLine($"SetData: {tasks.Length} tasks");
            _taskData.Clear();
            _taskData.AddRange(tasks);
            OnTaskDataChanged();
        }
    }
}
