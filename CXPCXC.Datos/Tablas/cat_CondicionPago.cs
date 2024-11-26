using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mod = CXPCXC.Modelos;

namespace CXPCXC.Datos.Tablas
{
    public class cat_CondicionPago
    {
        internal AccesoDatos b { get; set; } = new AccesoDatos();

        protected List<mod.cat_CondicionPago> Seleccionar_PorIdEmpresa(string idempresa)
        {
            b.ExecuteCommandSP("cat_CondicionPago_Seleccionar_PorIdEmpresa");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            List<mod.cat_CondicionPago> resultado = new List<mod.cat_CondicionPago>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cat_CondicionPago item = new mod.cat_CondicionPago();
                item.Id = int.Parse(reader["id"].ToString());
                item.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                item.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                item.Titulo = reader["titulo"].ToString();
                item.NumDias = int.Parse(reader["dias"].ToString());
                item.Activo = int.Parse(reader["activo"].ToString());
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }

        protected mod.cat_CondicionPago Seleccionar_PorId(int id)
        {
            b.ExecuteCommandSP("cat_CondicionPago_Seleccionar_PorId");
            b.AddParameter("@id", id, SqlDbType.Int);
            mod.cat_CondicionPago resultado = new mod.cat_CondicionPago();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                resultado.Id = int.Parse(reader["id"].ToString());
                resultado.FechaRegistro = DateTime.Parse(reader["fecharegistro"].ToString());
                resultado.IdEmpresa = int.Parse(reader["idempresa"].ToString());
                resultado.Titulo = reader["titulo"].ToString();
                resultado.NumDias = int.Parse(reader["dias"].ToString());
                resultado.Activo = int.Parse(reader["activo"].ToString());
            }
            b.CloseConnection();
            return resultado;
        }

        protected bool Seleccionar_SiExiste(mod.cat_CondicionPago items)
        {
            b.ExecuteCommandSP("cat_CondicionPago_Seleccionar_ValidarSiExiste_PorIdEmpresa_PorTitulo");
            b.AddParameter("@idempresa", items.IdEmpresa, SqlDbType.Int);
            b.AddParameter("@titulo", items.Titulo, SqlDbType.VarChar, 100);
            if (b.SelectString() != "")
                return true;
            else
                return false;
        }

        protected List<mod.cat_CondicionPago> SeleccionarParaListaDesplegable_Activos(string idempresa)
        {            
            b.ExecuteCommandSP("cat_CondicionPago_Seleccionar_ParaLista_PorIdEmpresa");
            b.AddParameter("@idempresa", idempresa, SqlDbType.Int);
            List<mod.cat_CondicionPago> resultado = new List<mod.cat_CondicionPago>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                mod.cat_CondicionPago item = new mod.cat_CondicionPago
                {
                    Id = int.Parse(reader["id"].ToString()),
                    Titulo = reader["titulo"].ToString()
                };
                resultado.Add(item);
            }
            b.CloseConnection();
            return resultado;
        }


        /* Definir utilidad de estos procesos */
        /*
        protected int daSiguienteIdentificador()
        {
            //int Id = 0;
            //String SqlCmd = "INSERT INTO cat_CondicionPagoCtrl(Fecha) VALUES(getdate())";
            //mbd.BD BD = new mbd.BD();
            //bool resultado = BD.EjecutaCmd(SqlCmd);
            //if (resultado)
            //{
            //    DataTable Datos = BD.LeeDatos("Select @@Identity as Id");
            //    if (Datos.Rows.Count > 0)
            //    {
            //        if (!Datos.Rows[0].IsNull("Id")) { Id = Convert.ToInt32(Datos.Rows[0]["Id"]); }
            //    }
            //}
            //return Id;
            return 0;
        }

        public bool Agregar(mod.cat_CondicionPago items)
        {
            bool resultado = false;
            int Id = daSiguienteIdentificador();
            if (Id > 0)
            {
                StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_CondicionPago(");
                SqlCmd.Append("Id");
                SqlCmd.Append(",FechaRegistro");
                SqlCmd.Append(",IdEmpresa");
                SqlCmd.Append(",Titulo");
                SqlCmd.Append(",Dias");
                SqlCmd.Append(",Activo");

                SqlCmd.Append(")");

                SqlCmd.Append(" VALUES (");
                SqlCmd.Append(Id.ToString());
                SqlCmd.Append(",getdate()");
                SqlCmd.Append("," + pDatos.IdEmpresa.ToString());
                SqlCmd.Append(",'" + pDatos.Titulo + "'");
                SqlCmd.Append("," + pDatos.NumDias);
                SqlCmd.Append("," + pDatos.Activo);
                SqlCmd.Append(");");
                mbd.BD BD = new mbd.BD();
                resultado = BD.EjecutaCmd(SqlCmd.ToString());

                BD.CierraBD();
            }
            return resultado;
        }

        */

        protected bool Modificar_Titulo_Dias(mod.cat_CondicionPago items)
        {
            b.ExecuteCommandSP("cat_CondicionPago_Modificar_Titulo_Dias");
            b.AddParameter("@id", items.Id, SqlDbType.Int);
            b.AddParameter("@id", items.Titulo, SqlDbType.VarChar, 100);
            b.AddParameter("@dias", items.NumDias, SqlDbType.Int);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }


    }
}
