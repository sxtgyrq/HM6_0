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
        public static bool AddItem(string fpCode, int height)
        {
            try
            {
                bool updateSuccess = false;
                string sQL = $"INSERT INTO fpdetail(BitcoinAddr,FPCode,Height,CanGetScore) VALUES('','{fpCode}',{height},0)";
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
            catch
            {
                return false;
            }
        }

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

        internal static List<ModelBase.Data.FpDetail> GetAll(MySqlConnection con, MySqlTransaction tran)
        {
            List<ModelBase.Data.FpDetail> Result = new List<ModelBase.Data.FpDetail>();
            var sQL = "SELECT BitcoinAddr,FPCode,Height,CanGetScore FROM fpdetail";
            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Result.Add(new ModelBase.Data.FpDetail
                        {
                            BitcoinAddr = Convert.ToString(reader["BitcoinAddr"]).Trim(),
                            FPCode = Convert.ToString(reader["FPCode"]).Trim(),
                            Height = Convert.ToInt32(reader["Height"]),
                            CanGetScore = Convert.ToBoolean(reader["CanGetScore"]),
                        });
                    }
                }
            }
            return Result;
            //   throw new NotImplementedException();
        }
    }
}
