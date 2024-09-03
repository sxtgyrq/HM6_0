using DalOfAddress.DBModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalOfAddress
{
    public class tunel
    {
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
