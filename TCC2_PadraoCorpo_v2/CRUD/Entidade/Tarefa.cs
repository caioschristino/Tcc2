using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC_CRUD;

namespace TCC2_PadraoCorpo_v2.CRUD.Entidade
{
    public class Tarefa : BaseInsert
    {
        public int idTarefa { get; set; }
        public int idUsuario { get; set; }
        public int idTipoTarefa { get; set; }
        public DateTime inicioTarefa { get; set; }
        public DateTime fimtarefa { get; set; }

        public override int insert(Object classe)
        {
            Tarefa tarefa = (Tarefa)classe;
            int id = 0;
            string sqlstmt = string.Empty;
            try
            {
                DAO dbCd = new DAO();
                DataSet sql;
                sqlstmt = string.Format("INSERT INTO TBL_TAREFA (CEA_USUARIO, CEA_TIPO_TAREFA, DT_INICIO, DT_FIM) VALUES({0},{1},'{2}','{3}') SELECT ID = @@IDENTITY"
                    , tarefa.idUsuario, tarefa.idTipoTarefa, tarefa.inicioTarefa.ToString("yyyy-MM-dd HH:mm:ss"), tarefa.fimtarefa.ToString("yyyy-MM-dd HH:mm:ss"));
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

        public void atualizarTerminoTarefa(DateTime fimtarefa)
        {
            string sqlstmt = string.Empty;
            try
            {
                DAO dbCd = new DAO();
                DataSet sql;
                sqlstmt = string.Format("UPDATE TBL_TAREFA SET DT_FIM = '{0}'", fimtarefa.ToString("yyyy-MM-dd HH:mm:ss"));
                sql = dbCd.createDataSet("atualizarDados", sqlstmt);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizarDados:" + e.Message + " - " + sqlstmt);
            }
        }
    }
}
