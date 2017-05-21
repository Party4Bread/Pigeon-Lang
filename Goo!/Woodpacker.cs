using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Goo
{
    public class Woodpacker
    {
        List<Cmd> cmdList;
        int inst = 0;
        Stack<int> stack;
        List<int> memory;
        int eax, ecx;
        public Woodpacker(List<Cmd>cmdList)
        {
            this.cmdList = cmdList;
            stack = new Stack<int>();
            memory = new List<int>();
            eax = 0;
            ecx = 0;
        }

        public void Step()
        {
            Cmd curInst = cmdList[inst++];
            
            switch(curInst.opCode)
            {
                case 1://IVK
                    Invoke(curInst.args[0]);
                    break;
                case 2://PUSH
                    stack.Push(curInst.args[0]);
                    break;
                case 3://POP
                    eax=stack.Pop();
                    break;
                case 4://Rpush
                    stack.Push(eax);
                    break;
                case 5://XCHG(SWP)
                    eax ^= ecx ^= eax;
                    break;
                case 6://STO
                    memory.Insert(ecx,stack.Pop());
                    break;
                case 90://BSOD
                    Console.WriteLine("NOPE! there 1s n0 c0mmand l1ke th4t!");
                    break;
                case 91:
                    //NOP
                    break;
            }
        }

        public void Run()
        {
            foreach(var d in cmdList)
            {
                Step();
            }
        }
       

        private void Invoke(int arg)
        {
            switch(arg)
            {
                case 1: //죽으렴
                    Environment.Exit(0);
                    break;
                case 2: //뱉으렴
                    Console.Write((char)stack.Pop());
                    break;
                case 100: //삡!
                    Console.Beep();
                    break;
            }
        }
    }
    public struct Cmd
    {
        public int opCode;
        public List<int> args;
    }
}
