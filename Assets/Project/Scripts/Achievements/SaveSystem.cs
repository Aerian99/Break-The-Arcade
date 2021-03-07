using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SaveQuest()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";


        FileStream stream = new FileStream(path, FileMode.Create);

        QuestData data = new QuestData(GameObject.Find("Quest Saver").GetComponent<QuestSaver>().quest);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static QuestData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);


            QuestData data = formatter.Deserialize(stream) as QuestData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
