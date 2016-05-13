using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectService.Movimento
{
    public abstract class Movimento
    {
        protected int contadorQuadros { get; set; }

        public double anguloCabeca { get; set; }

        public double anguloColuna { get; set; }

        public double anguloJoelho { get; set; }

        public abstract enumEstadoRastreado Rastrear(Skeleton Personagem);

        protected abstract void CalcularAngulos(Skeleton Personagem);
    }
}
