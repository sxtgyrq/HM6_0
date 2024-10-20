﻿using MySql.Data.MySqlClient;
using System;

namespace DalOfAddress.HMSever
{
    public class MoneyRefererGet
    {
        const string tableName = "addressreferermoney";
        public static void GetSubsidizeAndLeft(string address, long value)
        {
            long subsidizeGet, subsidizeLeft;
            using (MySqlConnection con = new MySqlConnection(Connection.ConnectionStr))
            {
                con.Open();
                using (MySqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        bool hasValue;
                        long moneycount;
                        {
                            string sQL = $@"SELECT
                            	moneyaddress,
                            	moneycount 
                            FROM
                            	{tableName} WHERE moneyaddress=@moneyaddress";
                            // long moneycount;
                            using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                            {
                                command.Parameters.AddWithValue("@moneyaddress", address);

                                using (var reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {

                                        moneycount = Convert.ToInt64(reader["moneycount"]);

                                        hasValue = true;
                                    }
                                    else
                                    {
                                        moneycount = 0;
                                        hasValue = false;
                                    }
                                }
                            }
                        }


                        if (hasValue)
                        {
                            var minusValue = Math.Min(moneycount, value);
                            subsidizeGet = minusValue;
                            subsidizeLeft = moneycount - minusValue;
                            if (subsidizeLeft == 0)
                            {
                                string sQL = $@"DELETE FROM {tableName} WHERE moneyaddress=@moneyaddress";
                                // long moneycount;
                                using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                {
                                    command.Parameters.AddWithValue("@moneyaddress", address);
                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string sQL = $@"UPDATE {tableName} SET moneycount=@moneycount WHERE moneyaddress=@moneyaddress";
                                // long moneycount;
                                using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                                {
                                    command.Parameters.AddWithValue("@moneycount", moneycount - minusValue);
                                    command.Parameters.AddWithValue("@moneyaddress", address);
                                    command.ExecuteNonQuery();
                                }
                            }
                            tran.Commit();
                        }
                        else
                        {
                            subsidizeGet = 0;
                            subsidizeLeft = 0;
                            tran.Rollback();
                        }

                    }
                    catch (Exception e)
                    {
                        throw e;
                        throw new Exception("新增错误");
                    }
                }
            }
        }

        public static void GetSubsidizeAndLeft(MySqlConnection con, MySqlTransaction tran, string address, long value, out long subsidizeGet, out long subsidizeLeft)
        {
            bool hasValue;
            long moneycount;
            {
                string sQL = $@"SELECT
                            	moneyaddress,
                            	moneycount 
                            FROM
                            	{tableName} WHERE moneyaddress=@moneyaddress";
                // long moneycount;
                using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                {
                    command.Parameters.AddWithValue("@moneyaddress", address);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            moneycount = Convert.ToInt64(reader["moneycount"]);

                            hasValue = true;
                        }
                        else
                        {
                            moneycount = 0;
                            hasValue = false;
                        }
                    }
                }
            }
            if (hasValue)
            {
                var minusValue = Math.Min(moneycount, value);
                subsidizeGet = minusValue;
                subsidizeLeft = moneycount - minusValue;
                if (subsidizeLeft == 0)
                {
                    string sQL = $@"DELETE FROM {tableName} WHERE moneyaddress=@moneyaddress";
                    // long moneycount;
                    using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                    {
                        command.Parameters.AddWithValue("@moneyaddress", address);
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    string sQL = $@"UPDATE {tableName} SET moneycount=@moneycount WHERE moneyaddress=@moneyaddress";
                    // long moneycount;
                    using (MySqlCommand command = new MySqlCommand(sQL, con, tran))
                    {
                        command.Parameters.AddWithValue("@moneycount", moneycount - minusValue);
                        command.Parameters.AddWithValue("@moneyaddress", address);
                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                subsidizeGet = 0;
                subsidizeLeft = 0;
            }
        }
    }
}

