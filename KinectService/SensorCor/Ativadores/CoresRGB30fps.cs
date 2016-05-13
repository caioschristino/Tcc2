using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinectService.SensorCor.Ativadores
{
    public class CoresRGB30fps : CoresSensor, IDisposable
    {
        public void Dispose()
        {
            this.kinectSensor.Stop();
        }

        public override void GerarImagem(Canvas canvasParaDesenhar, ColorImageFrame color)
        {
            byte[] imagem = ImagemRGB(color);
            if (this.kinectSensor == null || imagem == null)
            {
                return;
            }
            canvasParaDesenhar.Background = new ImageBrush(BitmapSource.Create(kinectSensor.ColorStream.FrameWidth, kinectSensor.ColorStream.FrameHeight,
                                96, 96, PixelFormats.Bgr32, null, imagem,
                                kinectSensor.ColorStream.FrameWidth * kinectSensor.ColorStream.FrameBytesPerPixel));

            canvasParaDesenhar.Children.Clear();
        }

        private byte[] ImagemRGB(ColorImageFrame frameCor)
        {
            if (frameCor == null)
            {
                return null;
            }

            using (frameCor)
            {
                byte[] bytesImagem = new byte[frameCor.PixelDataLength];
                frameCor.CopyPixelDataTo(bytesImagem);

                return bytesImagem;
            }
        }
    }
}
