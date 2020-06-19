using System;
using System.Text;
using shb_bank.Controller;
using shb_bank.View;

namespace shb_bank
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var generateMenu = new GenerateMenu();
            generateMenu.GetMenu();
        }
    }
}