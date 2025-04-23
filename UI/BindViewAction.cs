using Android.Views;

namespace ShortDev.Android.UI;

public delegate void BindViewAction<T>(View view, T item, int index);
