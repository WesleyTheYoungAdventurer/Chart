using Chart.Model;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Data.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Chart.ViewModel
{
    class SciChartViewModelTest: ViewModelBase
    {
       
        private string _chartTitle = "Test Sci Chart";
        private string _xAxisTitle = "X";
        private string _yAxisTitle = "Y";
        private bool _enableZoom = true;
        private bool _enablePan;
        private bool _enableTip = true;
        private ObservableCollection<IRenderableSeriesViewModel> _renderableSeries;

        public SciChartViewModelTest()
        {
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            for (int i = 0; i < 10000000; i++)
            {
                x.Add(i + 10);
                y.Add(i + i * 0.2);
             }

            var lineData = new XyDataSeries<double, double>() { SeriesName = "TestSeries" };
            lineData.Append(x,y);
            _renderableSeries = new ObservableCollection<IRenderableSeriesViewModel>();
            RanderableSeries.Add(new LineRenderableSeriesViewModel()
            {
                StrokeThickness = 2,
                Stroke = Colors.SteelBlue,
                DataSeries = lineData,
            });


            //Append the initial values to the chart
            var dummyDataProvider = new DummyDataProvider();

            var initialDataValues = dummyDataProvider.GetHistoricalData();
            lineData.Append(initialDataValues.XValues, initialDataValues.YValues);
            // Subscribe to future updates
            dummyDataProvider.SubscribeUpdates((newValues) =>
            {
                // Append when new values arrive
                lineData.Append(newValues.XValues, newValues.YValues);
                // Zoom the chart to fit
                lineData.InvalidateParentSurface(RangeMode.ZoomToFit);
            });

        }   
        public ObservableCollection<IRenderableSeriesViewModel> RanderableSeries
        {
            get { return _renderableSeries; }
            set { UpdateProper(ref _renderableSeries, value); }
        }
            
        public string ChartTitle
        {
            get { return _chartTitle; }
            set { UpdateProper(ref _chartTitle, value); }
        }

        public string XAxisTitle
        {
            get { return _xAxisTitle; }
            set { UpdateProper(ref _xAxisTitle, value); }
        }

        public string YAisTitle
        {
            get { return _yAxisTitle; }
            set { UpdateProper(ref _yAxisTitle, value); }
        }

        public bool EnableZoom
        {
            get { return _enableZoom; }
            set
            {
                if (_enableZoom != value)
                {
                    _enableZoom = value;
                    OnPropertyChanged();
                    if (_enableZoom)
                    {
                        EnablePan = false;
                    }
                }
            }
        }

        public bool EnablePan
        {
            get { return _enablePan; }
            set
            {
                if (_enablePan != value)
                {
                    _enablePan = value;
                    OnPropertyChanged();
                    if (_enablePan)
                    {
                        EnableZoom = false;
                    }
                }
            }
        }

        public bool EnableTip
        {
            get { return _enableTip; }
            set { UpdateProper(ref _enableTip, value); }
        }

    }
}
