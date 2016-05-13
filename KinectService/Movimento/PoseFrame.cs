using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectService.Movimento
{
    public abstract class PoseFrame : Movimento
    {
        protected int quadroIdentificado { get; set; }

        public override enumEstadoRastreado Rastrear(Skeleton Personagem)
        {
            enumEstadoRastreado novoEstado;
            if (Personagem != null)
            {
                this.CalcularAngulos(Personagem);
                if (this.quadroIdentificado == this.contadorQuadros)
                {
                    novoEstado = enumEstadoRastreado.Identificado;
                }
                else
                {
                    novoEstado = enumEstadoRastreado.EmExecucao;
                    this.contadorQuadros++;
                }
            }
            else
            {
                novoEstado = enumEstadoRastreado.NaoIdentificado;
                this.contadorQuadros = 0;
            }

            return novoEstado;
        }
    }
}
