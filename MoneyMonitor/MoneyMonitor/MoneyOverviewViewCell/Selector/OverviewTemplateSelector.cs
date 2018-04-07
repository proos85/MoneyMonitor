using System;
using MoneyMonitor.Data.Dto;
using MoneyMonitor.ViewModel;
using Xamarin.Forms;

namespace MoneyMonitor.MoneyOverviewViewCell.Selector
{
    public class OverviewTemplateSelector: DataTemplateSelector
    {
        private readonly DataTemplate _fixedViewCell;
        private readonly DataTemplate _variableViewCell;
        private readonly DataTemplate _charityViewCell;

        public OverviewTemplateSelector()
        {
            _fixedViewCell = new DataTemplate(typeof(FixedViewCell));
            _variableViewCell = new DataTemplate(typeof(VariableViewCell));
            _charityViewCell = new DataTemplate(typeof(CharityViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var vm = (MoneyExpenseViewModel) item;
            switch (vm.TypeExpense)
            {
                case ExpenseTypes.Fixed: return _fixedViewCell;
                case ExpenseTypes.Variable: return _variableViewCell;
                case ExpenseTypes.Charity: return _charityViewCell;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
