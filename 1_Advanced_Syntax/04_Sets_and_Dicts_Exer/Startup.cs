namespace _04_Sets_and_Dicts_Exer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Startup
    {
        public static void Main()
        {
            //E1_UniqueUsername();
            //E2_SetsOfElements();
            //E3_PeriodicTable();
            //E4_CountSymbols();
            //E5_Phonebook();
            //E6_MinorTask();
            //E7_FixEmails();
            //E8_HandsOfCards();
            //E9_UserLogs();
            //E10_PopulationCounter();
            //E11_LogsAggregator();
            //E12_LegendaryFarming();
            //E13_SrabskoUnleashed();
            //E14_DragonArmy();
        }

        public static void E14_DragonArmy()
        {
            var dragonSInfoDict = new Dictionary<string, Dictionary<string, Tuple<long,long,long>>>();

            var defaultHealth = 250;
            var defaultDamage = 45;
            var defaultArmor = 10;

            var numberOfDragons = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfDragons; i++)
            {
                var dragonInfo = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var dragonType = dragonInfo[0];
                var dragonName = dragonInfo[1];
                long damage = dragonInfo[2] == "null" ? defaultDamage : long.Parse(dragonInfo[2]);
                long health = dragonInfo[3] == "null" ? defaultHealth : long.Parse(dragonInfo[3]);
                long armor = dragonInfo[4] == "null" ? defaultArmor : long.Parse(dragonInfo[4]);

                if (!dragonSInfoDict.ContainsKey(dragonType))
                {
                    dragonSInfoDict.Add(dragonType, new Dictionary<string, Tuple<long, long, long>>());
                }

                dragonSInfoDict[dragonType][dragonName] = Tuple.Create(damage, health, armor);
            }

            foreach (var kvp in dragonSInfoDict)
            {
                var dragonType = kvp.Key;
                var dragonsStats = kvp.Value.Select(kv => kv.Value).ToArray();
                var avgDragDmg = dragonsStats.Select(tuple => tuple.Item1).Average();
                var avgDragHealth = dragonsStats.Select(tuple => tuple.Item2).Average();
                var avgDragArmor = dragonsStats.Select(tuple => tuple.Item3).Average();

                Console.WriteLine($"{dragonType}::({avgDragDmg:F2}/{avgDragHealth:F2}/{avgDragArmor:F2})");

                foreach (var dKV in dragonSInfoDict[dragonType].OrderBy(kv => kv.Key))
                {
                    var dmg = dKV.Value.Item1;
                    var health = dKV.Value.Item2;
                    var armor = dKV.Value.Item3;

                    Console.WriteLine($"-{dKV.Key} -> damage: {dmg}, health: {health}, armor: {armor}");
                }
            }
        }

        public static void E13_SrabskoUnleashed()
        {
            var concertInfoDict = new Dictionary<string, Dictionary<string, long>>();

            Regex regex = new Regex(@"^([^ ]+ ){1,3}@([^ ]+ ){1,3}([0-9]+) ([0-9]+)$");

            var input = Console.ReadLine();
            while (input != "End")
            {
                Match m = regex.Match(input);

                if (m.Success)
                {
                    string singer = string.Empty;
                    foreach (Capture capture in m.Groups[1].Captures)
                    {
                        singer += capture.Value;
                    }

                    string location = string.Empty;
                    foreach (Capture capture in m.Groups[2].Captures)
                    {
                        location += capture.Value;
                    }

                    singer = singer.Trim();
                    location = location.Trim();

                    int ticketPrice = int.Parse(m.Groups[3].Value);
                    int ticketsCount = int.Parse(m.Groups[4].Value);

                    if (!concertInfoDict.ContainsKey(location))
                    {
                        concertInfoDict.Add(location, new Dictionary<string, long>());
                    }

                    if (!concertInfoDict[location].ContainsKey(singer))
                    {
                        concertInfoDict[location].Add(singer, 0);
                    }

                    concertInfoDict[location][singer] += (ticketPrice * ticketsCount);
                }

                input = Console.ReadLine();
            }

            foreach (var kvp in concertInfoDict)
            {
                string venue = kvp.Key;
                string[] singersInfo = kvp.Value
                                            .OrderByDescending(kv => kv.Value)
                                            .Select(kv => $"#  {kv.Key} -> {kv.Value}")
                                            .ToArray();

                Console.WriteLine(venue);
                Console.WriteLine($"{string.Join("\n", singersInfo)}");
            }
        }

        public static void E12_LegendaryFarming()
        {
            var itemMaterialsDict = new Dictionary<string, long>()
            {
                { "shards", 0 },
                { "fragments", 0 },
                { "motes", 0 }
            };

            var itemsDict = new Dictionary<string, string>()
            {
                { "shards", "Shadowmourne" },
                { "fragments", "Valanyr" },
                { "motes", "Dragonwrath" },
            };

            var junkMaterialsDict = new Dictionary<string, long>();

            string acquiredItem = "No item";
            bool isAcquiredItem = false;

            while (!isAcquiredItem)
            {
                var input = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                long quantity = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        quantity = long.Parse(input[i]);
                        continue;
                    }

                    var material = input[i].ToLower();

                    if (itemMaterialsDict.ContainsKey(material))
                    {
                        itemMaterialsDict[material] += quantity;

                        if (itemMaterialsDict[material] >= 250)
                        {
                            itemMaterialsDict[material] -= 250;
                            acquiredItem = itemsDict[material];
                            isAcquiredItem = true;
                            break;
                        }

                        continue;
                    }

                    if (!junkMaterialsDict.ContainsKey(material))
                    {
                        junkMaterialsDict.Add(material, 0);
                    }
                    junkMaterialsDict[material] += quantity;
                }
            }

            Console.WriteLine($"{acquiredItem} obtained!");

            foreach (var kvp in itemMaterialsDict.OrderByDescending(kv => kv.Value)
                                                    .ThenBy(kv => kv.Key))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            foreach (var kvp in junkMaterialsDict.OrderBy(kv => kv.Key))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }

        public static void E11_LogsAggregator()
        {
            var userDurationsDict = new Dictionary<string, long>();
            var usersIpsDict = new Dictionary<string, HashSet<string>>();

            var rows = int.Parse(Console.ReadLine());

            for (int i = 0; i < rows; i++)
            {
                var input = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var ip = input[0];
                var user = input[1];
                var duration = long.Parse(input[2]);

                if (!userDurationsDict.ContainsKey(user))
                {
                    userDurationsDict.Add(user, 0);
                    usersIpsDict.Add(user, new HashSet<string>());
                }

                userDurationsDict[user] += duration;
                usersIpsDict[user].Add(ip);
            }

            foreach (var kvp in userDurationsDict.OrderBy(kv => kv.Key))
            {
                var userIps = string.Join(", ", usersIpsDict[kvp.Key].OrderBy(ip => ip));

                Console.WriteLine($"{kvp.Key}: {kvp.Value} [{userIps}]");
            }
        }

        public static void E10_PopulationCounter()
        {
            var countryCityPopulation = new Dictionary<string, Dictionary<string, long>>();

            var input = Console.ReadLine().Split('|').ToArray();

            while (input[0] != "report")
            {
                var city = input[0];
                var country = input[1];
                var population = long.Parse(input[2]);

                if (!countryCityPopulation.ContainsKey(country))
                {
                    countryCityPopulation.Add(country, new Dictionary<string, long>());
                }

                if (!countryCityPopulation[country].ContainsKey(city))
                {
                    countryCityPopulation[country].Add(city, 0);
                }

                countryCityPopulation[country][city] += population;

                input = Console.ReadLine().Split('|').ToArray();
            }

            foreach (var kvp in countryCityPopulation
                .OrderByDescending(keyDict => keyDict.Value.Select(kv => kv.Value).Sum()))
            {
                var countryCitiesInfo = kvp.Value;
                long totalCountryPopulation = countryCitiesInfo.Select(city => city.Value).Sum();

                Console.WriteLine($"{kvp.Key} (total population: {totalCountryPopulation})");

                foreach (var city in countryCitiesInfo.OrderByDescending(kv => kv.Value))
                {
                    Console.WriteLine($"=>{city.Key}: {city.Value}");
                }
            }
        }

        public static void E9_UserLogs()
        {
            var userIps = new Dictionary<string, Dictionary<string, int>>();

            var input = Console.ReadLine().Split(' ').ToArray();

            while (input[0] != "end")
            {
                var ip = input[0].Split('=')[1];
                var user = input[2].Split('=')[1];

                if (!userIps.ContainsKey(user))
                {
                    userIps.Add(user, new Dictionary<string, int>());
                }

                if (!userIps[user].ContainsKey(ip))
                {
                    userIps[user].Add(ip, 0);
                }

                userIps[user][ip]++;

                input = Console.ReadLine().Split(' ').ToArray();
            }

            foreach (var kvp in userIps.OrderBy(kv => kv.Key))
            {
                var ips = kvp.Value.Select(kv => $"{kv.Key} => {kv.Value}").ToList();

                Console.WriteLine($"{kvp.Key}:");
                Console.WriteLine($"{string.Join(", ", ips)}.");
            }
        }

        public static void E8_HandsOfCards()
        {
            var input = Console.ReadLine().Split(':').ToArray();

            var cardPowers = new Dictionary<string, int>()
            {
                { "J", 11 },
                { "Q", 12 },
                { "K", 13 },
                { "A", 14 },
            };

            var cartTypes = new Dictionary<char, int>()
            {
                { 'C', 1 },
                { 'D', 2 },
                { 'H', 3 },
                { 'S', 4 },
            };

            var peopleCardsDict = new Dictionary<string, HashSet<string>>();

            while (input[0] != "JOKER")
            {
                string person = input[0];

                if (!peopleCardsDict.ContainsKey(person))
                {
                    peopleCardsDict.Add(person, new HashSet<string>());
                }

                input[1]
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .ForEach(card => peopleCardsDict[person].Add(card));

                input = Console.ReadLine().Split(':').ToArray();
            }

            var peoplePoints = new Dictionary<string, long>();

            foreach (var kvp in peopleCardsDict)
            {
                peoplePoints.Add(kvp.Key, 0);

                foreach (var card in kvp.Value)
                {
                    var strCardPower = card.Substring(0, card.Length - 1);

                    int cardPower;
                    var cardType = cartTypes[card.Last()];

                    if (cardPowers.ContainsKey(strCardPower))
                    {
                        cardPower = cardPowers[strCardPower];
                    }
                    else
                    {
                        cardPower = int.Parse(strCardPower);
                    }

                    peoplePoints[kvp.Key] += (cardPower * cardType);
                }
            }

            foreach (var kvp in peoplePoints)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }

        public static void E7_FixEmails()
        {
            var emailsDict = new Dictionary<string, string>();

            string command = Console.ReadLine();
            string username = null;

            for (int i = 0; command != "stop"; i++)
            {
                if (i % 2 == 0)
                {
                    username = command;

                    if (!emailsDict.ContainsKey(username))
                    {
                        emailsDict.Add(username, null);
                    }
                }
                else
                {
                    if (!command.ToLower().EndsWith("us") && !command.ToLower().EndsWith("uk"))
                    {
                        emailsDict[username] = command;
                    }
                    else
                    {
                        emailsDict.Remove(username);
                    }
                }

                command = Console.ReadLine();
            }

            foreach (var kvp in emailsDict)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }
        }

        public static void E6_MinorTask()
        {
            var resourcesDict = new Dictionary<string, long>();

            string inputCommand = Console.ReadLine();
            string resource = null;

            for (int i = 1; inputCommand != "stop"; i++)
            {
                if (i % 2 == 1)
                {
                    resource = inputCommand;

                    if (!resourcesDict.ContainsKey(inputCommand))
                    {
                        resourcesDict.Add(resource, 0);
                    }
                }
                else
                {
                    resourcesDict[resource] += long.Parse(inputCommand);
                }

                inputCommand = Console.ReadLine();
            }

            foreach (var kvp in resourcesDict)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }
        }

        public static void E5_Phonebook()
        {
            var phonebookDict = new Dictionary<string, string>();

            string[] command = Console.ReadLine().Split(new[] { '-' },
                StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            while (command[0] != "search")
            {
                phonebookDict[command[0]] = command[1];

                command = Console.ReadLine().Split(new[] { '-' },
                StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            }

            string username = Console.ReadLine();

            while (username != "stop")
            {
                if (phonebookDict.ContainsKey(username))
                {
                    Console.WriteLine($"{username} -> {phonebookDict[username]}");
                }
                else
                {
                    Console.WriteLine($"Contact {username} does not exist.");
                }

                username = Console.ReadLine();
            }
        }

        public static void E4_CountSymbols()
        {
            var inputString = Console.ReadLine();

            var sortedDict = new SortedDictionary<char, int>();

            for (int i = 0; i < inputString.Length; i++)
            {
                char symbol = inputString[i];

                if (!sortedDict.ContainsKey(symbol))
                {
                    sortedDict[symbol] = 0;
                }

                sortedDict[symbol]++;
            }

            foreach (var kvp in sortedDict)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value} time/s");
            }
        }

        public static void E3_PeriodicTable()
        {
            var num = int.Parse(Console.ReadLine());

            var sortedSetElem = new HashSet<string>();

            for (int i = 0; i < num; i++)
            {
                var elements = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                elements.ForEach(elem =>
                {
                    sortedSetElem.Add(elem);
                });
            }

            Console.WriteLine(string.Join(" ", sortedSetElem.OrderBy(elem => elem)));
        }

        public static void E2_SetsOfElements()
        {
            var setsLenght = Console.ReadLine().Split(new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var firstSet = new SortedSet<int>();
            var secondSet = new SortedSet<int>();

            var lenghtFirstSet = setsLenght[0];

            for (int i = 0; i < setsLenght.Sum(); i++)
            {
                var number = int.Parse(Console.ReadLine());

                if (i < lenghtFirstSet)
                {
                    firstSet.Add(number);
                }
                else
                {
                    secondSet.Add(number);
                }
            }

            firstSet.IntersectWith(secondSet);

            Console.WriteLine(string.Join(" ", firstSet));
        }

        public static void E1_UniqueUsername()
        {
            int num = int.Parse(Console.ReadLine());

            var setUsernames = new HashSet<string>();

            for (int i = 0; i < num; i++)
            {
                var username = Console.ReadLine();

                setUsernames.Add(username);
            }

            Console.WriteLine(string.Join("\n", setUsernames));
        }
    }
}
