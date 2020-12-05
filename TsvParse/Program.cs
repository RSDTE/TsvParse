using System;
using System.Threading.Tasks;

namespace TsvParse
{
    /// <summary>
    /// robot framework tsv 的格式文件解析完成. 谢谢大家, 明天见!
    /// </summary>
    class Program
    {
        [STAThread]
        static async Task Main(string[] args) {
            var path = @"C:\Users\RSDTE\Desktop\robot\TsvTest.tsv";
            path = "./test.tsv";
            TsvParser p = new TsvParser(path);
            await p.Parse();
            Console.WriteLine("Hello World!");
        }
    }
}
