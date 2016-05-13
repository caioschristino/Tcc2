using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectService.Sensor
{
    public abstract class AtivadorSensor
    {
        public KinectSensor kinectSensor { get; set; }

        protected abstract bool SensorConectado(KinectSensorCollection sensores);

        public abstract enumStatusSensor RastrearSensor(KinectSensorCollection sensores);
    }
}
