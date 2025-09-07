using System;
using System.Globalization;
using System.Windows.Data;

namespace HospitalManagementSystem.Utils;
public sealed class DateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is DateOnly d ? (DateTime?)d.ToDateTime(TimeOnly.MinValue) : null;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is DateTime dt ? (DateOnly?)DateOnly.FromDateTime(dt) : null;
}