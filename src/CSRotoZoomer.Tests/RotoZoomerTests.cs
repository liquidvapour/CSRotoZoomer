using System;
using System.Drawing;
using CSRotoZoomer.Tests.images;
using NUnit.Framework;
using Rhino.Mocks;

namespace CSRotoZoomer.Tests
{
    [TestFixture]
    public class RotoZoomerTests
    {
        private Bitmap _srcImage;
        private IMap<Bitmap, uint[]> _mockImageMapper;

        [SetUp]
        public void SetUp()
        {
            _mockImageMapper = MockRepository.GenerateMock<IMap<Bitmap, uint[]>>();
            _srcImage = new Bitmap(typeof(PlaceHolder), "Test.bmp");
        }

        [Test]
        public void when_initialized_should_default_zoom_in_max()
        {
            var sut = new RotoZoomer(_mockImageMapper);
            sut.ResizeCanvas(new Rectangle(0, 0, 2, 2));
            sut.ZoomInMax = 666;
            
            sut.Initialize(_srcImage);

            Assert.That(sut.ZoomInMax, Is.EqualTo(-9));
        }

        [Test]
        public void when_init_called_before_resize_canvis_will_thorw_invalid_operation_exception()
        {
            var sut = new RotoZoomer(null);

            Assert.Throws<InvalidOperationException>(() => sut.Initialize(_srcImage));
        }
    }
}