using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Data;
using System.Data.SqlClient;


public class databaseSQL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CheckDB();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckDB() {
        string connectionString =
            "Server=localhost,1746;" +
            "Database=kinect;" +
            "User ID=root;" +
            "Password=;";
        IDbConnection dbcon;
        using (dbcon = new SqlConnection(connectionString)) {
            dbcon.Open();
            using (IDbCommand dbcmd = dbcon.CreateCommand()) {
                string sql =
                    "SELECT * FROM `body` WHERE 1";
                dbcmd.CommandText = sql;
                using (IDataReader reader = dbcmd.ExecuteReader()) {
                    while(reader.Read()) {
                        string FirstName = (string) reader["transform"];
                        string LastName = (string) reader["position"];
                        Console.WriteLine("Name: " +
                            FirstName + " " + LastName);
                        Debug.Log("Name: " +
                            FirstName + " " + LastName);
                    }
                }
            }
        }
    }
}
