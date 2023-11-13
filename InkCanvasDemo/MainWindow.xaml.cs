using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InkCanvasDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 启用手写识别
            inkCanvas.InkPresenter.InputDeviceTypes = CoreInputDeviceTypes.Mouse | CoreInputDeviceTypes.Pen;

            // 处理笔迹绘制
            inkCanvas.StrokeCollected += InkCanvas_StrokeCollected;
        }

        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            // 获取笔迹点信息
            var stylusPoints = e.Stroke.StylusPoints;

            // 自定义渲染笔迹
            // 例如根据速度、方向改变颜色、粗细
            foreach (var point in stylusPoints)
            {
                var stroke = new Line()
                {
                    X1 = point.X,
                    Y1 = point.Y,
                    X2 = point.X + point.PressureFactor * 5,
                    Y2 = point.Y + point.PressureFactor * 5,
                    StrokeThickness = point.PressureFactor * 5,
                    Stroke = new SolidColorBrush(GetColor(point.Velocity))
                };

                inkCanvas.Children.Add(stroke);
            }
        }
    }
