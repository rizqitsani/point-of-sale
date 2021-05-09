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

        public static void AddTransaction(Transaction trans, List<TransactionDetail> details)
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
                long parentID = cmd.LastInsertedId;

                foreach(var detail in details)
                {
                    string sql2 = "INSERT INTO transaction_detail VALUES (@DetailID, @DetailName, @DetailQty)";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, con);
                    cmd2.CommandType = CommandType.Text;

                    cmd2.Parameters.Add("@DetailID", MySqlDbType.Int32).Value = parentID;
                    cmd2.Parameters.Add("@DetailName", MySqlDbType.VarChar).Value = detail.NamaBarang;
                    cmd2.Parameters.Add("@DetailQty", MySqlDbType.Int32).Value = detail.JumlahBarang;

                    cmd2.ExecuteNonQuery();
                }

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
