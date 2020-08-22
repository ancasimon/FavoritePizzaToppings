using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FavoritePizzaToppings
{
    class Program
    {
        static void Main(string[] args)
        {
            //get the data from the json file and deserialize it so we can read it in C#:
            var json = File.ReadAllText("pizzas.json");
            var pizzas = JsonSerializer.Deserialize<List<Pizza>>(json);

            //declare a new list of strings that is going to hold just the strings of toppings:
            var pizzaToppingCombinations = new Dictionary<string, int>();


            //create new strings for each set of toppings, order them by themselves/alphabetize them , and then - if that particular string is not already in our new list, add it there!; if it is there, increment the count by 1:
            foreach (var pizza in pizzas)
            {
                //Console.WriteLine(string.Join(",", pizza.toppings));

                var toppingCombination = string.Join(",", pizza.toppings.OrderBy(item => item));
                //Console.WriteLine(string.Join(",", toppingCombination));

                if (!pizzaToppingCombinations.ContainsKey(toppingCombination))
                {
                    pizzaToppingCombinations.Add(toppingCombination, 1);
                }
                pizzaToppingCombinations[toppingCombination]++; //NEED a better understanding of this syntax - why does the ++ automatically refer to the int value in the dictionary???

            }

            //just to test the new list of combinations in the dictionary:
            //foreach (var item in pizzaToppingCombinations)
            //{
            //    Console.WriteLine($"Item in combinations list: {item.Key} - {item.Value}");
            //}

            //NEXT: sort the combinations by their count and turn it into a list so that we can then get the top 20 items:
            var top20Combinations = pizzaToppingCombinations.OrderByDescending(combo => combo.Value).ToList().GetRange(0, 20);

            //Finally, loop over the top 20 and display the combo type and count:
            var sequence = 0;
            foreach(var (comboToppings, comboCount) in top20Combinations)
            {
                sequence++;
                Console.WriteLine($"Number {sequence} in people's pizza toppings choice award goes to: {comboToppings.ToUpper()} - which was selected {comboCount} times!");
            }

            //var twoToppingPizzas = pizzas.Where(p => p.toppings.Count == 2);
            //Console.WriteLine(twoToppingPizzas.Count());
        }
    }
}
