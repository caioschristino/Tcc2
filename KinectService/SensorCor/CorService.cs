using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KinectService.SensorCor
{
    public class CorService<T> : ICorService where T : CoresSensor, new()
    {
        public T corT;

        public CorService(KinectSensor kinectSensor)
        {
            this.corT = Activator.CreateInstance<T>();
            this.corT.kinectSensor = kinectSensor;
        }

        public void GerarImagem(Canvas canvasParaDesenhar, AllFramesReadyEventArgs e)
        {
            using (ColorImageFrame corFrame = e.OpenColorImageFrame())
            {
                this.corT.GerarImagem(canvasParaDesenhar, corFrame);
            }
        }

    }
}
