using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace StockPriceNotice_forSlack
{
    class Program
    {
        static void Main()
        {
            // 株価を取得したい銘柄コードを入力し、Yahoo Financeを開く
            Console.WriteLine("4桁の銘柄コードを入力してください。");
            var stockNumber = Console.ReadLine();
            Console.WriteLine("銘柄コード" + stockNumber);
            var urlstring = $"http://stocks.finance.yahoo.co.jp/stocks/detail/?code={stockNumber}.T";

            // 指定した銘柄コードのページのHTMLをストリームで取得する
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            string sorce = client.DownloadString(urlstring);
            var parser = new HtmlParser();
            var doc = parser.Parse(sorce);

            // クエリーセレクタを指定し株価部分を取得する
            var stockName = doc.QuerySelector("#main th[class=symbol]");
            var priceNode = doc.QuerySelector("#main td[class=stoksPrice]");

            string Name = stockName.TextContent.Replace(",", string.Empty);

            // 取得した株価をStringからintにパースする
            int.TryParse(priceNode.TextContent, System.Globalization.NumberStyles.AllowThousands, null, out int stockPrice);

            Console.WriteLine("銘柄名：" + Name);
            Console.WriteLine("現在株価：{0}円", stockPrice);
            Console.Read();
        }
    }
}
