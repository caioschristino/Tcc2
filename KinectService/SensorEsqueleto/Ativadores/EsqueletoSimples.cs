using KinectService.Movimento.Controle;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KinectService.SensorEsqueleto.Ativadores
{
    public class EsqueletoSimples : EsqueletoSensor
    {
        public override List<Skeleton> IdentificarEsqueletos(SkeletonFrame esqueletoFrame, Func<Skeleton, bool> funcaoCaptura)
        {
            Skeleton[] esqueletos = new Skeleton[6];
            if (esqueletoFrame == null)
            {
                return null;
            }

            esqueletoFrame.CopySkeletonDataTo(esqueletos);
            return esqueletos.Where(funcaoCaptura).ToList();
        }
    }
}