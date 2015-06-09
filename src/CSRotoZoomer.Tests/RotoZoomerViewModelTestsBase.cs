using Rhino.Mocks;

namespace CSRotoZoomer.Tests
{
    public abstract class RotoZoomerViewModelTestsBase<TTestValue> : 
        PropertyChangedAutoPropertyTestsBase<RotoZoomerViewModel, IRotoZoomer, TTestValue>
    {
        protected RotoZoomerViewModelTestsBase(string propertyName, TTestValue testValue) : base(propertyName, testValue)
        {
        }

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