using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpplib
{
    public class clsSIPAC
    {
        public List<_usuarioPerfiles> usuarios_GetPerfiles(string usuarioNombre)
        {
            List<_usuarioPerfiles> _userPerfiles  = null;
            _usuarioPerfiles _userData = null;
            string sqlQuery = "SELECT 0 AS IdUsuario,'Selecciona un Perfil.' AS NombreUsuario, -1 AS IdRol , 'Selecciona un Perfil' AS NombreRol UNION ALL SELECT _Usuarios.Id AS IdUsuario, _Usuarios.Nombre AS NombreUsuario , _Roles.Id AS IdRol , _Roles.Rol AS RolNombre FROM _Usuarios INNER JOIN _UsuariosRoles ON _USUARIOS.ID = _UsuariosRoles.IdUsuario INNER JOIN _Roles ON _Roles.Id = _UsuariosRoles.IdRol WHERE NOMBRE = '" + usuarioNombre + "'";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(sqlQuery);
            if (datos.Rows.Count > 0) 
            {
                _userPerfiles = new List<_usuarioPerfiles>();
                foreach (DataRow reg in datos.Rows) 
                {
                    _userData = new _usuarioPerfiles();
                    _userData.Id = int.Parse(reg["IdUsuario"].ToString());
                    _userData.Nombre = reg["NombreUsuario"].ToString();
                    _userData.IdRol = int.Parse(reg["IdRol"].ToString());
                    _userData.RolNombre = reg["NombreRol"].ToString();
                    _userPerfiles.Add(_userData);
                }
            }
            datos.Dispose();
            BD.CierraBD();
            return _userPerfiles;
        }

        public List<_Empresas> usuarios_GetEmpresas(string usuarioNombre, int IdPerfil)
        {
            List<_Empresas> _userUndadNegocio= null;
            _Empresas _userData = null;
            string sqlQuery = "SELECT -1 AS Id, 'Selecciona una empresa' AS Nombre UNION ALL SELECT cat_Empresas.Id, cat_Empresas.Nombre FROM usuario INNER JOIN cat_Empresas ON cat_Empresas.Id = usuario.IdEmpresa WHERE usuario.Nombre = '" + usuarioNombre + "' AND usuario.Grupo = " + IdPerfil.ToString() + ";";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(sqlQuery);
            if (datos.Rows.Count > 0)
            {
                _userUndadNegocio = new List<_Empresas>();
                foreach (DataRow reg in datos.Rows)
                {
                    _userData = new _Empresas();
                    _userData.Id = int.Parse(reg["Id"].ToString());
                    _userData.Nombre = reg["Nombre"].ToString();
                    _userUndadNegocio.Add(_userData);
                }
            }
            datos.Dispose();
            BD.CierraBD();
            return _userUndadNegocio;
        }

        public List<_usuarioRegistro> usuarios_GetRegistro(int IdUsuario)
        {
            List<_usuarioRegistro> _userData = null;
            _usuarioRegistro _userRegistro = null;
            string sqlQuery = "SELECT IdUsr ,Usuario ,Clave ,Nombre ,UnidadNegocio ,Grupo ,Estado ,Conectado ,Fecha ,IdEmpresa ,correo ,TipoRecCorreo FROM usuario WHERE usuario.idusr = " + IdUsuario.ToString() + ";";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(sqlQuery);
            if (datos.Rows.Count > 0)
            {
                _userData = new List<_usuarioRegistro>();
                foreach (DataRow reg in datos.Rows)
                {
                    _userRegistro = new _usuarioRegistro();

                    _userRegistro.Id = int.Parse(reg["IdUsr"].ToString());
                    _userRegistro.Usuario = reg["Usuario"].ToString();
                    _userRegistro.Clave = reg["Clave"].ToString();
                    _userRegistro.Nombre = reg["Nombre"].ToString();
                    _userRegistro.UnidadNegocio = reg["UnidadNegocio"].ToString();
                    _userRegistro.Grupo = int.Parse(reg["Grupo"].ToString());
                    _userRegistro.Estado = int.Parse(reg["Estado"].ToString());
                    _userRegistro.Conectado = reg["Conectado"].ToString();
                    _userRegistro.Fecha = DateTime.Parse(reg["Fecha"].ToString());
                    _userRegistro.IdEmpresa = int.Parse(reg["IdEmpresa"].ToString());
                    _userRegistro.correo = reg["correo"].ToString();
                    _userRegistro.TipoRecCorreo = reg.IsNull("TipoRecCorreo") ? -1 : int.Parse(reg["TipoRecCorreo"].ToString());
                    
                    _userData.Add(_userRegistro);
                }
            }
            datos.Dispose();
            BD.CierraBD();
            return _userData;
        }

        public List<_usuarioRegistro> usuarios_GetRegistros(string usuarioNombre)
        {
            List<_usuarioRegistro> _userData = null;
            _usuarioRegistro _userRegistro = null;
            string sqlQuery = "SELECT IdUsr ,Usuario ,Clave ,Nombre ,UnidadNegocio ,Grupo, (SELECT _Roles.Rol FROM _Roles WHERE _Roles.Id = usuario.Grupo) AS GrupoNombre ,Estado ,Conectado ,Fecha ,IdEmpresa, (SELECT cat_Empresas.Nombre FROM cat_Empresas WHERE cat_Empresas.Id = usuario.IdEmpresa) AS EmpresaNombre ,correo ,TipoRecCorreo FROM usuario WHERE usuario.Nombre = '" + usuarioNombre + "';";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(sqlQuery);
            if (datos.Rows.Count > 0)
            {
                _userData = new List<_usuarioRegistro>();
                foreach (DataRow reg in datos.Rows)
                {
                    _userRegistro = new _usuarioRegistro();

                    _userRegistro.Id = int.Parse(reg["IdUsr"].ToString());
                    _userRegistro.Usuario = reg["Usuario"].ToString();
                    _userRegistro.Clave = reg["Clave"].ToString();
                    _userRegistro.Nombre = reg["Nombre"].ToString();
                    _userRegistro.UnidadNegocio = reg["UnidadNegocio"].ToString();
                    _userRegistro.Grupo = int.Parse(reg["Grupo"].ToString());
                    _userRegistro.GrupoNombre = reg["GrupoNombre"].ToString();
                    _userRegistro.Estado = int.Parse(reg["Estado"].ToString());
                    _userRegistro.Conectado = reg["Conectado"].ToString();
                    _userRegistro.Fecha = DateTime.Parse(reg["Fecha"].ToString());
                    _userRegistro.IdEmpresa = int.Parse(reg["IdEmpresa"].ToString());
                    _userRegistro.EmpresaNombre = reg["EmpresaNombre"].ToString();
                    _userRegistro.correo = reg["correo"].ToString();
                    if (!reg.IsNull("TipoRecCorreo")) _userRegistro.TipoRecCorreo = int.Parse(reg["TipoRecCorreo"].ToString());
                    
                    _userData.Add(_userRegistro);
                }
            }
            datos.Dispose();
            BD.CierraBD();
            return _userData;
        }

        public int UnidadNegocio(int IdUsuario, int IdEmpresa)
        {
            int _resultado = -1;
            string sqlQuery = "SELECT DISTINCT UnidadNegocio AS UN FROM usuario WHERE usuario.IdEmpresa = " + IdEmpresa.ToString() + " AND usuario.Nombre = (SELECT usuario.Nombre FROM usuario WHERE IdUsr = " + IdUsuario.ToString() + ");";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(sqlQuery);
            if (datos.Rows.Count > 0)
            {
                if (datos.Rows.Count == 1) 
                {
                    DataRow _registro = datos.Rows[0];
                    _resultado = int.Parse(_registro["UN"].ToString());
                }
                else
                {
                    // Generamos error.
                    throw new System.Exception("X20999001. No puede tener un usuario más de una unidad de pago para la empresa seleccioanda.");
                }
            }

            datos.Dispose();
            BD.CierraBD();
            datos = null;
            BD = null;
            
            return _resultado;
        }

        public List<SIPAC_Clientes> Clientes_Get(int IdEmpresa)
        {
            //IdEmpresa = 0; // Se realiza la asignación debido a que no existen asociación de clientes con empresas.
            List<SIPAC_Clientes> _clientes = null;
            SIPAC_Clientes _userData = null;
            string sqlQuery = "SELECT id AS Id, FechaRegistro, IdEmpresa, Nombre, Rfc, Direccion, Ciudad, Estado, Cp, ContactoProy, ContactoFact, Correo, Telefono, Extencion, ClienteSolomon, activo FROM cat_clientes WHERE cat_Clientes.activo = 1 AND cat_Clientes.IdEmpresa = " + IdEmpresa.ToString() + " ORDER BY cat_Clientes.Nombre;";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(sqlQuery);
            if (datos.Rows.Count > 0)
            {
                _clientes = new List<SIPAC_Clientes>();
                foreach (DataRow reg in datos.Rows)
                {
                    _userData = new SIPAC_Clientes();
                    _userData.Id = int.Parse(reg["Id"].ToString());
                    _userData.FechaRegistro = DateTime.Parse(reg["FechaRegistro"].ToString());
                    _userData.IdEmpresa = int.Parse(reg["IdEmpresa"].ToString());
                    _userData.Nombre = reg["Nombre"].ToString();
                    _userData.RFC = reg["Rfc"].ToString();
                    _userData.Direccion = reg["Direccion"].ToString();
                    _userData.Ciudad = reg["Ciudad"].ToString();
                    _userData.Estado = reg["Estado"].ToString();
                    _userData.CP = reg["Cp"].ToString();
                    _userData.ContactoProy = reg["ContactoProy"].ToString();
                    _userData.ContactoFact = reg["ContactoFact"].ToString();
                    _userData.Correo = reg["Correo"].ToString();
                    _userData.Telefono = reg["Telefono"].ToString();
                    _userData.Extencion = reg["Extencion"].ToString();
                    _userData.ClienteSolomon = reg["ClienteSolomon"].ToString();
                    _userData.Activo = reg["activo"].ToString() == "1" ? true : false;
                    _clientes.Add(_userData);
                }
            }
            datos.Dispose();
            BD.CierraBD();
            return _clientes;
        }

        public List<SIPAC_Instalaciones> Instalaciones_Get(int IdParent)
        {
            List<SIPAC_Instalaciones> _userInstalaciones = null;
            SIPAC_Instalaciones _userData = null;
            string sqlQuery = "SELECT Id, Nombre FROM cat_Instalaciones WHERE Activo = 1 AND IdParent = " + IdParent.ToString() + " ORDER BY Orden ASC;";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(sqlQuery);
            if (datos.Rows.Count > 0)
            {
                _userInstalaciones = new List<SIPAC_Instalaciones>();
                foreach (DataRow reg in datos.Rows)
                {
                    _userData = new SIPAC_Instalaciones();
                    _userData.Id = int.Parse(reg["Id"].ToString());
                    _userData.Nombre = reg["Nombre"].ToString();
                    _userInstalaciones.Add(_userData);
                }
            }
            datos.Dispose();
            BD.CierraBD();
            return _userInstalaciones;
        }

        public List<SIPAC_InstalacionesOcupacion> Instalaciones_Ocupacion_Get(int IdParent)
        {
            List<SIPAC_InstalacionesOcupacion> _userInstalaciones = null;
            SIPAC_InstalacionesOcupacion _userData = null;
            string sqlQuery = "SELECT cat_Instalaciones.Id AS IdInstalacion, cat_Instalaciones.IdParent , cat_Instalaciones.Nombre , cat_Instalaciones.M2 , cat_Instalaciones.Ocupado , cat_Instalaciones.Orden , cat_Instalaciones.Activo , cat_Clientes.Id AS IdCliente , cat_Clientes.Nombre AS Cliente FROM cat_Instalaciones INNER JOIN InstalacionesClientes ON cat_Instalaciones.Id = InstalacionesClientes.IdInstalacion INNER JOIN cat_Clientes ON cat_Clientes.Id = InstalacionesClientes.IdCliente WHERE cat_Instalaciones.IdParent = " + IdParent.ToString() + " ORDER BY cat_Instalaciones.Orden;";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(sqlQuery);
            if (datos.Rows.Count > 0)
            {
                _userInstalaciones = new List<SIPAC_InstalacionesOcupacion>();
                foreach (DataRow reg in datos.Rows)
                {
                    _userData = new SIPAC_InstalacionesOcupacion();
                    _userData.Id = int.Parse(reg["Id"].ToString());
                    _userData.Nombre = reg["Nombre"].ToString();
                    _userData.M2 = int.Parse(reg["M2"].ToString()); ;
                    _userData.Ocupado = bool.Parse(reg["Ocupado"].ToString());
                    _userData.IdCliente = int.Parse(reg["IdCliente"].ToString()); ;
                    _userData.Cliente = reg["Cliente"].ToString(); ;
                    _userInstalaciones.Add(_userData);
                }
            }
            datos.Dispose();
            BD.CierraBD();
            return _userInstalaciones;
        }

        public List<SIPAC_InstalacionesOcupacion> Instalaciones_Get(string CondicionWhere)
        {
            if (CondicionWhere.Length > 0)
            {
                CondicionWhere = "WHERE NOT IdParent = 0 AND cat_Instalaciones.Activo = 1 " + CondicionWhere;
            }
            else
            {
                CondicionWhere = "WHERE NOT IdParent = 0 AND cat_Instalaciones.Activo = 1";
            }


            List<SIPAC_InstalacionesOcupacion> _userInstalaciones = null;
            SIPAC_InstalacionesOcupacion _userData = null;
            string sqlQuery = "SELECT cat_Instalaciones.Id AS IdInstalacion, cat_Instalaciones.IdParent , IIF(cat_Instalaciones.IdParent > 0, (SELECT CI.NOMBRE FROM CAT_INSTALACIONES CI WHERE CI.ID = cat_Instalaciones.IdParent) + ' - ' + cat_Instalaciones.Nombre ,cat_Instalaciones.Nombre) AS Nombre , cat_Instalaciones.M2 , cat_Instalaciones.Ocupado , cat_Instalaciones.Orden , cat_Instalaciones.Activo , cat_Clientes.Id AS IdCliente , cat_Clientes.Nombre AS Cliente FROM cat_Instalaciones LEFT JOIN InstalacionesClientes ON cat_Instalaciones.Id = InstalacionesClientes.IdInstalacion LEFT JOIN cat_Clientes ON cat_Clientes.Id = InstalacionesClientes.IdCliente " + CondicionWhere +  " ORDER BY cat_Instalaciones.Orden;";
            mbd.BD BD = new mbd.BD();
            DataTable datos = BD.LeeDatos(sqlQuery);
            if (datos.Rows.Count > 0)
            {
                _userInstalaciones = new List<SIPAC_InstalacionesOcupacion>();
                foreach (DataRow reg in datos.Rows)
                {
                    _userData = new SIPAC_InstalacionesOcupacion();
                    _userData.Id = int.Parse(reg["IdInstalacion"].ToString());
                    _userData.Nombre = reg["Nombre"].ToString();
                    _userData.M2 = int.Parse(reg["M2"].ToString()); ;
                    _userData.Ocupado = bool.Parse(reg["Ocupado"].ToString());
                    _userData.IdCliente = reg.IsNull("IdCliente") ? 0 : int.Parse(reg["IdCliente"].ToString()); 
                    _userData.Cliente = reg["Cliente"].ToString(); 
                    _userInstalaciones.Add(_userData);
                }
            }
            datos.Dispose();
            BD.CierraBD();
            return _userInstalaciones;
        }
    }

    [Serializable]
    public class SIPAC_Clientes
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CP { get; set; }
        public string ContactoProy { get; set; }
        public string ContactoFact { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Extencion { get; set; }
        public string ClienteSolomon { get; set; }
        public bool Activo { get; set; }
    }

    [Serializable]
    public class SIPAC_Instalaciones
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    [Serializable]
    public class SIPAC_InstalacionesOcupacion 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int M2 { get; set; }
        public bool Ocupado { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
    }

    [Serializable]
    public class _usuarioRegistro
    {
        public int Id { get; set; }
        public string Usuario { get; set; }

        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string UnidadNegocio { get; set; }
        public int Grupo { get; set; }
        public string GrupoNombre { get; set; }
        public int Estado { get; set; }
        public string Conectado { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEmpresa { get; set; }
        public string EmpresaNombre { get; set; }
        public string correo { get; set; }
        public int TipoRecCorreo { get; set; }

    }

    [Serializable]
    public class _usuarioPerfiles
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdRol { get; set; }
        public string RolNombre { get; set; }
    }

    [Serializable]
    public class _Empresas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

}
