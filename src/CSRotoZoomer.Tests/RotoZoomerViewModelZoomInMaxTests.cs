using NUnit.Framework;
using Rhino.Mocks;

namespace CSRotoZoomer.Tests
{
    [TestFixture]
    public class RotoZoomerViewModelZoomInMaxTests : RotoZoomerViewModelTestsBase
    {
        [Test]
        public void when_zoom_in_max_set_will_file_property_changed_event()
        {
            AssertPropertyChangedRaisedFor(vm => vm.ZoomInMax = 100, "ZoomInMax");
        }

        [Test]
        public void when_zoom_in_max_set_will_delegate_to_roto_zoomer()
        {
            const int testValue = 100;
            WhenPropertySetWillDelegate(
                vm => vm.ZoomInMax = testValue,
                m => m.ZoomInMax = testValue);
        }

        [Test]
        public void when_zoom_in_max_set_to_existing_value_will_not_set_again()
        {

            WhenAlreadySetWillNotDelegate(
                x => x.ZoomInMax, 
                x => ViewModel.ZoomInMax = x, 
                100, 
                x => x.ZoomInMax = 100);

        }

        [Test]
        public void when_zoom_in_max_set_to_existing_value_will_not_raise_property_changed()
        {
            AssertPropertyChangedNotRaisedWhenPropertyAlreadySetToValue(
                100,
                x => x.ZoomInMax,
                (vm, value) => vm.ZoomInMax = value,
                "ZoomInMax");
        }

        [Test]
        public void when_getting_zoom_in_max_should_delegate_to_roto_zoomer()
        {
            var result = ViewModel.ZoomInMax;

            Model.AssertWasCalled(x => x.ZoomInMax);
        }
    }
}