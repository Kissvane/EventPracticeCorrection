using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAndLINQ
{
    class Government
    {
        //register of the living people
        public List<Character> livingPeople;

        //the current president
        //easily accessible as a static variable
        public static Character president;

        //constructor
        public Government(List<Character> characters)
        {
            livingPeople = new List<Character>();
            livingPeople.AddRange(characters);
        }

        //when somebody die the governement produce a death certifcate
        public void DeathCertificate(Object sender, DeathEventArgs args)
        {
            livingPeople.Remove(args.Body);
            //if the dead is the president
            if (args.Body == president)
            {
                //announce his death, death date and last words
                Console.WriteLine("Governemental announcement : {0} the president is Dead the {1}.\nHis last words were \"{2}\"", president.Name, args.DeathNote.ToLongDateString(), args.LastWords);
                //elect a new president if there is more than 2 leaving people
                if (livingPeople.Count > 2)
                    Election();
            }
            else
            {
                //announce his death
                Console.WriteLine("Governemental announcement : {0} is dead.", args.Body.Name);
            }
        }

        //organize an election
        public void Election()
        {
            List<Character> population = livingPeople;
            //sorting people by onumber of friends and enemies
            population.OrderBy(x => x.friends.Count).ThenByDescending(x => x.enemies.Count);
            //select the first as president
            president = population[0];
            //announce the new president
            Console.WriteLine("{0} is the new president.", president.Name);
            //unsuscribe everybody on president death (population and governement)
            president.BreakLinks();
            //remove president from the current list
            population.Remove(president);

            //make the governement aware of the president death again
            president.IsDead += DeathCertificate;

            foreach (Character character in population)
            {
                //make everybody in the list honour the president when he dies
                president.IsDead += character.Honour;
            }
        }

    }
}
