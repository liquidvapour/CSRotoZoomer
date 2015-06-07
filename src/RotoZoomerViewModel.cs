using System.ComponentModel;

namespace CSRotoZoomer
{
    public class RotoZoomerViewModel : INotifyPropertyChanged
    {
        private readonly RotoZoomer _rotoZoomer;
        public event PropertyChangedEventHandler PropertyChanged;

        public RotoZoomerViewModel(RotoZoomer rotoZoomer)
        {
            _rotoZoomer = rotoZoomer;
        }

        public int DeltaGamma
        {
            get { return (int) _rotoZoomer.DeltaGamma; }
            set
            {
                if ((int) _rotoZoomer.DeltaGamma == value) return;
                
                _rotoZoomer.DeltaGamma = value;
                OnPropertyChanged("DeltaGamma");
            }
        }

        public int ZoomCounter
        {
            get { return _rotoZoomer.ZoomCounter; }
        }

        public void OnUpdate()
        {
            OnPropertyChanged("ZoomCounter");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}