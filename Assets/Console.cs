using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Console : MonoBehaviour
{
    public List<string> lines = new List<string>();
    public int maximumLines = 15;
    Text console;
    TextGenerator generator;

    void Awake()
    {
        console = GetComponent<Text>();
        generator = new TextGenerator();
    }

    public void AddLine(string line)
    {
        lines.Add(line);
        if (lines.Count > maximumLines)
        {
            lines.RemoveAt(0);
        }

        UpdateText();
    }

    void UpdateText()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (string line in lines)
        {
            stringBuilder.AppendLine(line);
        }

        console.text = stringBuilder.ToString();
    }

    public IEnumerable<string> BreakMessageIntoLines(string message)
    {
        generator.Populate(message, console.GetGenerationSettings(console.rectTransform.sizeDelta));
        UILineInfo previousLine = new UILineInfo();
        bool first = true;
        foreach (UILineInfo line in generator.lines)
        {
            if (!first)
            {
                int length = line.startCharIdx - previousLine.startCharIdx;
                yield return message.Substring(previousLine.startCharIdx, length);
            }
            previousLine = line;
            first = false;
        }

        yield return message.Substring(previousLine.startCharIdx);
    }
}
