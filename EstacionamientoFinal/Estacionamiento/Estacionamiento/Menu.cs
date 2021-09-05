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
    public partial class Menu : Form
    {
        
        public Menu()
        {
            InitializeComponent();
            
            textBox5.Text = DateTime.Now.ToShortDateString();
            textBox3.Text = DateTime.Now.ToShortTimeString();
 
            string server = "localhost";
            string puerto = "3306";
            string user = "root";
            string pwd = "";
            string database = "estacionamiento_prueba";
            string consulta = "SELECT * FROM estacionamiento";
            string connStr = "server=" + server + ";uid=" + user + ";pwd=" + pwd + ";database=" + database + ";port=" + puerto;
            try
            {

                MySqlConnection conexion = new MySqlConnection(connStr);
                conexion.Open();

                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = new MySqlCommand(consulta, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                /*Bindear los datos con la tabla y el datasource la fuente de datos a la tabla*/
                BindingSource bSource = new BindingSource();
                bSource.DataSource = tabla;
                /*el datagriview1 mas el datasource es igual al binding source*/
                dataGridView1.DataSource = bSource;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al conectar al servidor de MySQL: " +
                    ex.Message, "Error al conectar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            estacionamiento ventana = new estacionamiento();
            this.Close();
            ventana.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           string patente = textBox1.Text;
           string hora = textBox3.Text;
           DateTime fecha = Convert.ToDateTime(textBox5.Text.ToString());
           string fechaformateada = fecha.ToString("yyyy-MM-dd");

            string ticket = "abierto";

            string consulta = "SELECT * FROM `estacionamiento` WHERE (patente='"+patente+"') and (ticket='"+ticket+"')";

            string insertar = "INSERT INTO `estacionamiento` (`id_parking`, `patente`, `hora_entra`, `hora_sali`, `fecha`, `ticket`, `valor`) VALUES ('null', '"+patente+"', '"+hora+"', NULL, '"+fechaformateada+"', '"+ticket+"', NULL);";

            string server = "localhost";
            string puerto = "3306";
            string user = "root";
            string pwd = "";
            string database = "estacionamiento_prueba";

            string connStr = "server=" + server + ";uid=" + user + ";pwd=" + pwd + ";database=" + database + ";port=" + puerto;

            try
            {

                MySqlConnection conexion = new MySqlConnection(connStr);
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand(consulta, conexion);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    MessageBox.Show("La patente ingresada ya se encuentra en el parking");
                    dr.Close();
                    conexion.Close();
                }
                else { 

                try
                {

                        MySqlConnection con = new MySqlConnection(connStr);
                        con.Open();

                        MySqlCommand insert = new MySqlCommand(insertar, con);
                        insert.ExecuteNonQuery();

                    MessageBox.Show("Patente ingresada correctamente ");
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error al conectar al servidor de MySQL: " +
                        ex.Message, "Error al conectar",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            

            }
        } catch (MySqlException ex) {
                MessageBox.Show("Error al insertar datos. la patente ya está en el estacionamiento ");
            }
}

        private void button4_Click(object sender, EventArgs e)
        {
            Reportes ventana2 = new Reportes();
            this.Close();
            ventana2.Show();
        }
    }
}
