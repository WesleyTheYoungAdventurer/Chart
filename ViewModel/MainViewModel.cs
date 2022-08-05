using Chart.Commands;
using Chart.Model;
using HandyControl.Controls;
using HandyControl.Tools;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace Chart.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private string _chartTitle ;
        private string _xAxisTitle ;
        private string _yAxisTitle ;
        private int _xAxisSelect =-1;
        private int _yAxisSelect =-1;
        private bool _enableZoom;
        private bool _enablePan = true;
        private bool _enableTip;
        private string _path;
        private List<BtnColor> _listColor;
        private List<XyTitle> _ArrayTitle;
        private ObservableCollection<IRenderableSeriesViewModel> _renderableSeries;
        readonly string[] _windownColors ={
"#FFF0F8FF",
"#FFEEE8AA",
"#FFDA70D6",
"#FFFF4500",
"#FFFFA500",
"#FF6B8E23",
"#FF808000",
"#FFFDF5E6",
"#FF000080",
"#FFFFDEAD",
"#FFFFE4B5",
"#FFFFE4E1",
"#FFF5FFFA",
"#FF191970",
"#FFC71585",
"#FF48D1CC",
"#FF00FA9A",
"#FF7B68EE",
"#FF87CEFA",
"#FF778899",
"#FFB0C4DE",
"#FFFFFFE0",
"#FF00FF00",
"#FF32CD32",
"#FF98FB98",
"#FFFAF0E6",
"#FF800000",
"#FF66CDAA",
"#FF0000CD",
"#FFBA55D3",
"#FF9370DB",
"#FF3CB371",
"#FFFF00FF",
"#FFAFEEEE",
"#FFDB7093",
"#FFFFEFD5",
"#FF708090",
"#FFFFFAFA",
"#FF00FF7F",
"#FF4682B4",
"#FFD2B48C",
"#FF008080",
"#FF6A5ACD",
"#FFD8BFD8",
"#00FFFFFF",
"#FF40E0D0",
"#FFEE82EE",
"#FFF5DEB3",
"#FFFFFFFF",
"#FFF5F5F5",
"#FFFF6347",
"#FF20B2AA",
"#FF87CEEB",
"#FFA0522D",
"#FFFFDAB9",
"#FFCD853F",
"#FFFFC0CB",
"#FFDDA0DD",
"#FFB0E0E6",
"#FF800080",
"#FFC0C0C0",
"#FFFF0000",
"#FF4169E1",
"#FF8B4513",
"#FFFA8072",
"#FFF4A460",
"#FF2E8B57",
"#FFFFF5EE",
"#FFBC8F8F",
"#FFFFFF00",
"#FFFFA07A",
"#FF90EE90",
"#FF8B0000",
"#FF9932CC",
"#FFFF8C00",
"#FF556B2F",
"#FF8B008B",
"#FFBDB76B",
"#FF006400",
"#FFA9A9A9",
"#FFB8860B",
"#FF008B8B",
"#FF00008B",
"#FF00FFFF",
"#FFDC143C",
"#FFFFF8DC",
"#FF6495ED",
"#FFFF7F50",
"#FFD2691E",
"#FFFAEBD7",
"#FF00FFFF",
"#FF7FFFD4",
"#FFF0FFFF",
"#FFF5F5DC",
"#FFFFE4C4",
"#FFE9967A",
"#FF000000",
"#FF0000FF",
"#FF8A2BE2",
"#FFA52A2A",
"#FFDEB887",
"#FF5F9EA0",
"#FF7FFF00",
"#FFFFEBCD",
"#FF8FBC8F",
"#FF483D8B",
"#FF2F4F4F",
"#FFFF69B4",
"#FFCD5C5C",
"#FF4B0082",
"#FFFFFFF0",
"#FFF0E68C",
"#FFE6E6FA",
"#FFF0FFF0",
"#FFFFF0F5",
"#FFFFFACD",
"#FFADD8E6",
"#FFF08080",
"#FFE0FFFF",
"#FFFAFAD2",
"#FFD3D3D3",
"#FF7CFC00",
"#FFFFB6C1",
"#FFADFF2F",
"#FF808080",
"#FF00CED1",
"#FF9400D3",
"#FFFF1493",
"#FF00BFFF",
"#FF696969",
"#FF1E90FF",
"#FF008000",
"#FF228B22",
"#FFFF00FF",
"#FFDCDCDC",
"#FFF8F8FF",
"#FFFFD700",
"#FFDAA520",
"#FFFFFAF0",
"#FF9ACD32",
"#FFB22222",
 };
        private List<XyDataSeries<double, double>> xyDataSeries = new List<XyDataSeries<double, double>>();

        public MainViewModel()
        {
            OpenPathCommand = new MyCommand(SelectPath);
            SelectItemChangedCommand = new MyCommand(SelectItemChanged);
            SelectCheckBoxCommand = new MyCommand(SelectCheckBoxEnable);
            ShowColorPickerCommand = new MyCommand(ShowColorPicker);

            // 实例化SciChart集合
            _renderableSeries = new ObservableCollection<IRenderableSeriesViewModel>();
        }

        public MyCommand OpenPathCommand { get; set; }
        public MyCommand SelectItemChangedCommand { get; set; }
        public MyCommand SelectCheckBoxCommand { get; set; }
        public MyCommand ShowColorPickerCommand { get; set; }

        public ObservableCollection<IRenderableSeriesViewModel> RenderableSeries
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

        public int ListIndex { get; set; }

        public string FullPath
        {
            get { return _path; }
            set { UpdateProper(ref _path, value); }
        }

        public List<BtnColor> ListColor
        {
            get { return _listColor; }
            set{UpdateProper(ref _listColor, value); }
         }

        public List<XyTitle> ArrayTitle
        {
            get { return _ArrayTitle; }
            set { UpdateProper(ref _ArrayTitle, value); }
        }


        public int XAxisSelect
        {
            get { return _xAxisSelect; }
            set { UpdateProper(ref _xAxisSelect, value); }
        }

        public int YAxisSelect
        {
            get { return _yAxisSelect; }
            set { UpdateProper(ref _yAxisSelect, value); }
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        public void SelectPath()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "请选择含有csv文件夹"
            };
            DialogResult result = folderBrowserDialog.ShowDialog();
            // 如果在选择文件夹取消将跳出
            if (result == DialogResult.Cancel)
            {
                return;
            }

            // 清空之前图表含有的系列
            RenderableSeries.Clear();

            new Task(() =>
            {
                FullPath = folderBrowserDialog.SelectedPath.Trim();

                // 获取数据
                CSV.ReadCsv(FullPath, out List<string> data, out _);
                List<XyTitle> xyTitles = new List<XyTitle>();

                // XY轴添加标题
                if (data.Count() != 0)
                {
                    string[] xyTitle = data.First().Split(new char[] { ',' });
                    for (int i = 0; i < xyTitle.Count(); i++)
                    {
                        xyTitles.Add(new XyTitle() { Title = xyTitle[i] });
                    }
                }
                ArrayTitle = xyTitles;
            }).Start();
        }

        public delegate void printString();
        /// <summary>
        /// 选择坐标轴更新
        /// </summary>
        public void SelectItemChanged()
        {
            // 清空之前图表含有的系列数据
            RenderableSeries.Clear();
            xyDataSeries.Clear();

            new Task(() =>
            {
                // 获取数据
                CSV.ReadCsv(FullPath, out _, out Dictionary<string, List<string[]>> DataDict);
                List<BtnColor> ColorList = new List<BtnColor>();
                int count = 0;
                Random random = new Random();

                if (XAxisSelect >= 0 && YAxisSelect >= 0)
                {
                    foreach (var i in DataDict)
                    {
                        string colorIndex = _windownColors[random.Next(_windownColors.Count())];
                        ColorList.Add(new BtnColor() { ID = count, Code = colorIndex, Name = i.Key, Selector = true});
                        xyDataSeries.Add(new XyDataSeries<double, double>() { SeriesName = i.Key });
                        // 系列允许添加未排序数据
                        xyDataSeries[count].AcceptsUnsortedData = true;

                        for (int j = 0; j < i.Value.Count() - 1; j++)
                        {
                            xyDataSeries[count].Append(double.Parse(i.Value[1 + j][XAxisSelect]), double.Parse(i.Value[1 + j][YAxisSelect]));
                        }

                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            RenderableSeries.Add(new LineRenderableSeriesViewModel()
                            {
                                StrokeThickness = 2,
                                Stroke = (Color)ColorConverter.ConvertFromString(colorIndex),
                                DataSeries = xyDataSeries[count],

                            });
                        });
                        count++;
                    }
                }
                ListColor = ColorList;
            }).Start();
        }

        /// <summary>
        /// 勾选框使能判断
        /// </summary>
        public void SelectCheckBoxEnable()
        {
            // 先删除指定索引的系列
            RenderableSeries.RemoveAt(ListIndex);

            // 勾选显示就在指定的索引添加系列
            if (ListColor[ListIndex].Selector)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    RenderableSeries.Insert(ListIndex, new LineRenderableSeriesViewModel()
                    {
                        StrokeThickness = 2,
                        Stroke = (Color)ColorConverter.ConvertFromString(ListColor[ListIndex].Code),
                        DataSeries = xyDataSeries[ListIndex]
                    });
                });
            }

            // 取消勾选就在指定的索引添加空数据系列
            else
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    RenderableSeries.Insert(ListIndex, new LineRenderableSeriesViewModel() { });
                });
            }
        }

        public void ShowColorPicker()
        {
            var picker = SingleOpenHelper.CreateControl<ColorPicker>();
            var window = new PopupWindow
            {
                PopupElement = picker,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Title = ListColor[ListIndex].Name
            };

            picker.Width = 265;
            picker.Height = 360;
            picker.Canceled += delegate { window.Close(); };
            picker.Confirmed += delegate
            {
                SolidColorBrush getColor = picker.SelectedBrush;

                RenderableSeries.RemoveAt(ListIndex);
                RenderableSeries.Insert(ListIndex, new LineRenderableSeriesViewModel()
                {
                    StrokeThickness = 2,
                    Stroke = (Color)ColorConverter.ConvertFromString(getColor.ToString()),
                    DataSeries = xyDataSeries[ListIndex]
                });

                ListColor[ListIndex].Code = getColor.ToString();

            };
            window.ShowDialog();
        }
    }

    class BtnColor: ViewModelBase
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 系列颜色
        /// </summary>
        public string Code{ get; set;}
        /// <summary>
        /// 文件名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 系列显示/隐藏
        /// </summary>
        public bool Selector { get; set; }
    }

    class XyTitle
    {
        public string Title { get; set; }
    }
}
