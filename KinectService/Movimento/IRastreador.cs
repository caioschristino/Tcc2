using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectService.Movimento
{
    public interface IRastreador
    {
        enumEstadoRastreado estadoAtualPersonagem { get; }

        double anguloCabeca { get; }

        double anguloColuna { get; }

        double anguloJoelho { get; }

        void Rastrear(Skeleton SkeletonPersonagem, out bool PoseIdentificada);
    }
}
