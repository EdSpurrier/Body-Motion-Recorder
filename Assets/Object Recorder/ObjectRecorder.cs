using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectRecorder : MonoBehaviour {

    public List<RecordingObject> recordingObjects;

    // Start is called before the first frame update
    void Start() {
        
        foreach(RecordingObject recObj in recordingObjects){
            recObj.Init();
        };

    }

    // Update is called once per frame
    void FixedUpdate() {
        foreach(RecordingObject recObj in recordingObjects){
            recObj.RecordFrame();
        };
    }
}
