using Android.Views;

namespace ShortDev.Android.UI;

public sealed class AdapterDescriptor<T>(int viewId, AdapterDescriptor<T>.ListAdapterInflateAction inflateAction)
{
    public int ViewId { get; } = viewId;
    public ListAdapterInflateAction InflateAction { get; } = inflateAction;

    public delegate void ListAdapterInflateAction(View view, T item);

    public ListAdapter<T> CreateListAdapter(IEnumerable<T> data)
        => new(this, data);

    public RecyclerViewAdapter<T> CreateRecyclerViewAdapter(IReadOnlyList<T> data)
        => new() { Descriptor = this, ItemsSource = data };
}
