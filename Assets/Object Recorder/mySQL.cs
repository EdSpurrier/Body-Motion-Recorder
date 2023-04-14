/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;
 
public class mySQL : MonoBehaviour {


    private MySqlConnection connection = null;

    public MySqlConnection Connection {
        get { return connection; }
    }


    void Start() {

        bool checkDB = IsConnect();
        Debug.Log( checkDB.ToString() );

       
        try {
            bool checkDB = IsConnect();
        } catch (MySqlException ex) {
          Debug.Log("Error: {0}" +  ex.ToString());
        }
    }

    public void Close() {
        connection.Close();
    }


    public void UpdateDatabase() {

    }

        
    public bool IsConnect() {
        if (Connection == null) {

            string connstring = string.Format("Server=localhost; database={0}; UID=root; password=", "kinect");
            //string connstring = string.Format("Server=edspurrier.com; database={0}; UID=edspurri_loggy_u; password=idsidsidsids", "edspurri_loggy");
            

            //string connstring = string.Format("Server=101.187.190.242; database={0}; UID=root; password=idsidsidsids", "loggy");
            connection = new MySqlConnection(connstring);
            connection.Open();

            ReadDB();
        };

        return true;
    }


    public void ReadDB() {
        string query = "SELECT * FROM body";
        //Create Command
        MySqlCommand cmd = new MySqlCommand(query, connection);
        //Create a data reader and Execute the command
        MySqlDataReader dataReader = cmd.ExecuteReader();

        //Create a list to store the result
        List< string >[] list = new List< string >[3];
        list[0] = new List< string >();
        list[1] = new List< string >();
        list[2] = new List< string >();

        //Read the data and store them in the list
        while (dataReader.Read())
        {
            list[0].Add(dataReader["transform"].ToString() );
            list[1].Add(dataReader["position"].ToString() );
            list[2].Add(dataReader["rotation"].ToString() );
        }

        //close Data Reader
        dataReader.Close();

        foreach(List<string> column in list) {
            foreach(string item in column) {
                Debug.Log(item);
            };
        };


        Close();
        
    }





    //Insert statement
    public void Insert() {
        string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

        //open connection
        if (this.OpenConnection() == true)
        {
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, connection);
            
            //Execute command
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    //Update statement
    public void Update() {
        string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

        //Open connection
        if (this.OpenConnection() == true)
        {
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = connection;

            //Execute query
            cmd.ExecuteNonQuery();

            //close connection
            this.CloseConnection();
        }
    }

    //Delete statement
    public void Delete() {
        string query = "DELETE FROM tableinfo WHERE name='John Smith'";

        if (this.OpenConnection() == true)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            this.CloseConnection();
        }
    }
}
 */