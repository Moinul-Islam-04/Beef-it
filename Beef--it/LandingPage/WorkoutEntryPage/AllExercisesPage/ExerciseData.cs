using System.Collections.ObjectModel;

namespace Beef__it.Models
{
    public class Exercise
    {
        public string Name { get; set; }
    }

    public class ExerciseGroup : ObservableCollection<Exercise>
    {
        public string GroupName { get; set; }

        public ExerciseGroup(string groupName)
        {
            GroupName = groupName;
        }
    }
}
