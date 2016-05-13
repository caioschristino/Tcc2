using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectService.Sensor.Ativadores
{
    public class AtivadorSimples : AtivadorSensor
    {
        protected override bool SensorConectado(KinectSensorCollection sensores)
        {
            var sensorConectado = sensores.FirstOrDefault(w => w.Status == KinectStatus.Connected);
            if (sensorConectado != null)
            {
                this.kinectSensor = sensorConectado;
                return true;
            }

            return false;
        }

        public override enumStatusSensor RastrearSensor(KinectSensorCollection sensores)
        {
            enumStatusSensor status = enumStatusSensor.NaoConectado;
            if (sensores != null)
            {
                status = enumStatusSensor.Inativo;
                if (this.SensorConectado(sensores))
                {
                    status = enumStatusSensor.Ativo;
                }
            }

            return status;
        }
    }
}
