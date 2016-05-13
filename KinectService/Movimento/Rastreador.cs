using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectService.Movimento
{
    public class Rastreador<T> : IRastreador where T : Movimento, new()
    {
        private T movimento;

        public enumEstadoRastreado estadoAtualPersonagem { get; private set; }

        public double anguloCabeca { get; private set; }

        public double anguloColuna { get; private set; }

        public double anguloJoelho { get; private set; }

        public Rastreador()
        {
            this.estadoAtualPersonagem = enumEstadoRastreado.NaoIdentificado;
            this.movimento = Activator.CreateInstance<T>();
        }

        public void Rastrear(Skeleton Personagem, out bool PoseIdentificada)
        {
            PoseIdentificada = false;
            enumEstadoRastreado novoEstado = movimento.Rastrear(Personagem);

            this.anguloCabeca = movimento.anguloCabeca;
            this.anguloColuna = movimento.anguloColuna;
            this.anguloJoelho = movimento.anguloJoelho;

            switch (novoEstado)
            {
                case enumEstadoRastreado.Identificado:
                    if (this.estadoAtualPersonagem != enumEstadoRastreado.Identificado)
                    {
                        PoseIdentificada = true;
                    }
                    break;
            }
        }
    }
}
