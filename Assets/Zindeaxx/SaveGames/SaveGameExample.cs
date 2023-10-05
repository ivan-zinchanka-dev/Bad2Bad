using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zindeaxx.Saves;

public class SaveGameExample : MonoBehaviour
{
    void Start()
    {
        if (SaveGame.SaveExists)
            Debug.Log("Save found!");
        else
            Debug.Log("Save not found!");

        SaveGame.DestroySave();

        SaveGame.EncryptionEnabled = true;
        SaveGame.SetObjectValue("TestValue", 500);

        Debug.Log(SaveGame.GetIntValue("TestValue"));
        Debug.Log(SaveGame.GetStringValue("TestValue"));
        Debug.Log(SaveGame.GetFloatValue("TestValue"));


        SaveGame.SetObjectValue("StringVal", "Hi - 202 ");

        Debug.Log(SaveGame.GetIntValue("StringVal"));
        Debug.Log(SaveGame.GetStringValue("StringVal"));
        Debug.Log(SaveGame.GetFloatValue("StringVal"));
    }

}
