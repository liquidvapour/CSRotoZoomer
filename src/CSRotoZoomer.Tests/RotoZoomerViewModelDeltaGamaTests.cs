using NUnit.Framework;
using Rhino.Mocks;

namespace CSRotoZoomer.Tests
{

    [TestFixture]
    public class RotoZoomerViewModelDeltaGamaTests : RotoZoomerViewModelTestsBase
    {
        [Test]
        public void when_delta_gamma_set_will_file_property_changed_event()
        {
            AssertPropertyChangedRaisedFor(vm => vm.DeltaGamma = 100, "DeltaGamma");
        }

        [Test]
        public void when_delta_gamma_set_will_delegate_to_roto_zoomer()
        {
            const int testValue = 100;
            WhenPropertySetWillDelegate(
                vm => vm.DeltaGamma = testValue,
                m => m.DeltaGamma = testValue);
        }

        [Test]
        public void when_delta_gamma_set_to_existing_value_will_not_set_again()
        {

            WhenAlreadySetWillNotDelegate(
                m => m.DeltaGamma,
                value => ViewModel.DeltaGamma = (int)value,
                100,
                m => m.DeltaGamma = 100.0);

        }

        [Test]
        public void when_delta_gamma_set_to_existing_value_will_not_raise_property_changed()
        {
            AssertPropertyChangedNotRaisedWhenPropertyAlreadySetToValue(
                100,
                v => v.DeltaGamma,
                (vm, value) => vm.DeltaGamma = (int)value,
                "DeltaGamma");
        }

        [Test]
        public void when_getting_delta_gamma_should_delegate_to_roto_zoomer()
        {
            var result = ViewModel.DeltaGamma;

            Model.AssertWasCalled(m => m.DeltaGamma);
        }
    }
}
