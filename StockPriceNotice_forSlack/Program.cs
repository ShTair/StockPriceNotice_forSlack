using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StockPriceNotice_forSlack
{
    class Program
    {
        static void Main(string[] args)
        {
            // 株価を取得したい銘柄コードを入力し、Yahoo Financeを開く
            Console.WriteLine("4桁の銘柄コードを入力してください。");
            string stockNumber = Console.ReadLine();
            Console.WriteLine("銘柄コード" + stockNumber);
            var urlstring = string.Format("http://stocks.finance.yahoo.co.jp/stocks/detail/?code={0}.T", stockNumber);

            // 指定した銘柄コードのページのHTMLをストリームで取得する
            var doc = default(IHtmlDocument);
            using (var client = new HttpClient()) 
            using (var stream = client.GetStreamAsync(new Uri(urlstring)))
            {
                var parser = new HtmlParser();
                doc = parser.ParseAsync(stream);
            }

            // クエリーセレクタを指定し株価部分を取得する
            var stockName = doc.QuerySelector("main th[class=symbol");
            var priceNode = doc.QuerySelector("#main td[class=stoksPrice]");
            
            // 取得した株価をStringからintにパースする
            var stockPrice = int.Parse(priceNode.InnerText);
            Console.WriteLine("銘柄名：" + stockName);
            Console.WriteLine("現在株価：{0}円", stockPrice);
            Console.Read();
        }
    }
}
