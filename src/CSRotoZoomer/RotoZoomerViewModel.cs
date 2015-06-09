using System;
using System.ComponentModel;

namespace CSRotoZoomer
{
    public class RotoZoomerViewModel : INotifyPropertyChanged
    {
        private readonly IRotoZoomer _rotoZoomer;
        public event PropertyChangedEventHandler PropertyChanged;

        public RotoZoomerViewModel(IRotoZoomer rotoZoomer)
        {
            _rotoZoomer = rotoZoomer;
        }

        public double DeltaGamma
        {
            get { return (int) _rotoZoomer.DeltaGamma; }
            set
            {
                if ((int) _rotoZoomer.DeltaGamma == value) return;
                
                _rotoZoomer.DeltaGamma = value;
                OnPropertyChanged("DeltaGamma");
            }
        }

        public int ZoomInMax
        {
            get { return _rotoZoomer.ZoomInMax; }
            set
            {
                if (_rotoZoomer.ZoomInMax == value) return;

                _rotoZoomer.ZoomInMax = value;
                OnPropertyChanged("ZoomInMax");
            }
        }

        public int ZoomCounter
        {
            get { return _rotoZoomer.ZoomCounter; }
        }

        public double XZoomDelta
        {
            get { return _rotoZoomer.XZoomDelta; }
            set
            {
                if (Math.Abs(_rotoZoomer.XZoomDelta - value) < 0.0001) return;
                
                _rotoZoomer.XZoomDelta = value;
                OnPropertyChanged("XZoomDelta");
            }
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