using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KinectService.SensorEsqueleto
{
    public class EsqueletoService<T> : IEsqueletoService where T : EsqueletoSensor, new()
    {
        public T personagemT;

        public List<Skeleton> personagens { get; private set; }

        double distanciaMinima { get; set; }

        double distanciaMaxima { get; set; }

        public EsqueletoService(KinectSensor kinectSensor)
        {
            this.personagens = new List<Skeleton>();
            this.personagemT = Activator.CreateInstance<T>();
            this.personagemT.kinectSensor = kinectSensor;
        }

        public void DefinirIntervaloCaptura(double max, double min)
        {
            this.distanciaMaxima = max;
            this.distanciaMinima = min;
        }

        public void CapturarPrimeiroEsqueletoCena(AllFramesReadyEventArgs e)
        {
            Func<Skeleton, bool> funcaoCaptura = skeleton =>
            {
                SkeletonTrackingState estadoCaptura;
                estadoCaptura = skeleton.TrackingState;
                return
                    (estadoCaptura == SkeletonTrackingState.Tracked
                    && (skeleton.Position.Z >= distanciaMinima && skeleton.Position.Z <= distanciaMaxima));
            };

            using (SkeletonFrame esqueletoFrame = e.OpenSkeletonFrame())
            {
                if (esqueletoFrame == null)
                {
                    return;
                }

                this.personagens = this.personagemT.IdentificarEsqueletos(esqueletoFrame, funcaoCaptura);
            }
        }
    }
}
