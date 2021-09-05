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
    public partial class estacionamiento : Form
    {

        public estacionamiento()
        {
            InitializeComponent();

            textBox6.Text = DateTime.Now.ToShortTimeString();
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

                conexion.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al conectar al servidor de MySQL: " +
                    ex.Message, "Error al conectar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void estacionamiento_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string server = "localhost";
            string puerto = "3306";
            string user = "root";
            string pwd = "";
            string database = "estacionamiento_prueba";
            string consulta = "SELECT * FROM estacionamiento WHERE (patente='"+textBox1.Text+"')";
            string connStr = "server=" + server + ";uid=" + user + ";pwd=" + pwd + ";database=" + database + ";port=" + puerto;
            try
            {

                MySqlConnection conexion = new MySqlConnection(connStr);
                conexion.Open();

                MySqlCommand comando = new MySqlCommand(consulta,conexion);
                comando.ExecuteNonQuery();

                MySqlDataReader lector;
                
                lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    lector.Close();
                    MessageBox.Show("Patente ingresada exitosamente");
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = new MySqlCommand(consulta, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    /*Bindear los datos con la tabla y el datasource la fuente de datos a la tabla*/
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = tabla;
                    /*el datagriview1 mas el datasource es igual al binding source*/
                    dataGridView1.DataSource = bSource;
                    conexion.Close();
                }
                else {
                    lector.Close();
                    string consulta1 = "SELECT * FROM estacionamiento";
                    
                    
                    MySqlDataAdapter adaptador = new MySqlDataAdapter();
                    adaptador.SelectCommand = new MySqlCommand(consulta1, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    /*Bindear los datos con la tabla y el datasource la fuente de datos a la tabla*/
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = tabla;
                    /*el datagriview1 mas el datasource es igual al binding source*/
                    dataGridView1.DataSource = bSource;
                    conexion.Close();
                }



                conexion.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al conectar al servidor de MySQL: " +
                    ex.Message, "Error al conectar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            //seccion para calcular la diferencia de minutos mas el precio a pagar
            string hora = textBox5.Text;
            string hora2 = textBox6.Text;
            DateTime ahora = DateTime.Parse(hora);
            DateTime final = DateTime.Parse(hora2);
            var operacion = (final-ahora).TotalMinutes;
            var valorHora = int.Parse(textBox2.Text);
            var costo = operacion * valorHora;
            MessageBox.Show("la diferencia de minutos es; "+ operacion.ToString() +"el costo a pagar es; "+ costo );

            textBox4.Text = operacion.ToString();
            textBox3.Text = costo.ToString();
            
         


            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                


                textBox5.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();

                textBox1.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();

                textBox7.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();

            } catch (MySqlException ex)
            {
                MessageBox.Show("Error ");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menu ventana = new Menu();
            this.Close();
            ventana.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Pago ventana = new Pago();
            ventana.textBox1.Text = textBox1.Text;
            ventana.textBox6.Text = textBox3.Text;
            ventana.textBox2.Text = textBox5.Text;
            ventana.textBox3.Text = textBox6.Text;
            ventana.textBox8.Text = textBox7.Text;
            this.Close();
            ventana.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
