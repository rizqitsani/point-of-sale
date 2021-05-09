using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PointOfSale
{
    class DbTransaction
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=pbkk_ets";
            MySqlConnection con = new MySqlConnection(sql);
            try
            {
                con.Open();
            }
            catch (MySqlException error)
            {
                MessageBox.Show("MySQL Connection! \n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return con;
        }

        public static void AddTransaction(Transaction trans)
        {
            string sql = "INSERT INTO transaction VALUES (NULL, @TransactionTotal, @TransactionMethod)";
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@TransactionTotal", MySqlDbType.Int32).Value = trans.Total;
            cmd.Parameters.Add("@TransactionMethod", MySqlDbType.VarChar).Value = trans.MetodePembayaran;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Terima kasih sudah berbelanja!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(MySqlException error)
            {
                MessageBox.Show("Transaksi gagal! \n" + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();
        }
    }
}
