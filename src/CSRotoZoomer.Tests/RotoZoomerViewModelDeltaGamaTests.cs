using NUnit.Framework;
using Rhino.Mocks;

namespace CSRotoZoomer.Tests
{
    [TestFixture]
    public class RotoZoomerViewModelDeltaGamaTests : RotoZoomerViewModelTestsBase<double>
    {
        public RotoZoomerViewModelDeltaGamaTests()
            : base("DeltaGamma", 100)
        {
        }

        protected override double GetViewModelProperty(RotoZoomerViewModel viewModel)
        {
            return viewModel.DeltaGamma;
        }

        protected override void SetViewModelProperty(RotoZoomerViewModel viewModel)
        {
            viewModel.DeltaGamma = TestValue;
        }

        protected override object GetModelProperty(IRotoZoomer model)
        {
            return model.DeltaGamma;
        }

        protected override void SetModelProperty(IRotoZoomer model)
        {
            model.DeltaGamma = TestValue;
        }
    }
}
