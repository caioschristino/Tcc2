using KinectService.Movimento.Modelo;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectService.Movimento.Pose
{
    public class PoseSentado : PoseFrame
    {
        public PoseSentado()
        {
            this.quadroIdentificado = 2;
        }

        protected override void CalcularAngulos(Skeleton Personagem)
        {
            Joint Espinha = Personagem.Joints[JointType.Spine];
            Joint Cabeça = Personagem.Joints[JointType.Head];
            Joint Clavicula = Personagem.Joints[JointType.ShoulderCenter];
            Joint Quadril = Personagem.Joints[JointType.HipCenter];
            Joint Joelho = Personagem.Joints[JointType.KneeRight];
            Joint Tornozelo = Personagem.Joints[JointType.AnkleRight];

            this.anguloCabeca = Personagem.CalcularProdutoEscalar(Cabeça, Clavicula, Espinha);

            this.anguloColuna = Personagem.CalcularProdutoEscalar(Cabeça, Espinha, Joelho);
            this.anguloColuna = (anguloColuna * 2) - 16;

            this.anguloJoelho = Personagem.CalcularProdutoEscalar(Tornozelo, Joelho, Quadril);
            this.anguloJoelho = 90 - anguloJoelho;
        }
    }
}
