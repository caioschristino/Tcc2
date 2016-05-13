using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectService.Sensor
{
    public interface ISensorService
    {
        KinectSensor kinectSensor { get; }

        enumStatusSensor statusSensor { get; }

        void ConfigurarSensor(KinectSensorCollection sensores, object imagemFormat, bool mostrarSkeleton);
    }
}
