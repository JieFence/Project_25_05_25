using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunction
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("playEmote", (string emoteName) =>
        {
            Debug.Log("has receive the para :" + emoteName);
        });
    }

    public void UnBind(Story story)
    {
        story.UnbindExternalFunction("playEmote");
    }
}
