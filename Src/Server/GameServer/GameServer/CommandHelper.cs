using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class CommandHelper
    {
        public static void Run()
        {
            bool run = true;
            while (run)
            {
                Console.Write(">");
                string line = Console.ReadLine().ToLower().Trim();
                try
                {
                    if(line == "")
                    {
                        Help();
                    }
                    else
                    {
                        string[] cmd = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        switch (cmd[0])
                        {
                            case "addexp":
                                AddExp(int.Parse(cmd[1]), int.Parse(cmd[2]));
                                break;
                            case "exit":
                                run = false;
                                break;
                            default:
                                Help();
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                
            }
        }

        private static void AddExp(int characterId, int exp)
        {
            var cha = Managers.CharacterManager.Instance.GetCharacter(characterId);
            if(cha == null)
            {
                Console.WriteLine("characterId:{0} not found.", characterId);
                return;
            }
            cha.AddExp(exp);
        }

        public static void Help()
        {
            Console.Write(@"
Help:
    addexp <characterId> <exp>          Add exp for character
    exit                                Exit Game Server
    help                                Show Help
");
        }
    }
}
