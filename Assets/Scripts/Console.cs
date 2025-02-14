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
        //this.gameObject.SetActive(false);
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

    public void setVisible(bool visible)
    {
        if (visible)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
