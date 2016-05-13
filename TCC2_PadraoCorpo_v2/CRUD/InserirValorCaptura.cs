using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC2_PadraoCorpo_v2.CRUD
{
    public class InserirValorCaptura<T> : IValorCaptura where T: BaseInsert, new()
    {
        private T captura;

        public InserirValorCaptura()
        {
            this.captura = Activator.CreateInstance<T>();
        }

        public int inserirValorCapturado(Object classe)
        {
            return captura.insert(classe);
        }
    }
}
