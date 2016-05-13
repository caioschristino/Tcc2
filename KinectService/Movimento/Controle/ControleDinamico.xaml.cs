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

namespace KinectService.Movimento.Controle
{
    /// <summary>
    /// Interaction logic for ControleDinamico.xaml
    /// </summary>
    public partial class ControleDinamico : UserControl
    {
        public ControleDinamico()
        {
            InitializeComponent();
            this.Angulo = (this.Percentual * 360) / 100;
            RenderArc();
        }

        public int Raio
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public Brush SegmentoCor
        {
            get { return (Brush)GetValue(SegmentColorProperty); }
            set { SetValue(SegmentColorProperty, value); }
        }

        public int EspessuraDesenho
        {
            get { return (int)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public double Percentual
        {
            get { return (double)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }

        public double Angulo
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentual", typeof(double), typeof(ControleDinamico), new PropertyMetadata(65d, new PropertyChangedCallback(OnPercentageChanged)));

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("EspessuraDesenho", typeof(int), typeof(ControleDinamico), new PropertyMetadata(5));

        public static readonly DependencyProperty SegmentColorProperty =
            DependencyProperty.Register("SegmentoCor", typeof(Brush), typeof(ControleDinamico), new PropertyMetadata(new SolidColorBrush(Colors.Red)));

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Raio", typeof(int), typeof(ControleDinamico), new PropertyMetadata(25, new PropertyChangedCallback(OnPropertyChanged)));

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angulo", typeof(double), typeof(ControleDinamico), new PropertyMetadata(120d, new PropertyChangedCallback(OnPropertyChanged)));

        private static void OnPercentageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ControleDinamico Circulo = sender as ControleDinamico;
            Circulo.Angulo = (Circulo.Percentual * 360) / 100;
        }

        private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ControleDinamico circle = sender as ControleDinamico;
            circle.RenderArc();
        }

        public void RenderArc()
        {
            Point pontoInicio = new Point(Raio, 0);
            Point pontoFinal = CoordenadasCartesianas(this.Angulo, this.Raio);

            pontoFinal.X += this.Raio;
            pontoFinal.Y += this.Raio;
            pathRoot.Width = this.Raio * 2 + this.EspessuraDesenho;
            pathRoot.Height = this.Raio * 2 + this.EspessuraDesenho;

            pathRoot.Margin = new Thickness(this.EspessuraDesenho, this.EspessuraDesenho, 0, 0);
            bool largeArc = this.Angulo > 180.0;

            Size outerArcSize = new Size(this.Raio, this.Raio);
            pathFigure.StartPoint = pontoInicio;

            if (pontoInicio.X == Math.Round(pontoFinal.X) && pontoInicio.Y == Math.Round(pontoFinal.Y))
            {
                pontoFinal.X -= 0.01;
            }
            arcSegment.Point = pontoFinal;
            arcSegment.Size = outerArcSize;
            arcSegment.IsLargeArc = largeArc;
        }

        private Point CoordenadasCartesianas(double Angulo, double Raio)
        {
            double AnguloRad = (Math.PI / 180.0) * (Angulo - 90);

            double x = Raio * Math.Cos(AnguloRad);
            double y = Raio * Math.Sin(AnguloRad);

            return new Point(x, y);
        }
    }
}
