using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public Text displayer;
    public List<string> lines;

    private void Start()
    {
        lines = new List<string>();
    }

    public void Flush()
    {
        lines.Clear();
    }

    public void AddLine(string line)
    {
        lines.Add(line);
    }

    public void Update()
    {
        string content = "";
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            content += lines[i] + "\n";
        }
        displayer.text = content;
    }


    public void logPosition(GameObject obj) {
        if (obj == null)
        {
            this.AddLine("Error in debugPos : obj is null.");
        }

        try
        {
            Vector3 pos = obj.transform.position;
            string name = obj.name;
            this.AddLine(name + ".pos = " + pos);
        }
        catch (Exception e)
        {
            this.AddLine(e.ToString());
        }
    }
}
