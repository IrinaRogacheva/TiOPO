using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVSet;

namespace TVSet
{
    class Program
    {
        static void Main(string[] args)
        {
            TVSet tv = new TVSet();

            tv.TurnOn();
            tv.SelectChannel(8);

            tv.SelectChannel(25);
            tv.SelectPreviousChannel();

            Console.WriteLine("channel: " + tv.GetChannel());
        }
    }
}
