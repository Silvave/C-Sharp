using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottsSystem
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new GringottsContext();

            //DepositsSumForOllivanderFamily(context);

            DepositsFilter(context);
        }

        private static void DepositsFilter(GringottsContext context)
        {
            var depositGroups = context.WizzardDeposits
                .Where(x => x.MagicWandCreator == "Ollivander family")
                .GroupBy(x => x.DepositGroup, x => x.DepositAmount,
                    (key, g) => new
                    {
                        DepositGroupName = key,
                        DepositsAmounts = g.ToList().Sum()
                    })
                .Where(x => x.DepositsAmounts < 150000)
                .OrderByDescending(x => x.DepositsAmounts);

            foreach (var depositGroup in depositGroups)
            {
                Console.WriteLine($"{depositGroup.DepositGroupName} - {depositGroup.DepositsAmounts}");
            }
        }

        private static void DepositsSumForOllivanderFamily(GringottsContext context)
        {
            var depositGroups = context.WizzardDeposits
                .Where(x => x.MagicWandCreator == "Ollivander family")
                .GroupBy(x => x.DepositGroup, x => x.DepositAmount,
                    (key, g) => new
                    {
                        DepositGroupName = key,
                        DepositsAmounts = g.ToList()
                    });

            foreach (var depositGroup in depositGroups)
            {
                Console.WriteLine($"{depositGroup.DepositGroupName} - {depositGroup.DepositsAmounts.Sum()}");
            }
        }
    }
}
