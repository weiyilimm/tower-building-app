using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

[System.Serializable]
public class jsontest : MonoBehaviour
{
    private string json;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("loading json...");
        LoadJson();
    }

    // Update is called once per frame
    /*
    void Update()
    {

    }*/

    private void LoadJson()
    {
        JSONNode node;
        using (StreamReader r = new StreamReader("Assets/test.json"))
        {   
            //read in the json
            json = r.ReadToEnd();
            Debug.Log(json);
            
            //reformat the json into dictionary style convention
            node = JSON.Parse(json);
            string colour = node["colour1"]["C"].Value;
            Debug.Log("Colour 1 C: "+ colour);

            //modify the json node
            node["colour1"]["C"] = "light green";
            Debug.Log("Modified colour: "+ node["colour1"]["C"].Value);

            //write new data
            //Debug.Log(Application.dataPath);

        }
        File.WriteAllText(Application.dataPath + "/test.json", node.ToString());
    }

    /*
    public void Load(string savedData)
    {
        JsonUtility.FromJsonOverwrite(savedData, this);
    }
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
    public static SaveCharacters CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SaveCharacters>(jsonString);
    }

    //JSONNode node = JSON.Parse(jsonString);
    //string score = node["Player"]["Score"].Value;
    */
}
