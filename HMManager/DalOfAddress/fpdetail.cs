using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalOfAddress
{
    public class FPDetail
    {
        public static bool DeleteItem(string fpCode, string height)
        {
            bool updateSuccess = false;
            string sQL = $"DELETE FROM fpdetail WHERE FPCode='{fpCode}' AND Height={height} AND BitcoinAddr=''";
            using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
            {
                con.Open();
                using (MySqlTransaction tran = con.BeginTransaction())
                {
                    using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                    {
                        if (command.ExecuteNonQuery() == 1)
                        {
                            tran.Commit();
                            updateSuccess = true;
                        }
                    }
                }
            }
            return updateSuccess;
        }

        public static bool UpdateItem(string fpCode, string height, string bitcoinAddr, string canGetScore)
        {
            bool updateSuccess = false;
            string sQL = $"UPDATE fpdetail SET BitcoinAddr='{bitcoinAddr}',CanGetScore={canGetScore} WHERE FPCode='{fpCode}' AND Height={height}";
            using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
            {
                con.Open();
                using (MySqlTransaction tran = con.BeginTransaction())
                {
                    using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                    {
                        if (command.ExecuteNonQuery() == 1)
                        {
                            tran.Commit();
                            updateSuccess = true;
                        }
                    }
                }
            }
            return updateSuccess;
            //  throw new NotImplementedException();
        }
    }
}
