using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonNPC : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        string pokemonName = ((Ink.Runtime.StringValue) DialogueManager
        .GetInstance().
        GetVariableState("pokemon_name")).value;

        switch (pokemonName)
        {
            case "":
                break;
            case "Charmander":
                spriteRenderer.color = Color.red;
                break;
            case "Bulbasaur":
                spriteRenderer.color = Color.green;
                break;
            case "Squirtle":
                spriteRenderer.color = Color.blue;
                break;
            default:
                Debug.LogWarning("Pokemon name not handled by switch statement:" + pokemonName);
                break;
        }
    }
}
