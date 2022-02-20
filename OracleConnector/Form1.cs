using Oracle.ManagedDataAccess.Client;
using System;
using System.Data.Common;
using System.Windows.Forms;

namespace OracleConnector
{
    public partial class Form1 : Form
    {
        private DbConnection connection;

        public Form1()
        {
            InitializeComponent();
        }

        private void CloseConnection()
        {
            if (connection == null)
            {
                return;
            }
            connection.Dispose();
            connection = null;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            CloseConnection();
            connection = new OracleConnection
            {
                ConnectionString = ConnectionStringTextBox.Text
            };
            connection.Open();
            ResultLabel.Text = "connected";
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            if (connection == null)
            {
                ResultLabel.Text = "Please connect first!";
                return;
            }
            using (DbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = QueryTextBox.Text;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    ResultLabel.Text = "Results:";
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; ++i)
                        {
                            ResultLabel.Text += String.Format((i == 0) ? "\n{0}" : ", {0}", reader[i]);
                        }

                    }
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            CloseConnection();
            ResultLabel.Text = "Not Connected";
        }
    }
}
