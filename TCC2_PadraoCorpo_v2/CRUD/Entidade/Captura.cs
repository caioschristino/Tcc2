using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC_CRUD;

namespace TCC2_PadraoCorpo_v2.CRUD.Entidade
{
    public class Captura : BaseInsert
    {
        public int idCaptura { get; set; }
        public int idTarefa { get; set; }
        public double capturaCabeca { get; set; }
        public double capturaColuna { get; set; }
        public double capturaJoelho { get; set; }

        public override int insert(Object classe)
        {
            Captura captura = (Captura)classe;
            int id = 0;
            string sqlstmt = string.Empty;
            try
            {
                DAO dbCd = new DAO();
                DataSet sql;
                sqlstmt = string.Format("INSERT INTO TBL_CAPTURA (CEA_TAREFA, NU_CAPTURA_CABECA, NU_CAPTURA_COLUNA, NU_CAPTURA_JOELHO) VALUES({0},{1},{2},{3}) SELECT ID = @@IDENTITY"
                    , captura.idTarefa, captura.capturaCabeca.ToString().Replace(",", "."), captura.capturaColuna.ToString().Replace(",", "."), captura.capturaJoelho.ToString().Replace(",", "."));
                sql = dbCd.createDataSet("inserirDados", sqlstmt);

                DataRowCollection resultado = sql.Tables["inserirDados"].Rows;
                if (resultado.Count > 0)
                {
                    id = System.Convert.ToInt32(resultado[0]["ID"]);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao inserirDados:" + e.Message + " - " + sqlstmt);
            }

            return id;
        }
    }
}
