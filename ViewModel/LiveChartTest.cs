using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Geared;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Chart.ViewModel
{
    class LiveChartTest
    {
        public LiveChartTest()
        {
            GetLineSeriesData();
        }

        /// <summary>
        /// 折线图集合
        /// </summary>
        private SeriesCollection lineSeriesCollection = new SeriesCollection();
        public SeriesCollection LineSeriesCollection
        {
            get { return lineSeriesCollection; }
            set { lineSeriesCollection = value; }
        }


        /// <summary>
        /// 设置行系列数据
        /// 参考：https://blog.csdn.net/qq_38693757/article/details/124473197
        /// </summary>
        private void GetLineSeriesData()
        {
            List<string> titles = new List<string> { };
            //设置系列数量
            for (int i = 0; i < 5; i++)
            {
                titles.Add(i.ToString());
            }

            List<List<ObservablePoint>> values = new List<List<ObservablePoint>>{};

            Random random = new Random();
            for (int i = 0; i < titles.Count(); i++)
            {
                List<ObservablePoint> test = new List<ObservablePoint> { };
                //设置单个系列数据量
                for (int j = 0; j < 500; j++)
                {
                    test.Add(new ObservablePoint(j + i, j + i*2));
                }
                values.Add(test);
            };


            for (int i = 0; i < titles.Count; i++)
            {
                LineSeries lineseries = new LineSeries
                {
                    DataLabels = false,
                    Title = titles[i],
                    // 使用Geared加速包
                    Values = new GearedValues<ObservablePoint>(values[i]).WithQuality(Quality.High),
                    // 免费版
                    //Values = new ChartValues<ObservablePoint>(values[i]),
                    Fill = Brushes.Transparent,
                    LineSmoothness = 0
                };
                LineSeriesCollection.Add(lineseries);
            }
        }
    }
}
