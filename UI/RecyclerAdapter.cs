using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace ShortDev.Android.UI;

public sealed class RecyclerViewAdapter<T>(AdapterDescriptor<T> descriptor, IReadOnlyList<T> data) : RecyclerView.Adapter
{
    public AdapterDescriptor<T> Descriptor { get; } = descriptor;
    public IReadOnlyList<T> Data { get; } = data;

    public RecyclerViewAdapter(AdapterDescriptor<T> descriptor, IEnumerable<T> data) : this(descriptor, new List<T>(data)) { }

    public override int ItemCount
        => Data.Count;

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        => Descriptor.InflateAction(holder.ItemView, Data[position]);

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        var view = LayoutInflater.From(parent.Context)!.Inflate(Descriptor.ViewId, parent, false);
        return new ViewHolder(view!);
    }

    sealed class ViewHolder(View view) : RecyclerView.ViewHolder(view)
    {
    }
}
