using System;
using System.ComponentModel;
using NUnit.Framework;
using Rhino.Mocks;

namespace CSRotoZoomer.Tests
{
    public abstract class PropertyChangedTestsBase<TViewModel, TModel> 
        where TViewModel: INotifyPropertyChanged 
        where TModel : class
    {
        protected TViewModel ViewModel { get; set; }
        protected TModel Model { get; set; }

        protected void AssertPropertyChangedRaisedFor(
            Action<TViewModel> action, 
            string propertyName)
        {
            var called = false;

            ViewModel.PropertyChanged += 
                (s, a) => called = a.PropertyName == propertyName;

            action(ViewModel);

            Assert.That(
                called, 
                string.Format(
                    "Did not see expected PropertyChanged event for property '{0}'.", 
                    propertyName));
        }

        protected void AssertPropertyChangedNotRaisedWhenPropertyAlreadySetToValue<TValue>(
            TValue value, 
            Function<TModel, TValue> prop, 
            Action<TViewModel, TValue> action, 
            string propertyName)
        {
            Model.Stub(prop).Return(value);

            var called = false;

            ViewModel.PropertyChanged += 
                (s, a) => called = a.PropertyName == propertyName;

            action(ViewModel, value);

            Assert.IsFalse(
                called, 
                string.Format(
                    "Saw unexpected PropertyChanged event for property '{0}'.", 
                    propertyName));
        }

        protected void WhenPropertySetWillDelegate(
            Action<TViewModel> when, 
            System.Func<TModel, object> wasCalled)
        {
            when(ViewModel);
            Model.AssertWasCalled(wasCalled);
        }

        protected void WhenAlreadySetWillNotDelegate<TValue>(
            Function<TModel, TValue> prop, 
            Action<TValue> action, 
            TValue value, 
            System.Func<TModel, object> wasCalled)
        {
            Model.Stub(prop).Return(value);

            action(value);

            Model.AssertWasNotCalled(wasCalled);
        }

        [SetUp]
        public void SetUp()
        {
            Model = GetModel();
            ViewModel = GetViewModel();
        }

        protected abstract TViewModel GetViewModel();
        protected abstract TModel GetModel();
    }
}