using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.Data;
 
public class Database : MonoBehaviour {


    private MySqlConnection connection = null;

    public MySqlConnection Connection {
        get { return connection; }
    }

    void Start() {
        IsConnect();
    }

    bool Connect() {
        try {
            Debug.Log( "Connecting To Database" );
            return IsConnect();
        } catch (MySqlException ex) {
            Debug.Log("Error: {0}" +  ex.ToString());
            return false;
        };
    }


    public void Close() {
        connection.Close();
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


    public void AddFramesToDatabase(List<RecordedFrame> frames) {
        if ( IsConnect() ) {
            
            foreach(RecordedFrame frame in frames) {

                foreach(RecordedTransform recTrans in frame.recordedTransform) {
                    string position = recTrans.position.x + "," + recTrans.position.y + "," + recTrans.position.z;
                    string rotation = recTrans.rotation.x + "," + recTrans.rotation.y + "," + recTrans.rotation.z + "," + recTrans.rotation.w;
                   
                    Insert(recTrans.transform.gameObject.name, position, rotation);
                };

            };
            
            //close connection
            this.Close();
        };
    }


    //Insert statement
    public void Insert(string transform, string position, string rotation) {
        string query = "INSERT INTO body (transform, position, rotation) VALUES('" + transform + "', '" + position + "', '" + rotation + "')";

        Debug.Log(query);

        if (Connection != null) {
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, connection);
            
            //Execute command
            cmd.ExecuteNonQuery();

        };
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


        //Close();
        
    }

}
