using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPriceNotice_forSlack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("4桁の銘柄コードを入力してください。");
            string stockNumber = Console.ReadLine();
            Console.WriteLine("銘柄コード" + stockNumber);
        }
    }
}
