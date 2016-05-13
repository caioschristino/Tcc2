using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TCC2
{
    public class EsqueletoUsuario
    {
        private KinectSensor MySensor;

        public EsqueletoUsuario(KinectSensor Sensor)
        {
            this.MySensor = Sensor;
        }

        public void DesenharArticulacao(Joint articulacao, Canvas canvasParaDesenhar, double AnguloIdentificado)
        {
            ColorImagePoint posicaoArticulacao = ConverterCoordenadasArticulacao(articulacao, canvasParaDesenhar.ActualWidth, canvasParaDesenhar.ActualHeight);

            int diametroArticulacao = 40;
            int StrokeThickness = 5;
            int Radius = 12;
            Brush corDesenho = Brushes.Red;
            CircularControlAngle objetoArticulacao = CriarComponenteVisualArticulacao(articulacao, diametroArticulacao, Radius, corDesenho, StrokeThickness);
            double deslocamentoHorizontal = posicaoArticulacao.X - objetoArticulacao.Width / 2;
            double deslocamentoVertical = (posicaoArticulacao.Y - objetoArticulacao.Height / 2);

            if (deslocamentoVertical >= 0 && deslocamentoVertical < canvasParaDesenhar.ActualHeight
                && deslocamentoHorizontal >= 0 && deslocamentoHorizontal < canvasParaDesenhar.ActualWidth)
            {
                Canvas.SetLeft(objetoArticulacao, deslocamentoHorizontal);
                Canvas.SetTop(objetoArticulacao, deslocamentoVertical);

                canvasParaDesenhar.Children.Add(objetoArticulacao);
            }


            StrokeThickness = 5;
            Radius = 12;
            corDesenho = Brushes.Black;
            CircularControlAngle objetoArticulacao2 = CriarComponenteVisualArticulacao(articulacao, diametroArticulacao, Radius, corDesenho, StrokeThickness, AnguloIdentificado);

            double deslocamentoHorizontal2 = posicaoArticulacao.X - objetoArticulacao.Width / 2;
            double deslocamentoVertical2 = (posicaoArticulacao.Y - objetoArticulacao.Height / 2);

            if (deslocamentoVertical2 >= 0 && deslocamentoVertical2 < canvasParaDesenhar.ActualHeight
                && deslocamentoHorizontal2 >= 0 && deslocamentoHorizontal2 < canvasParaDesenhar.ActualWidth)
            {
                Canvas.SetLeft(objetoArticulacao2, deslocamentoHorizontal2);
                Canvas.SetTop(objetoArticulacao2, deslocamentoVertical2);

                canvasParaDesenhar.Children.Add(objetoArticulacao2);
            }
        }

        private Line CriarComponenteVisualOsso(int larguraDesenho, Brush corDesenho, double origemX, double origemY, double destinoX, double destinoY)
        {
            Line objetoOsso = new Line();

            objetoOsso.StrokeThickness = larguraDesenho;
            objetoOsso.Stroke = corDesenho;
            objetoOsso.X1 = origemX;
            objetoOsso.X2 = destinoX;

            objetoOsso.Y1 = origemY;
            objetoOsso.Y2 = destinoY;

            return objetoOsso;
        }

        private CircularControlAngle CriarComponenteVisualArticulacao(Joint articulacao, int diametroArticulacao, int Radius, Brush SegmentColor, int StrokeThickness = 0, double Percentage = 0)
        {

            CircularControlAngle objetoArticulacao = new CircularControlAngle();
            objetoArticulacao.Radius = Radius;
            objetoArticulacao.StrokeThickness = StrokeThickness > 0 ? StrokeThickness : 3;
            objetoArticulacao.SegmentColor = SegmentColor;
            objetoArticulacao.Width = diametroArticulacao;
            objetoArticulacao.Height = diametroArticulacao;
            objetoArticulacao.Percentage = Percentage > 0 ? Percentage : 100;
            objetoArticulacao.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            objetoArticulacao.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            return objetoArticulacao;
        }

        private ColorImagePoint ConverterCoordenadasArticulacao(Joint articulacao, double larguraCanvas, double alturaCanvas)
        {
            ColorImagePoint posicaoArticulacao = MySensor.CoordinateMapper.MapSkeletonPointToColorPoint(articulacao.Position, MySensor.ColorStream.Format);
            posicaoArticulacao.X = (int)(posicaoArticulacao.X * larguraCanvas) / MySensor.ColorStream.FrameWidth;
            posicaoArticulacao.Y = (int)(posicaoArticulacao.Y * alturaCanvas) / MySensor.ColorStream.FrameHeight;
            return posicaoArticulacao;
        }

        public void DesenharOsso(Joint articulacaoOrigem, Joint articulacaoDestino, Canvas canvasParaDesenhar)
        {
            int larguraDesenho = 4;
            Brush corDesenho = Brushes.Green;

            ColorImagePoint posicaoArticulacaoOrigem =
                ConverterCoordenadasArticulacao(articulacaoOrigem, canvasParaDesenhar.ActualWidth, canvasParaDesenhar.ActualHeight);

            ColorImagePoint posicaoArticulacaoDestino =
                ConverterCoordenadasArticulacao(articulacaoDestino, canvasParaDesenhar.ActualWidth, canvasParaDesenhar.ActualHeight);

            Line objetoOsso =
                CriarComponenteVisualOsso(larguraDesenho, corDesenho,
                posicaoArticulacaoOrigem.X, posicaoArticulacaoOrigem.Y,
                posicaoArticulacaoDestino.X, posicaoArticulacaoDestino.Y);


            if (Math.Max(objetoOsso.X1, objetoOsso.X2) < canvasParaDesenhar.ActualWidth &&
                Math.Min(objetoOsso.X1, objetoOsso.X2) > 0 &&
                Math.Max(objetoOsso.Y1, objetoOsso.Y2) < canvasParaDesenhar.ActualHeight &&
                Math.Min(objetoOsso.Y1, objetoOsso.Y2) > 0)
                canvasParaDesenhar.Children.Add(objetoOsso);
        }
    }
}
