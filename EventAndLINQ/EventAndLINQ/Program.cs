using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAndLINQ
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //List of tasks representing lifetime of characters
            List<Task> lives = new List<Task>();
            //character list initialisation
            List<Character> characters = new List<Character>();
            //name list initialisation
            List<string> names = new List<string> { "Diego", "Adrien", "Simon", "Pierre", "Paul", "Jacques", "Michel", "Uriel", "Achille", "Tom", "Shérine", "Athena", "Jeanne", "Laura" };
            //characters creation
            foreach (string name in names)
            {
                characters.Add(new Character(name));
            }

            //government creation
            Government government = new Government(characters);

            foreach (Character character in characters)
            {
                //Life start and setting life in life list
                lives.Add(character.StartLife());

                //choosing enemies and friends
                character.ChooseEnemiesAndFriends(characters);

                //governement subscribe on character death event to produce death certificate
                character.IsDead += government.DeathCertificate;
            }

            //organisation of the first election
            government.Election();
            
            //wait for all lives to end
            await Task.WhenAll(lives);

            Console.WriteLine("Everybody is dead");
        }
    }
}
