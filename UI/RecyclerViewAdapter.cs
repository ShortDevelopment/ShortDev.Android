using Android.Views;
using AndroidX.RecyclerView.Widget;
using System.Collections.Specialized;

namespace ShortDev.Android.UI;

public sealed class RecyclerViewAdapter<T> : RecyclerView.Adapter
{
    public required int LayoutId { get; init; }
    public required ViewHolderFactory<T> Factory { get; init; }

    #region Data
    public required IReadOnlyList<T> ItemsSource
    {
        get => field ?? throw new NullReferenceException("No Data");
        set
        {
            if (field is INotifyCollectionChanged oldObservable)
                oldObservable.CollectionChanged -= OnCollectionChanged;

            bool firstChange = field is null;

            field = value;

            if (!firstChange)
                NotifyDataSetChanged();

            if (field is INotifyCollectionChanged observable)
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

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        var view = LayoutInflater.From(parent.Context)?
            .Inflate(LayoutId, parent, false) ?? throw new NullReferenceException("Inflated view was null");
        return Factory(view);
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
        if (holder is not ViewHolder<T> viewHolder)
            return;

        viewHolder.Bind(position, ItemsSource[position]);
    }

    public override void OnViewRecycled(Java.Lang.Object holder)
    {
        if (holder is not ViewHolder<T> viewHolder)
            return;

        viewHolder.Recycle();
    }
}
