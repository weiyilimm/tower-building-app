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
        using (StreamReader r = new StreamReader("Assets/JSON/buildings.json"))
        {
            //read in the json
            json = r.ReadToEnd();
            Debug.Log(json);

            //reformat the json into dictionary style convention
            node = JSON.Parse(json);
        }

        //building and part to modify
        string building = "maths_and_physics";
        string section = "wall_colour";

        //get part to be modified
        int colour = int.Parse(node[building][section].Value);
        Debug.Log("maths and physics wall colour: " + colour + getColours(building, section, colour));

        //modify the json node
        node[building][section] = 1;

        //write new data
        File.WriteAllText(Application.dataPath + "/JSON/buildings.json", node.ToString());

        //get modified part
        colour = int.Parse(node[building][section].Value);
         
        Debug.Log("maths and physics wall colour: " + colour + getColours(building, section, colour));
    }

    public string getColours(string b, string s, int i) //building, part of the building and colour index
    {
        JSONNode node;
        string colour = "#000000";
        using (StreamReader r = new StreamReader("Assets/JSON/colours.json"))
        {
            //read in the json
            json = r.ReadToEnd();
            //reformat the json into dictionary style convention
            node = JSON.Parse(json);
            string[] colours = node[b];
            colour = colours[i];
        }
        return colour;
    }
}
