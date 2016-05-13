using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KinectService.SensorCor
{
    public abstract class CoresSensor
    {
        public KinectSensor kinectSensor { get; set; }

        public abstract void GerarImagem(Canvas canvasParaDesenhar, ColorImageFrame color);
    }
}
