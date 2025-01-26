using Android.Views;

namespace ShortDev.Android.UI;

public delegate void ViewBindAction<T>(View view, int index, T item);
