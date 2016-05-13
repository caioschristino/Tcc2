using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectService.Sensor
{
    public class SensorService<T> : ISensorService where T : AtivadorSensor, new()
    {
        private T sensorT;
        public KinectSensor kinectSensor { get; private set; }
        public enumStatusSensor statusSensor { get; set; }

        TransformSmoothParameters parametroEsqueleto = new TransformSmoothParameters
        {
            Smoothing = 0.5f,
            Correction = 0.0f,
            Prediction = 0.0f,
            JitterRadius = 1.0f,
            MaxDeviationRadius = 0.5f
        };

        public SensorService()
        {
            this.statusSensor = enumStatusSensor.NaoConectado;
            this.sensorT = Activator.CreateInstance<T>();
        }

        public void ConfigurarSensor(KinectSensorCollection sensores, object imagemFormat, bool mostrarSkeleton)
        {
            this.statusSensor = this.sensorT.RastrearSensor(sensores);
            if (this.statusSensor == enumStatusSensor.Ativo)
            {
                this.kinectSensor = this.sensorT.kinectSensor;
                this.ConfigurarImageFormat(this.kinectSensor, imagemFormat);
                if (mostrarSkeleton)
                {
                    this.kinectSensor.DepthStream.Enable();
                    this.kinectSensor.SkeletonStream.Enable(parametroEsqueleto);
                }
            }
        }

        private void ConfigurarImageFormat(KinectSensor kinectSensor, object imagemFormat)
        {
            switch (imagemFormat.GetType().Name)
            {
                case "DepthImageFormat":
                    DepthImageFormat formatDepth = (DepthImageFormat)imagemFormat;
                    kinectSensor.DepthStream.Enable(formatDepth);
                    break;

                case "ColorImageFormat":
                    ColorImageFormat formatColor = (ColorImageFormat)imagemFormat;
                    kinectSensor.ColorStream.Enable(formatColor);
                    break;
            }
        }
    }
}
