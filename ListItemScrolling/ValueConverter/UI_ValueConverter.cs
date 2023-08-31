using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ListItemScrolling.ValueConverter
{
    /// <summary>
    /// An UI logic converter.
    /// </summary>
    public class ButtonVisibilityInterlock_Converter : IMultiValueConverter
    {
        /// <summary>
        /// Instantiate a static converter object.
        /// <para>As I use it as static resource.</para>
        /// </summary>
        public static readonly ButtonVisibilityInterlock_Converter Instance = new();

        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            foreach(bool isVisible in values)
            {
                if (isVisible)
                    return false;
            }
            return true;
        }
    }
}