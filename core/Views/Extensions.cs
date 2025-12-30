using Android.Views;
using System.Runtime.CompilerServices;

namespace ShortDev.Android.Views;

public static class Extensions
{
    extension(View view)
    {
        /// <summary>
        /// Finds a view with the specified resource ID and returns it if found. <br/>
        /// Throws an exception if the view is not present.
        /// </summary>
        /// <typeparam name="T">The type of the view to return. Must be a subclass of <see cref="View"/>.</typeparam>
        /// <param name="id">The resource identifier of the view to find.</param>
        /// <returns>The view associated with the specified resource ID.</returns>
        /// <exception cref="InvalidOperationException">Thrown if a view with the specified resource ID is not found.</exception>
        public T FindRequiredViewById<T>(int id, [CallerArgumentExpression(nameof(id))] string idExpression = "") where T : View
            => view.FindViewById<T>(id) ?? throw new InvalidOperationException($"View with id '{idExpression}' not found.");
    }
}
