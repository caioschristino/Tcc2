using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KinectService.SensorEsqueleto
{
    public interface IEsqueletoService
    {
        List<Skeleton> personagens { get; }

        void DefinirIntervaloCaptura(double max, double min);

        void CapturarPrimeiroEsqueletoCena(AllFramesReadyEventArgs e);
    }
}
