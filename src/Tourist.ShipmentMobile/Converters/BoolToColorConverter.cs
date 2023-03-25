using System.Globalization;

namespace Tourist.ShipmentMobile.Converters;
public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Color controlEnabledColor = new Color();

        try
        {
            if (parameter is Color)
            {
                controlEnabledColor = (Color)parameter;
            }
            else if (parameter is string)
            {
                var colorString = (string)parameter;
                controlEnabledColor = Color.FromHex(colorString);
            }
            else
            {
                //parameter not in valid format
                throw new NotSupportedException("The parameter passed via ValueConverter is not supported or cannot be converted to a Color.");
            }

            if (value is bool)
            {
                if ((bool)value)
                {
                    return controlEnabledColor;
                }
                else
                {
                    return Color.FromHex("#ffffff");
                }
            }

            return controlEnabledColor;

        }
        catch (NotSupportedException)
        {
            return controlEnabledColor;
        }
        catch (Exception)
        {
            return controlEnabledColor;
        }
    }

    public object ConvertBack(object v, Type tt, object p, CultureInfo c) => null;
}