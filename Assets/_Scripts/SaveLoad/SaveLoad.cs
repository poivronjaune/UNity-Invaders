using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad
{
    private static string fileName = "SaveData.txt";
    private static string dirName = "SavaData";

    public static void SaveState(SaveObject so)
    {
        if (!DirectoryExists() )
            Directory.CreateDirectory(Application.persistentDataPath + "/" + dirName);
            

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetSavePath());
        bf.Serialize(file, so);
        file.Close();
    }

    public static SaveObject LoadState()
    {
        SaveObject so = new SaveObject();;

        if (SaveExists())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(GetSavePath(), FileMode.Open);
                so = (SaveObject)bf.Deserialize(file);
                file.Close();
            }
            catch (SerializationException)
            {
                // TODO: fix this for real world
                Debug.Log("Failed to load saved game!");
            }
        }
        else
        {
            SaveState(so);
        }

        return so;
    }

    private static bool SaveExists()
    {
        return File.Exists(GetSavePath());
    }

    private static bool DirectoryExists()
    {
        return Directory.Exists(Application.persistentDataPath + "/" + dirName);
    }

    private static string GetSavePath()
    {
        return Application.persistentDataPath + "/" + dirName + "/" + fileName;
    }

}
