using System.ComponentModel;
using NUnit.Framework;
using Rhino.Mocks;

namespace CSRotoZoomer.Tests
{
    public abstract class PropertyChangedAutoPropertyTestsBase<TViewModel, TModel, TTestValue> : PropertyChangedTestsBase<TViewModel, TModel> 
        where TViewModel : INotifyPropertyChanged 
        where TModel : class
    {
        private readonly string _propertyName;
        public TTestValue TestValue { get; private set; }

        protected PropertyChangedAutoPropertyTestsBase(string propertyName, TTestValue testValue)
        {
            _propertyName = propertyName;
            TestValue = testValue;
        }

        [Test]
        public void when_delta_gamma_set_will_file_property_changed_event()
        {
            AssertPropertyChangedRaisedFor(SetViewModelProperty, _propertyName);
        }

        [Test]
        public void when_delta_gamma_set_will_delegate_to_roto_zoomer()
        {
            WhenPropertySetWillDelegate(
                SetViewModelProperty,
                SetModelProperty);
        }

        [Test]
        public void when_delta_gamma_set_to_existing_value_will_not_set_again()
        {
            WhenAlreadySetWillNotDelegate(
                GetModelProperty,
                SetViewModelProperty,
                TestValue,
                SetModelProperty);

        }

        [Test]
        public void when_delta_gamma_set_to_existing_value_will_not_raise_property_changed()
        {
            AssertPropertyChangedNotRaisedWhenPropertyAlreadySetToValue(
                TestValue,
                GetModelProperty,
                SetViewModelProperty,
                _propertyName);
        }

        [Test]
        public void when_getting_delta_gamma_should_delegate_to_roto_zoomer()
        {
            GetViewModelProperty(ViewModel);

            Model.AssertWasCalled(new System.Func<TModel, object>(GetModelProperty));
        }

        protected abstract TTestValue GetViewModelProperty(TViewModel viewModel);
        protected abstract object GetModelProperty(TModel model);
        protected abstract void SetViewModelProperty(TViewModel viewModel);
        protected abstract void SetModelProperty(TModel model);
    }
}