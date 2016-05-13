using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC_CRUD;

namespace TCC2_PadraoCorpo_v2.CRUD.Entidade
{
    public class TipoTarefa : BaseInsert
    {
        public int idTarefa { get; set; }
        public string nome { get; set; }

        public override int insert(Object classe)
        {
            TipoTarefa tipo = (TipoTarefa)classe;
            int id = 0;
            string sqlstmt = string.Empty;
            try
            {
                DAO dbCd = new DAO();
                DataSet sql;
                sqlstmt = string.Format("INSERT INTO TBL_TIPO_TAREFA (TXT_NOME) VALUES('{0}') SELECT ID = @@IDENTITY"
                    , tipo.nome);
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


        public static List<TipoTarefa> TarefasAtivas()
        {
            string sqlstmt = "SELECT * FROM TBL_TIPO_TAREFA";
            try
            {
                return (buscarBD(sqlstmt));
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar: " + e.Message + " -- " + sqlstmt);
            }
        }

        private static List<TipoTarefa> buscarBD(string sqlstmt)
        {
            List<TipoTarefa> tipos = new List<TipoTarefa>();
            try
            {
                DAO dbCd = new DAO();
                DataSet sql = dbCd.createDataSet("busca", sqlstmt);
                DataRowCollection resultado = sql.Tables["busca"].Rows;

                if (resultado.Count > 0)
                {
                    foreach (DataRow linha in resultado)
                    {
                        tipos.Add(new TipoTarefa()
                        {
                            idTarefa = Convert.ToInt32(linha["CDA_TIPO_TAREFA"]),
                            nome = linha["TXT_NOME"].ToString()
                        });
                    }
                }
                return (tipos);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar no BD: " + e.Message + " -- " + sqlstmt);
            }
        }

        public static int IdTarefa(string p)
        {
            string sqlstmt = string.Format("SELECT * FROM TBL_TIPO_TAREFA WHERE TXT_NOME = '{0}'", p);
            try
            {
                return (buscarBD(sqlstmt).FirstOrDefault().idTarefa);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar: " + e.Message + " -- " + sqlstmt);
            }
        }
    }
}
