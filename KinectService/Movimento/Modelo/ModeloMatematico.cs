using KinectService.Movimento.Pose;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectService.Movimento.Modelo
{
    public static class ModeloMatematico
    {
        private static Vector4 CriarVetorEntreDoisPontosV(Joint articulacao1, Joint articulacao2)
        {
            Vector4 vetorResultante = new Vector4();
            vetorResultante.X = Convert.ToSingle(
                Math.Sqrt(
                   Math.Pow(articulacao1.Position.X - articulacao2.Position.X, 2)));
            vetorResultante.Y = Convert.ToSingle(
                Math.Sqrt(
                   Math.Pow(articulacao1.Position.Y - articulacao2.Position.Y, 2)));
            vetorResultante.Z = Convert.ToSingle(
                Math.Sqrt(
                   Math.Pow(articulacao1.Position.Z - articulacao2.Position.Z, 2)));

            return vetorResultante;
        }

        private static Vector4 CriarVetorEntreDoisPontosW(Joint articulacao1, Joint articulacao2)
        { 
            var art1Y = articulacao1.Position.Y;
                if (articulacao1.JointType == JointType.KneeRight)
                {
                    art1Y = art1Y + 12.0F;
                }

                Vector4 vetorResultante = new Vector4();
                vetorResultante.X = Convert.ToSingle(
                    Math.Sqrt(
                       Math.Pow(articulacao1.Position.X - articulacao2.Position.X, 2)));
                vetorResultante.Y = Convert.ToSingle(
                    Math.Sqrt(
                       Math.Pow(art1Y - articulacao2.Position.Y, 2)));
                vetorResultante.Z = Convert.ToSingle(
                    Math.Sqrt(
                       Math.Pow(articulacao1.Position.Z - articulacao2.Position.Z, 2)));

                return vetorResultante;
        }

        private static double ProdutoModuloVetores(Vector4 vetorV, Vector4 vetorW)
        {
            return
                (
                    Math.Sqrt(
                        Math.Pow(vetorV.X, 2) +
                        Math.Pow(vetorV.Y, 2) +
                        Math.Pow(vetorV.Z, 2)
                    )
                    *
                    Math.Sqrt(
                        Math.Pow(vetorW.X, 2) +
                        Math.Pow(vetorW.Y, 2) +
                        Math.Pow(vetorW.Z, 2)
                    )
                );
        }

        private static double ProdutoVetores(Vector4 vetorV, Vector4 vetorW)
        {
            return vetorV.X * vetorW.X +
                    vetorV.Y * vetorW.Y +
                    vetorV.Z * vetorW.Z;
        }

        public static double CalcularProdutoEscalar(this Skeleton Personagem, Joint articulacao1, Joint articulacao2, Joint articulacao3)
        {
            Vector4 vetorV = CriarVetorEntreDoisPontosV(articulacao1, articulacao2);
            Vector4 vetorW = CriarVetorEntreDoisPontosW(articulacao2, articulacao3);

            double resultadoRadianos = Math.Acos(ProdutoVetores(vetorV, vetorW) / ProdutoModuloVetores(vetorV, vetorW));
            double resultadoGraus = resultadoRadianos * 180 / Math.PI;

            return resultadoGraus;
        }
    }
}
