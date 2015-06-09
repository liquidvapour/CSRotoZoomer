using NUnit.Framework;
using Rhino.Mocks;

namespace CSRotoZoomer.Tests
{
    [TestFixture]
    public class RotoZoomerViewModelZoomInMaxTests : RotoZoomerViewModelTestsBase<int>
    {
        public RotoZoomerViewModelZoomInMaxTests() : base("ZoomInMax", 100)
        {
        }

        protected override int GetViewModelProperty(RotoZoomerViewModel viewModel)
        {
            return viewModel.ZoomInMax;
        }

        protected override object GetModelProperty(IRotoZoomer model)
        {
            return model.ZoomInMax;
        }

        protected override void SetViewModelProperty(RotoZoomerViewModel viewModel)
        {
            viewModel.ZoomInMax = TestValue;
        }

        protected override void SetModelProperty(IRotoZoomer model)
        {
            model.ZoomInMax = TestValue;
        }
    }
}