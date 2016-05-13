using KinectService.Movimento.Controle;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TCC2
{
    public class ApresentacaoMain
    {
        Canvas canvasParaDesenhar;
        KinectSensor sensor;


        public ApresentacaoMain(Canvas canvasParaDesenhar, KinectSensor sensor)
        {
            this.sensor = sensor;
            this.canvasParaDesenhar = canvasParaDesenhar;
        }

        public void DesenharComponenteVirtual(Joint articulacao, FrameworkElement elementA, FrameworkElement elementB, double Percentage = 0)
        {
            ColorImagePoint posicaoArticulacao = ConverterCoordenadasArticulacao(articulacao, canvasParaDesenhar.ActualWidth, canvasParaDesenhar.ActualHeight);
            PosicionarComponente(posicaoArticulacao, elementA);
            PosicionarComponente(posicaoArticulacao, elementB);
        }


        private ColorImagePoint ConverterCoordenadasArticulacao(Joint articulacao, double larguraCanvas, double alturaCanvas)
        {
            ColorImagePoint posicaoArticulacao = sensor.CoordinateMapper.MapSkeletonPointToColorPoint(articulacao.Position, sensor.ColorStream.Format);
            posicaoArticulacao.X = (int)(posicaoArticulacao.X * larguraCanvas) / sensor.ColorStream.FrameWidth;
            posicaoArticulacao.Y = (int)(posicaoArticulacao.Y * alturaCanvas) / sensor.ColorStream.FrameHeight;
            return posicaoArticulacao;
        }

        private void PosicionarComponente(ColorImagePoint posicaoArticulacao, FrameworkElement controleVirtual)
        {
            double deslocamentoHorizontal = posicaoArticulacao.X - controleVirtual.Width / 2;
            double deslocamentoVertical = (posicaoArticulacao.Y - controleVirtual.Height / 2);

            if (deslocamentoVertical >= 0 && deslocamentoVertical < canvasParaDesenhar.ActualHeight
                && deslocamentoHorizontal >= 0 && deslocamentoHorizontal < canvasParaDesenhar.ActualWidth)
            {
                Canvas.SetLeft(controleVirtual, deslocamentoHorizontal);
                Canvas.SetTop(controleVirtual, deslocamentoVertical);

                if (!canvasParaDesenhar.Children.Contains(controleVirtual))
                {
                    canvasParaDesenhar.Children.Add(controleVirtual);
                }
            }
        }
    }
}
