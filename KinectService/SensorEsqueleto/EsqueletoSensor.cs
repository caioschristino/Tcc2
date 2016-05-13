using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KinectService.SensorEsqueleto
{
    public abstract class EsqueletoSensor
    {
        public KinectSensor kinectSensor { get; set; }

        public abstract List<Skeleton> IdentificarEsqueletos(SkeletonFrame esqueletoFrame, Func<Skeleton, bool> funcaoCaptura);
    }
}
