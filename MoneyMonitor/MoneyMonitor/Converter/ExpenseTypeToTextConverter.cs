using System;
using System.Globalization;
using MoneyMonitor.Data.Dto;
using Xamarin.Forms;

namespace MoneyMonitor.Converter
{
    public class ExpenseTypeToTextConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueString = value?.ToString();
            if (string.IsNullOrEmpty(valueString))
            {
                return value;
            }

            if (Enum.TryParse(valueString, true, out ExpenseTypes expenseTypes))
            {
                switch (expenseTypes)
                {
                    case ExpenseTypes.Fixed:
                        return "Vast";
                    case ExpenseTypes.Variable:
                        return "Variabel";
                    case ExpenseTypes.Charity:
                        return "Goed doel";
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}