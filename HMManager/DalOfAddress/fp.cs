using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalOfAddress
{
    public class FP
    {
        //INSERT INTO fp(FPName,lon,lat,baseHeight,FPCode) VALUES ('名称',10,10,700,'sss')
        public static bool Insert(DBModel.FP fp, out string msg)
        {
            fp.FPName = fp.FPName.Replace("'", "");
            msg = "";
            var sQL = $"INSERT INTO fp(FPName,lon,lat,baseHeight,FPCode) VALUES ('{fp.FPName.Trim()}',{fp.lon},{fp.lat},{fp.baseHeight},'{fp.FPCode}');";

            int influentRowCount;

            using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
            {
                con.Open();
                using (MySqlTransaction tran = con.BeginTransaction())
                {
                    using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                    {
                        influentRowCount = command.ExecuteNonQuery();
                        if (influentRowCount > 0)
                        {

                        }
                        else
                        {
                            tran.Rollback();
                            return false;
                        }
                    }
                    sQL = $"INSERT INTO fpdetail(BitcoinAddr,FPCode,Height,CanGetScore) VALUES('','{fp.FPCode}',0,1)";
                    using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                    {
                        influentRowCount = command.ExecuteNonQuery();
                        if (influentRowCount > 0)
                        {
                            tran.Commit();
                            return true;
                        }
                        else
                        {
                            tran.Rollback();
                            return false;
                        }
                    }
                }
            }

            return false;
            //throw new NotImplementedException();
        }

        public static void ShowAll()
        {
            var sQL = $"SELECT B.FPCode,B.lon,B.lat,B.baseHeight,A.Height,A.BitcoinAddr,A.CanGetScore,B.FPName FROM fpdetail A LEFT JOIN fp B ON A.FPCode=B.FPCode ORDER BY A.FPCode,A.Height ASC";
            using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
            {
                con.Open();
                using (MySqlTransaction tran = con.BeginTransaction())
                {
                    using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var fPCode = reader.GetString(0);
                                var lon = reader.GetDouble(1);
                                var lat = reader.GetDouble(2);
                                var baseHeight = reader.GetInt32(3);
                                var Height = reader.GetInt32(4);
                                var BitcoinAddr = reader.GetString(5);
                                var CanGetScore = reader.GetBoolean(6);
                                var fPName = reader.GetString(7);
                                var lineStr = $"fPCode:{fPCode}  position:{lon},{lat}  baseHeight:{baseHeight}  Height:{Height}  BitcoinAddr:{BitcoinAddr}  CanGetScore:{CanGetScore}  fPName{fPName}";
                                Console.WriteLine(lineStr);
                            }
                        }
                    }
                }
            }
            //  throw new NotImplementedException();
        }
    }
}
