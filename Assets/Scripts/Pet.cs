using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////
//Assignment/Lab/Project: Virtual Pet Game
//Name: Logan Hickman
//Section: 2021SP.SGD.213.2101
//Instructor: Aurore Wold
//Date: 02/22/2022
/////////////////////////////////////////////
public class Pet
{
    /// <summary>
    /// Increased by Eat()
    /// </summary>
    private int hunger = 75;
    /// <summary>
    /// Increased by Play()
    /// </summary>
    private int happiness = 75;
    /// <summary>
    /// Increase by Rest()
    /// </summary>
    private int energy = 75;

    /// <summary>
    /// Name of this pet
    /// </summary>
    public string PetName { get; }
    /// <summary>
    /// How hungry is the pet?
    /// </summary>
    /// <seealso cref="Eat"/>
    /// <seealso cref="Play"/>
    /// <seealso cref="Rest"/>
    public int Hunger
    {
        get
        {
            return hunger;
        }
    }
    /// <summary>
    /// How hungry is this pet?
    /// </summary>
    /// <seealso cref="Eat"/>
    /// <seealso cref="Play"/>
    /// <seealso cref="Rest"/>
    public int Happiness
    {
        get
        {
            return happiness;
        }
    }
    /// <summary>
    /// How playful is the pet feeling?
    /// </summary>
    /// <seealso cref="Eat"/>
    /// <seealso cref="Play"/>
    /// <seealso cref="Rest"/>
    public int Energy
    {
        get
        {
            return energy;
        }
    }

    /// <summary>
    /// Pet is created with 75 Hunger, Happiness, and Energy
    /// </summary>
    /// <param name="name">What to name the pet</param>
    public Pet(string name)
    {
        PetName = name;
    }

    /// <summary>
    /// Increases hunger by 30 and decreases happiness and energy by 10
    /// </summary>
    /// <seealso cref="Eat"/>
    /// <seealso cref="Play"/>
    /// <seealso cref="Rest"/>
    public void Eat()
    {
        hunger += 30;
        happiness -= 10;
        energy -= 10;
    }

    /// <summary>
    /// Increases happiness by 30 and decreases hunger and energy by 10
    /// </summary>
    /// <seealso cref="Eat"/>
    /// <seealso cref="Play"/>
    /// <seealso cref="Rest"/>
    public void Play()
    {
        hunger -= 10;
        happiness += 30;
        energy -= 10;
    }
    /// <summary>
    /// Increases energy by 30 and decreases hunger and happiness by 10
    /// </summary>
    /// <seealso cref="Eat"/>
    /// <seealso cref="Play"/>
    /// <seealso cref="Rest"/>
    public void Rest()
    {
        hunger -= 10;
        happiness -= 10;
        energy += 30;
    }

    /// <summary>
    /// Randomly decreases each stat from 0-9
    /// </summary>
    public void DecreaseStatsRandomly()
    {
        hunger -= Random.Range(0, 10);
        happiness -= Random.Range(0, 10);
        energy -= Random.Range(0, 10);
    }
}
