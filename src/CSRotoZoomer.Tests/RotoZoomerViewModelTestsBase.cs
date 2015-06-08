using Rhino.Mocks;

namespace CSRotoZoomer.Tests
{
    public class RotoZoomerViewModelTestsBase : 
        PropertyChangedTestsBase<RotoZoomerViewModel, IRotoZoomer>
    {
        protected override RotoZoomerViewModel GetViewModel()
        {
            return new RotoZoomerViewModel(Model);
        }

        protected override IRotoZoomer GetModel()
        {
            return MockRepository.GenerateMock<IRotoZoomer>();
        }
    }
}