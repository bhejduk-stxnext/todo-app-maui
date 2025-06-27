using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TodoApp.ViewModels.ObjectModel;

public class SortableObservableCollection<T> : ObservableCollection<T>
{
    public void Sort(Func<SortableObservableCollection<T>, IOrderedEnumerable<T>> orderBy)
    {
        var sorted = orderBy(this)
            .ToList();

        for (var i = 0; i < sorted.Count; i++)
        {
            var item = sorted[i];
            var oldIndex = IndexOf(item);

            if (oldIndex != i)
            {
                Debug.WriteLine($"Moved: {oldIndex} to {i}.");
                Move(oldIndex, i);
            }
        }
    }
}