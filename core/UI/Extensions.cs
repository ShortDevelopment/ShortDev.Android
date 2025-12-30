namespace ShortDev.Android.UI;

public static class Extensions
{
    public static RecyclerViewAdapter<T> CreateAdapter<T>(this IReadOnlyList<T> list, int layoutId, ViewHolderFactory<T> factory)
    {
        return new()
        {
            LayoutId = layoutId,
            Factory = factory,
            ItemsSource = list,
        };
    }

    public static RecyclerViewAdapter<T> CreateAdapter<T>(this IReadOnlyList<T> list, int layoutId, BindViewAction<T> onBind)
        => list.CreateAdapter(layoutId, view => new ActionViewHolder<T>(view) { OnBind = onBind });
}
