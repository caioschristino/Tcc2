using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC_CRUD;

namespace TCC2_PadraoCorpo_v2.CRUD.Entidade
{
    public class Usuario : BaseInsert
    {
        public int idUsuario { get; set; }
        public int idade { get; set; }
        public string sexo { get; set; }

        public override int insert(Object classe)
        {
            Usuario usuario = (Usuario)classe;
            int id = 0;
            string sqlstmt = string.Empty;
            try
            {
                DAO dbCd = new DAO();
                DataSet sql;
                sqlstmt = string.Format("INSERT INTO TBL_USUARIO (NU_IDADE, TXT_SEXO) VALUES({0},'{1}') SELECT ID = @@IDENTITY"
                    , usuario.idade, usuario.sexo);
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
