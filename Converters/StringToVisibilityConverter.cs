﻿namespace Taggle.Converters;

/// <summary>
/// Used in Views to set <c>IsVisible</c> of string controls
/// based on the given value. If the string is empty or null, will hide the control.
/// </summary>
public class StringToVisibilityConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
		!string.IsNullOrEmpty(value as string);

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
		throw new NotImplementedException();
}
