using NUnit.Framework;
using Rhino.Mocks;

namespace CSRotoZoomer.Tests
{
    [TestFixture]
    public class RotoZoomerViewModelTests
    {
        private RotoZoomerViewModel _sut;
        private IRotoZoomer _rotoZoomer;

        [SetUp]
        public void SetUp()
        {
            _rotoZoomer = MockRepository.GenerateMock<IRotoZoomer>();
            _sut = new RotoZoomerViewModel(_rotoZoomer);
        }

        [Test]
        public void when_zoom_in_max_set_will_file_property_changed_event()
        {
            var called = false;

            _sut.PropertyChanged += (s, a) => called = true;
            _sut.ZoomInMax = 100;

            Assert.That(called);
        }

        [Test]
        public void when_zoom_in_max_set_will_delegate_to_roto_zoomer()
        {
            _sut.ZoomInMax = 100;

            _rotoZoomer.AssertWasCalled(x => x.ZoomInMax = 100);
        }

        [Test]
        public void when_zoom_in_max_set_to_existing_value_will_not_set_again()
        {
            _rotoZoomer.Stub(x => x.ZoomInMax).Return(100);

            _sut.ZoomInMax = 100;

            _rotoZoomer.AssertWasNotCalled(x => x.ZoomInMax = 100);
        }

        [Test]
        public void when_zoom_in_max_set_to_existing_value_will_not_raise_property_changed()
        {
            _rotoZoomer.Stub(x => x.ZoomInMax).Return(100);
            
            var called = false;

            _sut.PropertyChanged += (s, a) => called = true;
            _sut.ZoomInMax = 100;

            Assert.IsFalse(called);

        }
    }
}
