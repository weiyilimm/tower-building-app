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
        using (StreamReader r = new StreamReader("Assets/buildings.json"))
        {   
            //read in the json
            json = r.ReadToEnd();
            Debug.Log(json);
            
            //reformat the json into dictionary style convention
            node = JSON.Parse(json);
            int colour = int.Parse(node["maths_and_physics"]["wall_colour"].Value);
            Debug.Log("maths and physics wall colour: "+ colour + getColours(colour));

            //modify the json node
            node["maths_and_physics"]["wall_colour"] = "1";
            colour = int.Parse(node["maths_and_physics"]["wall_colour"].Value);
            Debug.Log("maths and physics wall colour: " + colour + getColours(colour));

            //write new data
            //Debug.Log(Application.dataPath);

        }
        File.WriteAllText(Application.dataPath + "/test.json", node.ToString());
    }

    public string getColours(int i)
    {
        string[] colours = { "red", "green", "blue", "yellow" };
        return colours[i];
    }
}
