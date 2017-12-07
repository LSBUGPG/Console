using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    public string[] messages;
    public Console console;

    void Start()
    {
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        while (true)
        {
            foreach (string message in messages)
            {
                foreach (string line in console.BreakMessageIntoLines(message))
                {
                    console.AddLine(line);
                    yield return new WaitForSeconds(0.1f);
                }

                console.AddLine("");

                yield return new WaitForSeconds(2);
            }

            yield return null;
        }
    }

}
