using Android.Views;
using AndroidX.RecyclerView.Widget;
using System.Collections.Specialized;

namespace ShortDev.Android.UI;

public sealed class RecyclerViewAdapter<T> : RecyclerView.Adapter
{
    public required AdapterDescriptor<T> Descriptor { get; init; }

    #region Data
    IReadOnlyList<T>? _itemsSource;
    public required IReadOnlyList<T> ItemsSource
    {
        get => _itemsSource ?? throw new NullReferenceException("No Data");
        set
        {
            if (_itemsSource is INotifyCollectionChanged oldObservable)
                oldObservable.CollectionChanged -= OnCollectionChanged;

            bool firstChange = _itemsSource is null;

            _itemsSource = value;

            if (!firstChange)
                NotifyDataSetChanged();

            if (_itemsSource is INotifyCollectionChanged observable)
                observable.CollectionChanged += OnCollectionChanged;
        }
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                NotifyItemInserted(e.NewStartingIndex);
                break;
            case NotifyCollectionChangedAction.Remove:
                NotifyItemRemoved(e.OldStartingIndex);
                break;
            case NotifyCollectionChangedAction.Replace:
                NotifyItemChanged(e.NewStartingIndex);
                break;
            case NotifyCollectionChangedAction.Move:
                NotifyItemMoved(e.OldStartingIndex, e.NewStartingIndex);
                break;
            case NotifyCollectionChangedAction.Reset:
                NotifyDataSetChanged();
                break;
        }
    }
    #endregion

    public override int ItemCount
        => ItemsSource.Count;

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        => Descriptor.InflateAction(holder.ItemView, ItemsSource[position]);

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        var view = LayoutInflater.From(parent.Context)?
            .Inflate(Descriptor.ViewId, parent, false) ?? throw new NullReferenceException("Inflated view was null");
        return new ViewHolder(view);
    }

    sealed class ViewHolder(View view) : RecyclerView.ViewHolder(view) { }
}
