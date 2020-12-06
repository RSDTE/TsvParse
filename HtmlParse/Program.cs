using HtmlAgilityPack;
using System;
using System.Text;

namespace HtmlParse
{
    class Program
    {
        static void Main(string[] args) {
            var path = @"C:\Users\RSDTE\Desktop\robot\HtmlTest.html";
            var p = new HtmlParse.HtmlParser(path);
            p.Parse();
            Console.WriteLine("Hello World!");
        }





    }
}
