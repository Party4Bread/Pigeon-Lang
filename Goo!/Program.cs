using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Goo
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = File.ReadAllText("a.pigeon");
            List<Cmd> codeline = new List<Cmd>();

            int codeint = new int();
            Cmd opCode = new Cmd();
            opCode.args = new List<int>();
            for (int i=0;i<code.Length;i++)
            {
                switch (code[i])
                {
                    case '구':
                        codeint++;
                        break;
                    case '!':
                        if (opCode.opCode == 0)
                        {
                            opCode.opCode = codeint;
                            codeint = 0;
                        }
                        else
                        {
                            opCode.args.Add(codeint);
                            codeint = 0;
                        }
                        break;
                    case '?':
                        opCode.args.Add(codeint);
                        codeline.Add(opCode);

                        opCode = new Cmd();
                        opCode.args = new List<int>();
                        codeint = 0;
                        break;
                    default:
                        Console.WriteLine("Error 0: char"+i+"which is"+code[i]+"is not pigeon's sound!");
                        break;
                }
            }
            Woodpacker wood = new Woodpacker(codeline);
            wood.Run();            
            Console.ReadKey();
        }
    }
}
