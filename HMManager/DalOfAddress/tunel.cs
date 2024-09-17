using DalOfAddress.DBModel;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.Sec;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalOfAddress
{
    public class Tunel
    {
        public static void Add(string FPCodeFrom, int HeightFrom, string FPCodeTo, int HeightTo, int speed, int roadType, out string msg)
        {
            //  throw new NotImplementedException();
            switch (roadType)
            {
                case 0:
                    {
                        var sQL = $"UPDATE tunel SET IsUsing=0 WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                        using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
                        {
                            con.Open();
                            using (MySqlTransaction tran = con.BeginTransaction())
                            {
                                using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                {
                                    var success = command.ExecuteNonQuery() == 1;
                                    tran.Commit();
                                    if (success) msg = "设置成功";
                                    else msg = "设置失败";
                                }
                            }
                        }
                    }; break;
                case 1:
                    {
                        int count;
                        {

                            using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
                            {
                                con.Open();
                                using (MySqlTransaction tran = con.BeginTransaction())
                                {
                                    {
                                        var sQL = $"SELECT COUNT(*) FROM tunel WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                                        using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                        {
                                            count = Convert.ToInt32(command.ExecuteScalar());
                                        }
                                    }
                                    if (count == 0)
                                    {
                                        var sQL = $"INSERT INTO tunel(FPCodeFrom,HeightFrom,FPCodeTo,HeightTo,IsRegionTransfer,Speed,IsUsing) VALUES ('{FPCodeFrom}',{HeightFrom},'{FPCodeTo}',{HeightTo},0,{speed},1);";
                                        using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                        {
                                            if (command.ExecuteNonQuery() == 1)
                                            {
                                                tran.Commit();
                                                msg = "录入成功";
                                            }
                                            else
                                            {
                                                Console.WriteLine($"插入失败");
                                                tran.Rollback();
                                                msg = "录入失败";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var sQL = $"UPDATE tunel SET IsRegionTransfer=0,Speed={speed},IsUsing=1 WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                                        using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                        {
                                            if (command.ExecuteNonQuery() == 1)
                                            {
                                                tran.Commit();
                                                msg = "修改成功";
                                            }
                                            else
                                            {
                                                Console.WriteLine($"插入失败");
                                                tran.Rollback();
                                                msg = "修改失败";
                                            }
                                        }
                                    }
                                }
                            }
                        }


                    }; break;
                case 2:
                    {
                        {
                            int count;
                            {

                                using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
                                {
                                    con.Open();
                                    using (MySqlTransaction tran = con.BeginTransaction())
                                    {
                                        {
                                            var sQL = $"SELECT COUNT(*) FROM tunel WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                            {
                                                count = Convert.ToInt32(command.ExecuteScalar());
                                            }
                                        }
                                        if (count == 0)
                                        {
                                            var sQL = $"INSERT INTO tunel(FPCodeFrom,HeightFrom,FPCodeTo,HeightTo,IsRegionTransfer,Speed,IsUsing) VALUES ('{FPCodeFrom}',{HeightFrom},'{FPCodeTo}',{HeightTo},0,{speed},1);";
                                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                            {
                                                if (command.ExecuteNonQuery() == 1)
                                                {
                                                    tran.Commit();
                                                    msg = "录入成功";
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"插入失败");
                                                    tran.Rollback();
                                                    msg = "录入失败";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var sQL = $"UPDATE tunel SET IsRegionTransfer=0,Speed={speed},IsUsing=1 WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                            {
                                                if (command.ExecuteNonQuery() == 1)
                                                {
                                                    tran.Commit();
                                                    msg = "修改成功";
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"插入失败");
                                                    tran.Rollback();
                                                    msg = "修改失败";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        {
                            int count;
                            {
                                var centercode = FPCodeFrom;
                                var centerHeight = HeightFrom;
                                FPCodeFrom = FPCodeTo;
                                HeightFrom = HeightTo;
                                FPCodeTo = centercode;
                                HeightTo = centerHeight;
                                using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
                                {
                                    con.Open();
                                    using (MySqlTransaction tran = con.BeginTransaction())
                                    {
                                        {
                                            var sQL = $"SELECT COUNT(*) FROM tunel WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                            {
                                                count = Convert.ToInt32(command.ExecuteScalar());
                                            }
                                        }
                                        if (count == 0)
                                        {
                                            var sQL = $"INSERT INTO tunel(FPCodeFrom,HeightFrom,FPCodeTo,HeightTo,IsRegionTransfer,Speed,IsUsing) VALUES ('{FPCodeFrom}',{HeightFrom},'{FPCodeTo}',{HeightTo},0,{speed},1);";
                                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                            {
                                                if (command.ExecuteNonQuery() == 1)
                                                {
                                                    tran.Commit();
                                                    msg = "录入成功";
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"插入失败");
                                                    tran.Rollback();
                                                    msg = "录入失败";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var sQL = $"UPDATE tunel SET IsRegionTransfer=0,Speed={speed},IsUsing=1 WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                            {
                                                if (command.ExecuteNonQuery() == 1)
                                                {
                                                    tran.Commit();
                                                    msg = "修改成功";
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"插入失败");
                                                    tran.Rollback();
                                                    msg = "修改失败";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }; break;
                case 3:
                    {
                        speed = 30 * 100000;
                        int count;
                        {

                            using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
                            {
                                con.Open();
                                using (MySqlTransaction tran = con.BeginTransaction())
                                {
                                    {
                                        var sQL = $"SELECT COUNT(*) FROM tunel WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                                        using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                        {
                                            count = Convert.ToInt32(command.ExecuteScalar());
                                        }
                                    }
                                    if (count == 0)
                                    {
                                        var sQL = $"INSERT INTO tunel(FPCodeFrom,HeightFrom,FPCodeTo,HeightTo,IsRegionTransfer,Speed,IsUsing) VALUES ('{FPCodeFrom}',{HeightFrom},'{FPCodeTo}',{HeightTo},1,{speed},1);";
                                        using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                        {
                                            if (command.ExecuteNonQuery() == 1)
                                            {
                                                tran.Commit();
                                                msg = "录入成功";
                                            }
                                            else
                                            {
                                                Console.WriteLine($"插入失败");
                                                tran.Rollback();
                                                msg = "录入失败";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var sQL = $"UPDATE tunel SET IsRegionTransfer=1,Speed={speed},IsUsing=1 WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                                        using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                        {
                                            if (command.ExecuteNonQuery() == 1)
                                            {
                                                tran.Commit();
                                                msg = "修改成功";
                                            }
                                            else
                                            {
                                                Console.WriteLine($"插入失败");
                                                tran.Rollback();
                                                msg = "修改失败";
                                            }
                                        }
                                    }
                                }
                            }
                        }


                    }; break;
                case 4:
                    {
                        speed = 30 * 100000;
                        {
                            int count;
                            {

                                using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
                                {
                                    con.Open();
                                    using (MySqlTransaction tran = con.BeginTransaction())
                                    {
                                        {
                                            var sQL = $"SELECT COUNT(*) FROM tunel WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                            {
                                                count = Convert.ToInt32(command.ExecuteScalar());
                                            }
                                        }
                                        if (count == 0)
                                        {
                                            var sQL = $"INSERT INTO tunel(FPCodeFrom,HeightFrom,FPCodeTo,HeightTo,IsRegionTransfer,Speed,IsUsing) VALUES ('{FPCodeFrom}',{HeightFrom},'{FPCodeTo}',{HeightTo},1,{speed},1);";
                                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                            {
                                                if (command.ExecuteNonQuery() == 1)
                                                {
                                                    tran.Commit();
                                                    msg = "录入成功";
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"插入失败");
                                                    tran.Rollback();
                                                    msg = "录入失败";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var sQL = $"UPDATE tunel SET IsRegionTransfer=1,Speed={speed},IsUsing=1 WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                            {
                                                if (command.ExecuteNonQuery() == 1)
                                                {
                                                    tran.Commit();
                                                    msg = "修改成功";
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"插入失败");
                                                    tran.Rollback();
                                                    msg = "修改失败";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        {
                            int count;
                            {
                                var centercode = FPCodeFrom;
                                var centerHeight = HeightFrom;
                                FPCodeFrom = FPCodeTo;
                                HeightFrom = HeightTo;
                                FPCodeTo = centercode;
                                HeightTo = centerHeight;
                                using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
                                {
                                    con.Open();
                                    using (MySqlTransaction tran = con.BeginTransaction())
                                    {
                                        {
                                            var sQL = $"SELECT COUNT(*) FROM tunel WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                            {
                                                count = Convert.ToInt32(command.ExecuteScalar());
                                            }
                                        }
                                        if (count == 0)
                                        {
                                            var sQL = $"INSERT INTO tunel(FPCodeFrom,HeightFrom,FPCodeTo,HeightTo,IsRegionTransfer,Speed,IsUsing) VALUES ('{FPCodeFrom}',{HeightFrom},'{FPCodeTo}',{HeightTo},0,{speed},1);";
                                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                            {
                                                if (command.ExecuteNonQuery() == 1)
                                                {
                                                    tran.Commit();
                                                    msg = "录入成功";
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"插入失败");
                                                    tran.Rollback();
                                                    msg = "录入失败";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var sQL = $"UPDATE tunel SET IsRegionTransfer=0,Speed={speed},IsUsing=1 WHERE FPCodeFrom='{FPCodeFrom}' AND HeightFrom={HeightFrom} AND FPCodeTo='{FPCodeTo}' AND HeightTo={HeightTo};";
                                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                            {
                                                if (command.ExecuteNonQuery() == 1)
                                                {
                                                    tran.Commit();
                                                    msg = "修改成功";
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"插入失败");
                                                    tran.Rollback();
                                                    msg = "修改失败";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }; break;
                default:
                    {
                        msg = "失败";
                    }; break;

            }
        }

        public static List<ModelBase.Data.Segment> GetAll()
        {
            List<ModelBase.Data.Segment> result = new List<ModelBase.Data.Segment>();
            var sQL = @"SELECT
	B.lon AS StartLon,B.lat AS StartLat,A.HeightFrom+B.baseHeight AS StartHeight,B.baseHeight AS StartBaseHeight,
	C.lon AS EndLon,C.lat AS EndLat,A.HeightTo+C.baseHeight AS EndHeight,C.baseHeight AS EndBaseHeight,
	A.Speed,A.FPCodeFrom,A.FPCodeTo
	
FROM
	tunel A
	LEFT JOIN fp B ON A.FPCodeFrom = B.FPCode
	LEFT JOIN fp C ON A.FPCodeTo = C.FPCode 
WHERE
	IsUsing =1 ORDER BY A.FPCodeFrom ASC,A.HeightFrom ASC,A.FPCodeTo ASC,A.HeightTo ASC;";
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
                                result.Add(new ModelBase.Data.Segment()
                                {
                                    StartHeight = Convert.ToInt32(reader["StartHeight"]),
                                    StartBaseHeight = Convert.ToInt32(reader["StartBaseHeight"]),
                                    StartLon = Convert.ToDouble(reader["StartLon"]),
                                    StartLat = Convert.ToDouble(reader["StartLat"]),
                                    EndLon = Convert.ToDouble(reader["EndLon"]),
                                    EndLat = Convert.ToDouble(reader["EndLat"]),
                                    EndHeight = Convert.ToInt32(reader["EndHeight"]),
                                    EndBaseHeight = Convert.ToInt32(reader["EndBaseHeight"]),
                                    FPCodeFrom = Convert.ToString(reader["FPCodeFrom"]).Trim(),
                                    FPCodeTo = Convert.ToString(reader["FPCodeTo"]).Trim(),
                                    Speed = Convert.ToInt32(reader["Speed"]),
                                });
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static void UpdateItem()
        {
            // FPDetail.GetAll();
            using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
            {
                con.Open();
                using (MySqlTransaction tran = con.BeginTransaction())
                {
                    var allItems = FPDetail.GetAll(con, tran);
                    for (int i = 0; i < allItems.Count; i++)
                    {
                        for (int j = 0; j < allItems.Count; j++)
                        {
                            if (i == j)
                            {
                                continue;
                            }
                            else if (allItems[i].FPCode != allItems[j].FPCode)
                            {
                                continue;
                            }
                            else
                            {
                                bool needToInsert = false;
                                {
                                    var sQL = $"SELECT COUNT(*) FROM tunel WHERE FPCodeFrom='{allItems[i].FPCode}' AND HeightFrom={allItems[i].Height} AND FPCodeTo='{allItems[j].FPCode}' AND HeightTo={allItems[j].Height};";

                                    using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                    {
                                        var selectCount = Convert.ToInt32(command.ExecuteScalar());
                                        if (selectCount == 1)
                                        {
                                            needToInsert = false;
                                        }
                                        else if (selectCount == 0)
                                        {
                                            needToInsert = true;
                                        }
                                        else
                                        {
                                            tran.Rollback();
                                            throw new Exception("逻辑错误");
                                        }
                                    }
                                }
                                if (needToInsert)
                                {
                                    int speed = 30;
                                    if (allItems[i].Height > allItems[j].Height)
                                    {
                                        speed = 200;
                                    }
                                    else
                                    {
                                        speed = 30;
                                    }
                                    var sQL = $"INSERT INTO tunel(FPCodeFrom,HeightFrom,FPCodeTo,HeightTo,IsRegionTransfer,Speed,IsUsing) VALUES ('{allItems[i].FPCode}',{allItems[i].Height},'{allItems[j].FPCode}',{allItems[j].Height},0,{speed},1);";
                                    using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                    {
                                        if (command.ExecuteNonQuery() == 1)
                                        {
                                        }
                                        else
                                        {
                                            Console.WriteLine($"插入失败");
                                            tran.Rollback();
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    {
                        var sQL = @"DELETE A
FROM
	tunel A
	LEFT JOIN fpdetail B ON A.FPCodeFrom = B.FPCode 
	AND A.HeightFrom = B.Height 
	AND A.FPCodeTo = B.FPCode 
	AND A.HeightTo = B.Height 
WHERE
	A.FPCodeFrom = A.FPCodeTo AND (SELECT COUNT(*) FROM fpdetail C WHERE C.FPCode=A.FPCodeFrom  AND (C.Height-A.HeightFrom)*(C.Height-A.HeightTo)<0)  >0";
                        using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                        {
                            command.ExecuteNonQuery();

                            tran.Commit();

                        }
                    }
                }
            }
        }
    }
}
