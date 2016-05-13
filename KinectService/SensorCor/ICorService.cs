using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KinectService.SensorCor
{
    public interface ICorService
    {
        void GerarImagem(Canvas canvasParaDesenhar, AllFramesReadyEventArgs e);
    }
}
