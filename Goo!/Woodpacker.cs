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
        int eax, ebx;
        public Woodpacker(List<Cmd>cmdList)
        {
            this.cmdList = cmdList;
            stack = new Stack<int>();
            memory = new List<int>();
            eax = 0;
            ebx = 0;
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
                    eax ^= ebx ^= eax;
                    break;
                case 6://STO
                    memory.Insert(eax,stack.Pop());
                    break;
                case 7://GET
                    eax = memory[stack.Pop()];
                    break;
                case 8://OPR
                    Operator(curInst.args[0]);
                    break;
                case 9://JE
                    if(eax==0)
                    {
                        inst = stack.Pop();
                    }
                    break;
                case 10://JB
                    if (eax > 0)
                    {
                        inst = stack.Pop();
                    }
                    break;
                case 11://JL
                    if (eax < 0)
                    {
                        inst = stack.Pop();
                    }
                    break;
                case 12://JMP
                    inst = stack.Pop();
                    break;
                case 13:
                    break;
                case 90://BSOD
                    Console.WriteLine("NOPE! there 1s n0 c0mmand l1ke th4t!");
                    break;
                case 91:
                    //NOP
                    break;
            }
        }

        private void Operator(int v)
        {
            switch(v)
            {
                case 1://ADD
                    eax += ebx;
                    break;
                case 2://SUB
                    eax -= ebx;
                    break;
                case 3://MUL
                    eax *= ebx;
                    break;
                case 4://DIV
                    if (ebx == 0)
                        eax = int.MaxValue;
                    else
                        eax/=ebx;
                    break;
                case 5://OR
                    eax |= ebx;
                    break;
                case 6://AND
                    eax &= ebx;
                    break;
                case 7://XOR
                    eax ^= ebx;
                    break;
                case 8://NOT
                    eax = ~eax;
                    break;
                case 9://resetRegister
                    eax = ebx = 0;
                    break;
                case 10:
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
                case 3://먹엉
                    stack.Push(BitConverter.ToInt32(Encoding.ASCII.GetBytes(Console.ReadLine()),0));
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
