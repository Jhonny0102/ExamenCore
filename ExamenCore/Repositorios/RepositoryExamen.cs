using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ExamenCore.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ExamenCore.Repositorios
{
    public class RepositoryExamen
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;
        public RepositoryExamen() 
        {
            string connectionString = "Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=NetCoreExamen;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = cn;
        }

        public List<string> GetNombreClientes()
        {
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_NOMBRECLIENTES";

            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            
            List<string> nombreClientes = new List<string>();
            while (this.reader.Read())
            {
                nombreClientes.Add(this.reader["EMPRESA"].ToString());
            }
            this.reader.Close();
            this.cn.Close();

            return nombreClientes;
        }

        public ModelsCliente GetInformacionCliente(string clienteSelect)
        {
            string sql = "SELECT * FROM CLIENTES WHERE EMPRESA=@cliente";
            SqlParameter pamCliente = new SqlParameter("@cliente", clienteSelect);
            this.com.Parameters.Add(pamCliente);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.reader.Read();

            string codcliente = this.reader["CODIGOCLIENTE"].ToString();
            string empresa = this.reader["EMPRESA"].ToString();
            string contacto = this.reader["CONTACTO"].ToString();
            string cargo = this.reader["CARGO"].ToString();
            string ciudad = this.reader["CIUDAD"].ToString();
            string telefono = this.reader["TELEFONO"].ToString();

            ModelsCliente cliente = new ModelsCliente();
            cliente.empresa= empresa;
            cliente.contacto = contacto;
            cliente.cargo = cargo;
            cliente.ciudad = ciudad;
            cliente.telefono = telefono;

            this.reader.Close();
            this.com.Parameters.Clear();
            this.cn.Close();
            return cliente;
        }

        public List<string> GetPedidosCliente(string codcliente)
        {
            SqlParameter pamCliente = new SqlParameter("@Cliente", codcliente);

            this.com.Parameters.Add(pamCliente);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_PEDIDOSCLIENTE";
            
            this.cn.Open();
            this.reader = this.com.ExecuteReader();

            List<string> pedidosCliente = new List<string>();
            while (this.reader.Read())
            {
                pedidosCliente.Add(this.reader["CODIGOPEDIDO"].ToString());
            }
            this.reader.Close();
            this.com.Parameters.Clear();
            this.cn.Close();

            return pedidosCliente;
        }

        public ModelsPedidos GetInformacionPedidos(string codigoCliente, string codigoPedido)
        {
            SqlParameter pamCod_Cliente = new SqlParameter("@COD_CLIENTE", codigoCliente);
            SqlParameter pamCod_Pedido = new SqlParameter("@COD_PEDIDO", codigoPedido);

            this.com.Parameters.Add(pamCod_Pedido);
            this.com.Parameters.Add(pamCod_Cliente);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INFOPEDIDO";

            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.reader.Read();

            string codPedido = this.reader["CODIGOPEDIDO"].ToString();
            string fechaEntrega = this.reader["FECHAENTREGA"].ToString();
            string formatoEnvio = this.reader["FORMAENVIO"].ToString();
            int importe = int.Parse(this.reader["IMPORTE"].ToString());

            ModelsPedidos pedido = new ModelsPedidos();
            pedido.codigopedido = codPedido;
            pedido.fechaentrega = fechaEntrega;
            pedido.formaenvio = formatoEnvio;
            pedido.importe = importe;

            this.reader.Close();
            this.com.Parameters.Clear();
            this.cn.Close();
            return pedido;
        }
    }
}
