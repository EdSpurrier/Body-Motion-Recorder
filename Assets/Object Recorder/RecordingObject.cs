using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;


[System.Serializable]
public class RecordedTransform : System.Object
{
    public Transform transform;
    public Quaternion rotation;
    public Vector3 position;
}


[System.Serializable]
public class RecordedFrame : System.Object
{
    public List<RecordedTransform> recordedTransform;
}

[System.Serializable]
public class RecordingObject : System.Object
{
    public string name { get; set; }
    public int frameRate = 30;
    public GameObject parentObject;
    public bool getAllChildren = true;
    public List<Transform> childTransforms;
    public int frameSaveRate = 50;
    public int currentFrame = 0;
    public List<RecordedFrame> frames;
    public Database database;
    
    // Start is called before the first frame update
    public void Init()
    {  
        Transform[] collected = parentObject.GetComponentsInChildren<Transform>();
        childTransforms = collected.ToList<Transform>();
    }

    public void RecordFrame() {
        if (currentFrame >= frameSaveRate) {
            return;
        };


        RecordedFrame frame = new RecordedFrame(); 

        frame.recordedTransform = new List<RecordedTransform>();

        foreach(Transform trans in childTransforms) {
            RecordedTransform recTrans = new RecordedTransform();
            
            recTrans.transform = trans.transform;
            recTrans.position = trans.position;
            recTrans.rotation = trans.rotation;
            
            frame.recordedTransform.Add(recTrans);
        };

        frames.Add(frame);

        currentFrame++;

        if (currentFrame == frameSaveRate) {
            //currentFrame = 0;
            SaveToFile();
        };
    }

    public void SaveToFile() {
        Debug.Log("Saving To File");
        //string json = JsonUtility.ToJson(frames);
        Debug.Log(frames.Count);

        database.AddFramesToDatabase(frames);

        //frames = new List<RecordedFrame>(); 
    }


}
