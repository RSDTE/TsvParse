using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var path = @"C:\Users\RSDTE\Desktop\robot\tsvdemo.tsv";
            TsvParser p = new TsvParser(path);
            await p.Parse();
            Console.WriteLine("Hello World!");
        }

        //public  string SuiteToString(SuiteSection suite) {
        //    var res = new StringBuilder();
        //    var data = new string[8];

        //    data[0] = "*Settings*";
        //    res.Append(WriteRow(data));
        //    Array.Clear(data, 0, data.Length);

        //    // 写出 documentation 内容
        //    if (!string.IsNullOrWhiteSpace(suite.Documentation)) {
        //        data[0] = nameof(suite.Documentation);
        //        foreach (var item in suite.Documentation.Split(Environment.NewLine)) {
        //            if (string.IsNullOrWhiteSpace(data[0])) {
        //                data[0] = "...";
        //            }

        //            data[1] = item;
        //            res.Append(WriteRow(data));
        //            Array.Clear(data, 0, data.Length);
        //        }
        //    }

        //    Array.Clear(data, 0, data.Length);
        //    // 写出 suite setup 的内容
        //    if (suite.SuiteSetup.value != null && suite.SuiteSetup.value.Any()) {
        //        data[0] = "Suite Setup";
        //        var index = 1;
        //        foreach (var item in suite.SuiteSetup.value) {
        //            if (string.IsNullOrWhiteSpace(data[0])) {
        //                data[0] = "...";
        //            }
        //            data[index] = item;
        //            index += 1;
        //            if (index > 7) {
        //                res.Append(WriteRow(data));
        //                Array.Clear(data, 0, data.Length);
        //                index = 1;
        //            }
        //        }

        //        if (string.IsNullOrWhiteSpace(data[0])) {
        //            data[0] = "...";
        //        }

        //        if (!string.IsNullOrWhiteSpace(suite.SuiteSetup.comment)) {
        //            data[index] = suite.SuiteSetup.comment;
        //            index += 1;
        //        }
        //        if (index > 1) {
        //            res.Append(WriteRow(data));
        //            Array.Clear(data, 0, data.Length);
        //        }
        //    }

        //    if (suite.SuiteTeardown.value != null && suite.SuiteTeardown.value.Any()) {
        //        data[0] = "Suite Teardown";
        //        var index = 1;
        //        foreach (var item in suite.SuiteTeardown.value) {
        //            if (string.IsNullOrWhiteSpace(data[0])) {
        //                data[0] = "...";
        //            }
        //            data[index] = item;
        //            index += 1;
        //            if (index > 7) {
        //                res.Append(WriteRow(data));
        //                Array.Clear(data, 0, data.Length);
        //                index = 1;
        //            }
        //        }

        //        if (string.IsNullOrWhiteSpace(data[0])) {
        //            data[0] = "...";
        //        }

        //        if (!string.IsNullOrWhiteSpace(suite.SuiteTeardown.comment)) {
        //            data[index] = suite.SuiteTeardown.comment;
        //            index += 1;
        //        }
        //        if (index > 1) {
        //            res.Append(WriteRow(data));
        //            Array.Clear(data, 0, data.Length);
        //        }
        //    }

        //    if (suite.TestSetup.value != null && suite.TestSetup.value.Any()) {
        //        data[0] = "Test Setup";
        //        var index = 1;
        //        foreach (var item in suite.TestSetup.value) {
        //            if (string.IsNullOrWhiteSpace(data[0])) {
        //                data[0] = "...";
        //            }
        //            data[index] = item;
        //            index += 1;
        //            if (index > 7) {
        //                res.Append(WriteRow(data));
        //                Array.Clear(data, 0, data.Length);
        //                index = 1;
        //            }
        //        }

        //        if (string.IsNullOrWhiteSpace(data[0])) {
        //            data[0] = "...";
        //        }

        //        if (!string.IsNullOrWhiteSpace(suite.TestSetup.comment)) {
        //            data[index] = suite.TestSetup.comment;
        //            index += 1;
        //        }
        //        if (index > 1) {
        //            res.Append(WriteRow(data));
        //            Array.Clear(data, 0, data.Length);
        //        }
        //    }

        //    if (suite.TestTeardown.value != null && suite.TestTeardown.value.Any()) {
        //        data[0] = "Test Teardown";
        //        var index = 1;
        //        foreach (var item in suite.TestTeardown.value) {
        //            if (string.IsNullOrWhiteSpace(data[0])) {
        //                data[0] = "...";
        //            }
        //            data[index] = item;
        //            index += 1;
        //            if (index > 7) {
        //                res.Append(WriteRow(data));
        //                Array.Clear(data, 0, data.Length);
        //                index = 1;
        //            }
        //        }

        //        if (string.IsNullOrWhiteSpace(data[0])) {
        //            data[0] = "...";
        //        }

        //        if (!string.IsNullOrWhiteSpace(suite.TestTeardown.comment)) {
        //            data[index] = suite.TestTeardown.comment;
        //            index += 1;
        //        }
        //        if (index > 1) {
        //            res.Append(WriteRow(data));
        //            Array.Clear(data, 0, data.Length);
        //        }
        //    }

        //    if (!string.IsNullOrWhiteSpace(suite.TestTimeout.value)) {
        //        data[0] = "Test Timeout";
        //        data[1] = suite.TestTimeout.value;
        //        var index = 2;
        //        if (!string.IsNullOrWhiteSpace(suite.TestTimeout.message)) {
        //            data[index] = suite.TestTimeout.message;
        //            index += 1;
        //        }

        //        if (!string.IsNullOrWhiteSpace(suite.TestTimeout.comment)) {
        //            data[index] = suite.TestTimeout.comment;
        //        }
        //        res.Append(WriteRow(data));
        //        Array.Clear(data, 0, data.Length);
        //    }

        //    if (!string.IsNullOrWhiteSpace(suite.TestTemplate.value)) {
        //        data[0] = "Test Template";
        //        if (!string.IsNullOrWhiteSpace(suite.TestTemplate.comment)) {
        //            data[1] = suite.TestTemplate.comment;
        //        }
        //        res.Append(WriteRow(data));
        //        Array.Clear(data, 0, data.Length);
        //    }

        //    if (suite.ForceTags != null && suite.ForceTags.Any()) {
        //        data[0] = "Force Tags";
        //        var index = 1;
        //        foreach (var item in suite.ForceTags) {
        //            if (string.IsNullOrWhiteSpace(data[0])) {
        //                data[0] = "...";
        //            }
        //            data[index] = item;
        //            index += 1;
        //            if (index > 7) {
        //                res.Append(WriteRow(data));
        //                Array.Clear(data, 0, data.Length);
        //                index = 1;
        //            }
        //        }
        //        if (index > 1) {
        //            res.Append(WriteRow(data));
        //            Array.Clear(data, 0, data.Length);
        //        }
        //    }

        //    if (suite.DefaultTags != null && suite.DefaultTags.Any()) {
        //        data[0] = "Default Tags";
        //        var index = 1;
        //        foreach (var item in suite.DefaultTags) {
        //            if (string.IsNullOrWhiteSpace(data[0])) {
        //                data[0] = "...";
        //            }
        //            data[index] = item;
        //            index += 1;
        //            if (index > 7) {
        //                res.Append(WriteRow(data));
        //                Array.Clear(data, 0, data.Length);
        //                index = 1;
        //            }
        //        }
        //        if (index > 1) {
        //            res.Append(WriteRow(data));
        //            Array.Clear(data, 0, data.Length);
        //        }
        //    }

        //    if (suite.Metadatas != null && suite.Metadatas.Any()) {
        //        foreach (var metadata in suite.Metadatas) {
        //            if (string.IsNullOrWhiteSpace(metadata.name) || string.IsNullOrWhiteSpace(metadata.value)) {
        //                continue;
        //            }
        //            data[0] = "Metadata";
        //            data[1] = metadata.name;
        //            data[2] = metadata.value;
        //            if (!string.IsNullOrWhiteSpace(metadata.comment)) {
        //                data[3] = metadata.comment;
        //            }
        //            res.Append(WriteRow(data));
        //            Array.Clear(data, 0, data.Length);
        //        }
        //    }

        //    if (suite.Libraries != null && suite.Libraries.Any()) {
        //        foreach (var library in suite.Libraries) {
        //            if (string.IsNullOrWhiteSpace(library.path))
        //                continue;
        //            data[0] = "Library";
        //            data[1] = library.path;
        //            var index = 2;
        //            foreach (var arg in library.args) {
        //                if (string.IsNullOrWhiteSpace(data[0])) {
        //                    data[0] = "...";
        //                }
        //                data[index++] = arg;
        //                if (index > 7) {
        //                    res.Append(WriteRow(data));
        //                    Array.Clear(data, 0, data.Length);
        //                    index = 1;
        //                }
        //            }
        //            if (!string.IsNullOrWhiteSpace(library.alias)) {
        //                data[index++] = "WITH NAME";
        //                if (index > 7) {
        //                    res.Append(WriteRow(data));
        //                    Array.Clear(data, 0, data.Length);
        //                    index = 1;
        //                }
        //                if (string.IsNullOrWhiteSpace(data[0])) {
        //                    data[0] = "...";
        //                }
        //                data[index++] = library.alias;
        //                if (index > 7) {
        //                    res.Append(WriteRow(data));
        //                    Array.Clear(data, 0, data.Length);
        //                    index = 1;
        //                }
        //            }

        //            if (!string.IsNullOrWhiteSpace(library.comment)) {
        //                if (string.IsNullOrWhiteSpace(data[0])) {
        //                    data[0] = "...";
        //                }
        //                data[index] = library.comment;
        //            }
        //            res.Append(WriteRow(data));
        //            Array.Clear(data, 0, data.Length);
        //        }
        //    }


        //    if (suite.Resources != null && suite.Resources.Any()) {
        //        foreach (var resource in suite.Resources) {
        //            if (string.IsNullOrWhiteSpace(resource.path)) {
        //                continue;
        //            }
        //            data[0] = "Resource";
        //            data[1] = resource.path;
        //            if (!string.IsNullOrWhiteSpace(resource.comment)) {
        //                data[2] = resource.comment;
        //            }
        //            res.Append(WriteRow(data));
        //            Array.Clear(data, 0, data.Length);
        //        }
        //    }

        //    if (suite.Variables != null && suite.Variables.Any()) {
        //        foreach (var variable in suite.Variables) {
        //            if (string.IsNullOrWhiteSpace(variable.path))
        //                continue;
        //            data[0] = "Variables";
        //            data[1] = variable.path;
        //            var index = 2;
        //            foreach (var arg in variable.args) {
        //                if (string.IsNullOrWhiteSpace(data[0])) {
        //                    data[0] = "...";
        //                }
        //                data[index++] = arg;
        //                if (index > 7) {
        //                    res.Append(WriteRow(data));
        //                    Array.Clear(data, 0, data.Length);
        //                    index = 1;
        //                }
        //            }

        //            if (!string.IsNullOrWhiteSpace(variable.comment)) {
        //                if (string.IsNullOrWhiteSpace(data[0])) {
        //                    data[0] = "...";
        //                }
        //                data[index] = variable.comment;
        //            }
        //            res.Append(WriteRow(data));
        //            Array.Clear(data, 0, data.Length);
        //        }
        //    }



        //    if (suite.VariableSections != null && suite.VariableSections.Any()) {
        //        res.Append(WriteRow(data));
        //        data[0] = "*Variables*";
        //        res.Append(WriteRow(data));
        //        Array.Clear(data, 0, data.Length);
        //        foreach (var variable in suite.VariableSections) {
        //            data[0] = variable.Name;
        //            var index = 1;
        //            foreach (var item in variable.Value.Split("|")) {
        //                if (string.IsNullOrWhiteSpace(data[0])) {
        //                    data[0] = "...";
        //                }
        //                data[index++] = item;
        //                if (index > 7) {
        //                    res.Append(WriteRow(data));
        //                    Array.Clear(data, 0, data.Length);
        //                    index = 1;
        //                }
        //            }
        //            if (string.IsNullOrWhiteSpace(data[0])) {
        //                data[0] = "...";
        //            }
        //            if (!string.IsNullOrWhiteSpace(variable.Comment)) {
        //                data[index] = variable.Comment;
        //            }
        //            res.Append(WriteRow(data));
        //            Array.Clear(data, 0, data.Length);
        //        }
        //    }

        //    if (suite.TestCaseSections != null && suite.TestCaseSections.Any()) {
        //        res.Append(WriteRow(data));
        //        data[0] = "*Test Cases*";
        //        res.Append(WriteRow(data));
        //        Array.Clear(data, 0, data.Length);
        //        foreach (var item in suite.TestCaseSections) {
        //            res.Append(item);
        //        }
        //    }


        //    if (suite.KeywordSections != null && suite.KeywordSections.Any()) {
        //        res.Append(WriteRow(data));
        //        data[0] = "*Keywords*";
        //        res.Append(WriteRow(data));
        //        Array.Clear(data, 0, data.Length);
        //        foreach (var item in suite.KeywordSections) {
        //            res.Append(item);
        //        }
        //    }

        //    return res.ToString();
        //}

        //private string WriteRow(IList<string> data) {
        //    var strBuilder = new StringBuilder();
        //    foreach (var i in Enumerable.Range(0, 8)) {
        //        if (i < data.Count()) {
        //            strBuilder.Append(data[i]);
        //        } else {
        //            strBuilder.Append(string.Empty);
        //        }
        //        if (i < 7)
        //            strBuilder.Append("\t");
        //    }
        //    strBuilder.Append(Environment.NewLine);
        //    return strBuilder.ToString();
        //}
    }
}
