using System.Collections;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SavePlayer(StatsManager stats)
    {
        Debug.Log("OK1");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        Debug.Log("OK2");
        PlayerData playerData = new PlayerData(stats);
        formatter.Serialize(stream, playerData);
        stream.Close();
        Debug.Log("SUCCESSFULL");
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return playerData;
        } 
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
