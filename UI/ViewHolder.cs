using Android.Views;
using AndroidX.RecyclerView.Widget;

namespace ShortDev.Android.UI;

public abstract class ViewHolder<T>(View view) : RecyclerView.ViewHolder(view)
{
    public abstract void Bind(int index, T item);

    public virtual void Recycle() { }
}

internal sealed class ActionViewHolder<T>(View view) : ViewHolder<T>(view)
{
    public required BindViewAction<T> OnBind { get; init; }

    public override void Bind(int index, T item)
        => OnBind(ItemView, item, index);
}
