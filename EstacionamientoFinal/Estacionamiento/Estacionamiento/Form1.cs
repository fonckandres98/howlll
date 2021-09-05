using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Estacionamiento
{
    
    public partial class Form1 : Form
    {
        private MySqlConnection conexion = new MySqlConnection();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string server = "localhost";
            string puerto = "3306";
            string user = "root";
            string database = "estacionamiento_prueba";
            string pwd = "";
            string consulta = "SELECT * FROM usuarios WHERE (usuario='"+textBox1.Text+ "' ) and (contrasena='" +textBox2.Text +"');";

            string connStr = "server=" + server + ";uid=" + user + ";pwd=" + pwd + ";database="+database+";port=" + puerto;
            try
            {
               
               conexion = new MySqlConnection(connStr);
                conexion.Open();
                MySqlCommand resultado = new MySqlCommand(consulta,conexion);
                MySqlDataReader dr = resultado.ExecuteReader();

                if (dr.Read())
                {
                    MessageBox.Show("Bienvenido al sistema");
                    Menu ventana = new Menu();
                    
                    ventana.Show();
                    conexion.Close();
                    this.Hide();
                    
                }
                else {
                    MessageBox.Show("Error en las credenciales");

                }
               


            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al conectar al servidor de MySQL: " +
                    ex.Message, "Error al conectar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexion.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
