INCLUDE globals.ink

{pokemon_name == "" : ->main | ->already_chose}

== main ==
Which pokenmon do you choose?
    + [Charmander]
        -> chosen("Charmander")
    + [Bulbasaur]
        -> chosen("Bulbasaur")
    + [Squirtle]
        -> chosen("Squirtle")
        
        

=== chosen(pokemon) ===
~ pokemon_name = pokemon
You chose {pokemon}!
Take care of it!
-> END

=== already_chose ===
You <color=\#ff001f><b>already</b></color> chose {pokemon_name}!
-> END