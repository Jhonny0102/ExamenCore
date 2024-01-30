using ExamenCore.Models;
using ExamenCore.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#region

//--OBTENES TODOS LOS CLIENTES
//CREATE PROCEDURE SP_NOMBRECLIENTES
//AS
//	SELECT EMPRESA  FROM CLIENTES
//GO

//-- Obtener pedidos de un cliente
//ALTER PROCEDURE SP_PEDIDOSCLIENTE
//(@Cliente nvarchar(50))
//AS
//    DECLARE @codCliente nvarchar(50)
//	SELECT @codCliente = CodigoCliente FROM CLIENTES
//	WHERE EMPRESA=@Cliente
	
//	SELECT CODIGOPEDIDO FROM PEDIDOS
//	WHERE CODIGOCLIENTE=@codCliente
//GO

//--Obtener informacion sobre un pedido
//CREATE PROCEDURE SP_INFOPEDIDO
//(@COD_CLIENTE NVARCHAR(50), @COD_PEDIDO NVARCHAR(50))
//AS
//	SELECT * FROM PEDIDOS 
//	WHERE CODIGOCLIENTE=@COD_CLIENTE AND CODIGOPEDIDO=@COD_PEDIDO
//GO

#endregion


namespace ExamenCore
{

    public partial class FormPractica : Form
    {
        RepositoryExamen repo;
        public FormPractica()
        {
            InitializeComponent();
            this.repo = new RepositoryExamen();
            this.LoadNombresClientes();
        }

        private void LoadNombresClientes()
        {
            List<string> nombresClientes = this.repo.GetNombreClientes();
            foreach (string data in nombresClientes)
            {
                this.cmbclientes.Items.Add(data);
            }
        }

        private void cmbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string clienteSelect = this.cmbclientes.SelectedItem.ToString();
            ModelsCliente cliente = this.repo.GetInformacionCliente(clienteSelect);

            this.txtempresa.Text = cliente.empresa.ToString();
            this.txtcontacto.Text = cliente.contacto.ToString();
            this.txtcargo.Text = cliente.cargo.ToString();
            this.txtciudad.Text = cliente.ciudad.ToString();
            this.txttelefono.Text = cliente.telefono.ToString();

            List<string> clientePedidos = this.repo.GetPedidosCliente(clienteSelect);
            foreach (string pedidos in clientePedidos)
            {
                this.lstpedidos.Items.Add(pedidos);
            }
        }

        private void lstpedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string clienteSelect = this.cmbclientes.SelectedItem.ToString();
            string pedidoSelect = this.lstpedidos.SelectedItem.ToString();

            ModelsPedidos pedidoInfo = this.repo.GetInformacionPedidos(clienteSelect,pedidoSelect);
            this.txtcodigopedido.Text = pedidoInfo.codigopedido.ToString();
            
        }
    }
}
