using Android.Views;

namespace ShortDev.Android.UI;

public sealed class ListAdapter<T>(AdapterDescriptor<T> descriptor, IReadOnlyList<T> data) : BaseAdapter<T>
{
    public AdapterDescriptor<T> Descriptor { get; } = descriptor;
    public IReadOnlyList<T> Data { get; } = data;

    public ListAdapter(AdapterDescriptor<T> descriptor, IEnumerable<T> data) : this(descriptor, new List<T>(data)) { }

    public override T this[int position]
        => Data[position];

    public override int Count
        => Data.Count;

    public override long GetItemId(int position)
        => position;

    public override View GetView(int position, View? convertView, ViewGroup? parent)
    {
        convertView ??= LayoutInflater.From(parent!.Context)!.Inflate(Descriptor.ViewId, null, false)!;
        Descriptor.InflateAction(convertView!, this[position]);
        return convertView;
    }
}
