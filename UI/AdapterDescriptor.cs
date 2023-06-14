﻿using Android.Views;

namespace ShortDev.Android.UI;

public sealed class AdapterDescriptor<T>
{
    public AdapterDescriptor(int viewId, ListAdapterInflateAction inflateAction)
    {
        ViewId = viewId;
        InflateAction = inflateAction;
    }

    public int ViewId { get; }
    public ListAdapterInflateAction InflateAction { get; }

    public delegate void ListAdapterInflateAction(View view, T item);

    public ListAdapter<T> CreateListAdapter(IEnumerable<T> data)
        => new(this, data);

    public RecyclerViewAdapter<T> CreateRecyclerViewAdapter(IEnumerable<T> data)
        => new(this, data);
}
