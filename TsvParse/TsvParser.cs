using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using System.Data;

/// <summary>
/// 1. robot framework 的 TSV 文件解析
/// 2. 写入
/// </summary>

namespace TsvParse
{
    public class TsvParser
    {
        private readonly string tsvFilePath;
        private readonly string separator;
        private readonly List<string> tokens;

        public TsvParser(string tsvFilePath) {
            this.tsvFilePath = tsvFilePath;
            separator = "\t";
            tokens = new List<string>(){
                "*Settings*",
                "*Variables*",
                "*Test Cases*",
                "*Keywords*"
            };
        }

        public async Task Parse() {
            using StringReader sr = new StringReader(await File.ReadAllTextAsync(this.tsvFilePath));
            var line = await sr.ReadLineAsync();
            var token = string.Empty;
            var subToken = string.Empty;
            TestCaseSection testcase = null;
            KeywordSection keywordSection = null;
            var suite = new SuiteSection();
            while (line != null && line.Length > 0) {
                Console.WriteLine(line);
                var arr = line.Split(this.separator);
                if (!tokens.Contains(arr[0])) {
                    switch (token) {
                        case "*Settings*": {
                                if (arr[0] != "...") {
                                    subToken = arr[0];
                                }
                                SuiteParse(suite, subToken, arr.Skip(1).Take(arr.Length - 1));
                            }
                            break;
                        case "*Variables*":
                            if (arr[0] != "...") {
                                subToken = arr[0];
                            }
                            VariableParse(suite, subToken, arr.Skip(1).Take(arr.Length - 1));
                            break;
                        case "*Test Cases*":
                            if (!string.IsNullOrWhiteSpace(arr[0])) {
                                if (suite.TestCaseSections is null) {
                                    suite.TestCaseSections = new List<TestCaseSection>();
                                }
                                if (testcase != null) {
                                    suite.TestCaseSections.Add(testcase);
                                    testcase = null;
                                }

                                if(testcase is null) {
                                    testcase = new TestCaseSection { Name = arr[0] };
                                }
                            }
                            if(arr[1] != "...") {
                                subToken = arr[1];
                            }
                            TestCaseParse(testCaseSection: testcase, subToken, arr.Skip(1).Take(arr.Length - 1));
                            break;
                        case "*Keywords*":
                            // to do...
                            if (!string.IsNullOrWhiteSpace(arr[0])) {
                                if (suite.KeywordSections is null) {
                                    suite.KeywordSections = new List<KeywordSection>();
                                }

                                if (keywordSection != null) {
                                    suite.KeywordSections.Add(keywordSection);
                                    keywordSection = null;
                                }

                                if (keywordSection is null) {
                                    keywordSection = new KeywordSection { Name = arr[0] };
                                }
                            }
                            if (arr[1] != "...") {
                                subToken = arr[1];
                            }
                            UserKeyParse(keywordSection, subToken, arr.Skip(1).Take(arr.Length - 1));
                            break;
                        default:
                            break;
                    }
                }else {
                    if(testcase != null) {
                        suite.TestCaseSections.Add(testcase);
                        testcase = null;
                    }
                    if(keywordSection != null) {
                        suite.KeywordSections.Add(keywordSection);
                        keywordSection = null;
                    }
                    token = arr[0];
                }
                line = await sr.ReadLineAsync();
            }

            if (testcase != null) {
                suite.TestCaseSections.Add(testcase);
                testcase = null;
            }
            if (keywordSection != null) {
                suite.KeywordSections.Add(keywordSection);
                keywordSection = null;
            }
            Console.WriteLine("-------------------------");
            //Console.WriteLine(suite);
           await File.WriteAllTextAsync("./demo.tsv", suite.ToString());
        }

        /// <summary>
        /// 解析 keyword 部分
        /// </summary>
        /// <param name="keywordSection"></param>
        /// <param name="token"></param>
        /// <param name="data"></param>
        private void UserKeyParse(KeywordSection keywordSection, string token, IEnumerable<string> data) {
            if (keywordSection is null || string.IsNullOrWhiteSpace(token) || data is null || !data.Any()) {
                return;
            }

            var dic = new Dictionary<string, Action<KeywordSection, List<string>>> {
                {
                    "[Documentation]",
                    (keyword, items) => {
                        var value = string.Join("", items);
                        if (string.IsNullOrWhiteSpace(keyword.Documentation)) {
                            keyword.Documentation = value;
                        } else {
                            keyword.Documentation = $"{keyword.Documentation}{Environment.NewLine}{value}";
                        }
                    }
                },
                {
                    "[Arguments]",
                    (keyword, items) => {
                        var len = items.Count;
                        var lastValue = items.Last();
                        var list = new List<string>();
                        var comment = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                            list = items.Take(len-1).ToList();
                        } else {
                            list = items;
                        }

                        if(keyword.Arguments.value != null) {
                            list.AddRange(keyword.Arguments.value);
                            comment = keyword.Arguments.comment;
                        }
                        keyword.Arguments = (value: list, comment: comment);
                    }
                },
                {
                    "[Teardown]",
                    (keyword, items) => {
                        var len = items.Count;
                        var lastValue = items.Last();
                        var list = new List<string>();
                        var comment = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                            list = items.Take(len-1).ToList();
                        } else {
                            list = items;
                        }

                        if(keyword.Teardown.value != null) {
                            list.AddRange(keyword.Teardown.value);
                            comment = keyword.Teardown.comment;
                        }
                        keyword.Teardown = (value: list, comment: comment);
                    }
                },
                {
                    "[Return]",
                    (keyword, items) => {
                        var len = items.Count;
                        var lastValue = items.Last();
                        var list = new List<string>();
                        var comment = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                            list = items.Take(len-1).ToList();
                        } else {
                            list = items;
                        }

                        if(keyword.ReutrnValue.value != null) {
                            list.AddRange(keyword.ReutrnValue.value);
                            comment = keyword.ReutrnValue.comment;
                        }
                        keyword.ReutrnValue = (value: list, comment: comment);
                    }
                },
                {
                    "[Timeout]",
                    (keyword, items) => {
                        var lastValue = items.Last();
                        var len = items.Count;
                        var comment = string.Empty;
                        var msg = string.Empty;
                        var value = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                        }
                        if(len > 2) {
                            value = items[0];
                            msg = items[2];
                        } else {
                            value = items[0];
                        }
                        keyword.Timeout = (value: value, msg: msg, comment: comment);
                    }
                },
                {
                    "[Tags]",
                    (keyword, items) => {
                        if(keyword.Tags is null) {
                            keyword.Tags = new List<string>();
                        }
                        keyword.Tags.AddRange(items);
                    }
                },
            };

            var arr = data.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            if (dic.TryGetValue(token, out var func)) {
                func(keywordSection, arr.Skip(1).Take(arr.Count - 1).ToList());
            } else {
                if (keywordSection.Code is null) {
                    keywordSection.Code = new System.Data.DataTable() { TableName = keywordSection.Name };
                }
                if (arr[0] == "...") {
                    var rowCount = keywordSection.Code.Rows.Count;
                    arr = arr.Skip(1).Take(arr.Count - 1).ToList();
                    var colCount = keywordSection.Code.Columns.Count;
                    var row = keywordSection.Code.Rows[rowCount - 1];
                    var index = 0;
                    foreach (var i in Enumerable.Range(0, colCount).Reverse()) {
                        if (!string.IsNullOrWhiteSpace(row[i].ToString())) {
                            break;
                        }
                        index += 1;
                    }
                    var len = arr.Count - index;
                    if (len > 0) {
                        foreach (var i in Enumerable.Range(colCount, len)) {
                            keywordSection.Code.Columns.Add($"column{i}", typeof(string));
                        }
                    }
                    foreach (var i in Enumerable.Range(0, arr.Count)) {
                        row[colCount - index + i] = arr[i];
                    }
                } else {
                    arr = arr.Skip(1).Take(arr.Count - 1).ToList();
                    var count = arr.Count + 1;
                    var columnCount = keywordSection.Code.Columns.Count;
                    // 扩展列
                    if (count > columnCount) {
                        foreach (var i in Enumerable.Range(columnCount, count - columnCount)) {
                            keywordSection.Code.Columns.Add($"column{i}", typeof(string));
                        }
                    }

                    var row = keywordSection.Code.NewRow();
                    row[0] = token;
                    foreach (var i in Enumerable.Range(0, count - 1)) {
                        row[i + 1] = arr[i];
                    }
                    keywordSection.Code.Rows.Add(row);
                }
            }
        }


        /// <summary>
        /// 解析 Test Case 部分内容
        /// </summary>
        /// <param name="testCaseSection"></param>
        /// <param name="token"></param>
        /// <param name="data"></param>
        private void TestCaseParse(TestCaseSection testCaseSection, string token, IEnumerable<string> data) {
            if(testCaseSection is null || string.IsNullOrWhiteSpace(token) || data is null || !data.Any()) {
                return;
            }

            var dic = new Dictionary<string, Action<TestCaseSection, List<string>>>() {
                {
                    "[Documentation]",
                    (testcase, items) => {
                        var value = string.Join("", items);
                        if (string.IsNullOrWhiteSpace(testcase.Documentation)) {
                            testcase.Documentation = value;
                        } else {
                            testcase.Documentation = $"{testcase.Documentation}{Environment.NewLine}{value}";
                        }
                    }
                },
                {
                    "[Setup]",
                    (testcase, items) => {
                        var len = items.Count;
                        var lastValue = items.Last();
                        var list = new List<string>();
                        var comment = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                            list = items.Take(len-1).ToList();
                        } else {
                            list = items;
                        }

                        if(testcase.Setup.value != null) {
                            list.AddRange(testcase.Setup.value);
                            comment = testcase.Setup.comment;
                        }
                        testcase.Setup = (value: list, comment: comment);
                    }
                },
                {
                    "[Teardown]",
                    (testcase, items) => {
                        var len = items.Count;
                        var lastValue = items.Last();
                        var list = new List<string>();
                        var comment = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                            list = items.Take(len-1).ToList();
                        } else {
                            list = items;
                        }

                        if(testcase.Teardown.value != null) {
                            list.AddRange(testcase.Teardown.value);
                            comment = testcase.Teardown.comment;
                        }
                        testcase.Teardown = (value: list, comment: comment);
                    }
                },
                {
                    "[Timeout]",
                    (testcase, items) => {
                        var lastValue = items.Last();
                        var len = items.Count;
                        var comment = string.Empty;
                        var msg = string.Empty;
                        var value = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                        }
                        if(len > 2) {
                            value = items[0];
                            msg = items[1];
                        } else {
                            value = items[0];
                        }
                        testcase.Timeout = (value: value, msg: msg, comment: comment);
                    }
                },
                {
                    "[Template]",
                    (testcase, items) => {
                        var lastValue = items.Last();
                        var comment = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                        }
                        testcase.Template = (value: items[0], comment: comment);
                    }
                },
                {
                    "[Tags]",
                    (testcase, items) => {
                        if(testcase.Tags is null) {
                            testcase.Tags = new List<string>();
                        }
                        testcase.Tags.AddRange(items);
                    }
                },
            };

            var arr = data.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            if(dic.TryGetValue(token, out var func)) {
                func(testCaseSection, arr.Skip(1).Take(arr.Count-1).ToList());
            } else {
                if(testCaseSection.Code is null) {
                    testCaseSection.Code = new System.Data.DataTable() { TableName = testCaseSection.Name };
                }
                if (arr[0] == "...") {
                    var rowCount = testCaseSection.Code.Rows.Count;
                    arr = arr.Skip(1).Take(arr.Count - 1).ToList();
                    var colCount = testCaseSection.Code.Columns.Count;
                    var row = testCaseSection.Code.Rows[rowCount - 1];
                    var index = 0;
                    foreach (var i in Enumerable.Range(0, colCount).Reverse()) {
                        if (!string.IsNullOrWhiteSpace(row[i].ToString())) {
                            break;
                        }
                        index += 1;
                    }
                    var len = arr.Count - index;
                    if (len > 0) {
                        foreach (var i in Enumerable.Range(colCount, len)) {
                            testCaseSection.Code.Columns.Add($"column{i}", typeof(string));
                        }
                    }
                    foreach (var i in Enumerable.Range(0, arr.Count)) {
                        row[colCount - index + i] = arr[i];
                    }
                } else {
                    arr = arr.Skip(1).Take(arr.Count - 1).ToList();
                    var count = arr.Count + 1;
                    var columnCount = testCaseSection.Code.Columns.Count;
                    // 扩展列
                    if (count > columnCount) {
                        foreach (var i in Enumerable.Range(columnCount, count - columnCount)) {
                            testCaseSection.Code.Columns.Add($"column{i}", typeof(string));
                        }
                    }

                    var row = testCaseSection.Code.NewRow();
                    row[0] = token;
                    foreach (var i in Enumerable.Range(0, count - 1)) {
                        row[i + 1] = arr[i];
                    }
                    testCaseSection.Code.Rows.Add(row);
                }
                
            }
        }


        /// <summary>
        /// 解析 Variable 部分
        /// </summary>
        /// <param name="suite"></param>
        /// <param name="token"></param>
        /// <param name="data"></param>
        private void VariableParse(SuiteSection suite, string token, IEnumerable<string> data) { 
            if (string.IsNullOrWhiteSpace(token) || data is null)
                return;

            if(suite is null) {
                suite = new SuiteSection();
            }
            var isExist = true;
            if(suite.VariableSections is null) {
                suite.VariableSections = new List<VariableSection>();
            }
            var varaible = suite.VariableSections.Where(item => item.Name == token).SingleOrDefault();
            if(varaible is null) {
                isExist = false;
                varaible = new VariableSection() {
                    Name = token,
                };
            }
            var arr = data.Where(item => !string.IsNullOrWhiteSpace(item)).ToArray();
            var count = arr.Length;
            var lastValue = arr.Last();
            if (lastValue.StartsWith("#")) {
                varaible.Comment = lastValue;
                count -= 1;
            }
            var valueString = new StringBuilder();
            foreach(var i in Enumerable.Range(0, count)) {
                valueString.Append($"|{arr[i]}");
            }
            if(!string.IsNullOrWhiteSpace(varaible.Value))
                valueString.Insert(0, varaible.Value);
            varaible.Value = valueString.ToString().TrimStart('|');
            if (!isExist) {
                suite.VariableSections.Add(varaible);
            }
        }


        /// <summary>
        /// 解析 Suite Setting 部分
        /// </summary>
        /// <param name="suite"></param>
        /// <param name="token"></param>
        /// <param name="data"></param>
        private void SuiteParse(SuiteSection suite, string token, IEnumerable<string> data) {
            if (suite is null || string.IsNullOrWhiteSpace(token) || data is null || !data.Any()) {
                return;
            }

            var dic = new Dictionary<string, Action<SuiteSection, List<string>>>() {
                {
                    "Documentation",
                    (current, arr) => {
                        var value = string.Join("", arr);
                        if (string.IsNullOrWhiteSpace(current.Documentation)) {
                            current.Documentation = value;
                        } else {
                            current.Documentation = $"{current.Documentation}{Environment.NewLine}{value}";
                        }
                    }
                },

                {
                    "Suite Setup",
                    (current, arr) => {
                        var len = arr.Count;
                        var lastValue = arr.Last();
                        var list = new List<string>();
                        var comment = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                            list = arr.Take(len-1).ToList();
                        } else {
                            list = arr;
                        }

                        if(current.SuiteSetup.value != null) {
                            list.AddRange(current.SuiteSetup.value);
                            comment = current.SuiteSetup.comment;
                        }

                        current.SuiteSetup = (value: list, comment: comment);
                    }
                },

                {
                    "Suite Teardown",
                    (current, arr) => {
                        var len = arr.Count;
                        var lastValue = arr.Last();
                        var list = new List<string>();
                        var comment = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                            list = arr.Take(len-1).ToList();
                        } else {
                            list = arr;
                        }

                        if(current.SuiteTeardown.value != null) {
                            list.AddRange(current.SuiteTeardown.value);
                            comment = current.SuiteTeardown.comment;
                        }

                        current.SuiteTeardown = (value: list, comment: comment);
                    }
                },

                {
                    "Test Setup",
                    (current, arr) => {
                        var len = arr.Count;
                        var lastValue = arr.Last();
                        var list = new List<string>();
                        var comment = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                            list = arr.Take(len-1).ToList();
                        } else {
                            list = arr;
                        }

                        if(current.TestSetup.value != null) {
                            list.AddRange(current.TestSetup.value);
                            comment = current.TestSetup.comment;
                        }

                        current.TestSetup = (value: list, comment: comment);
                    }
                },

                {
                    "Test Teardown",
                    (current, arr) => {
                        var len = arr.Count;
                        var lastValue = arr.Last();
                        var list = new List<string>();
                        var comment = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                            list = arr.Take(len-1).ToList();
                        } else {
                            list = arr;
                        }

                        if(current.TestTeardown.value != null) {
                            list.AddRange(current.TestTeardown.value);
                            comment = current.TestTeardown.comment;
                        }

                        current.TestTeardown = (value: list, comment: comment);
                    }
                },

                {
                    "Test Timeout",
                    (current, arr) => {
                        var lastValue = arr.Last();
                        var len = arr.Count;
                        var comment = string.Empty;
                        var msg = string.Empty;
                        var value = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                        }
                        if(len > 2) {
                            value = arr[0];
                            msg = arr[1];
                        } else {
                            value = arr[0];
                        }
                        current.TestTimeout = (value: value, message: msg, comment: comment);
                    }
                },

                {
                    "Test Template",
                    (current, arr) => {
                        var lastValue = arr.Last();
                        var comment = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                        }
                        current.TestTemplate = (value: arr[0], comment: comment);
                    }
                },

                {
                    "Force Tags",
                    (current, arr) => {
                        if(current.ForceTags is null) {
                            current.ForceTags = new List<string>();
                        }
                        current.ForceTags.AddRange(arr);
                    }
                },

                {
                    "Default Tags",
                    (current, arr) => {
                        if(current.DefaultTags is null) {
                            current.DefaultTags = new List<string>();
                        }
                        current.DefaultTags.AddRange(arr);
                    }
                },

                {
                    "Metadata",
                    (current, arr) => {
                        var len = arr.Count;
                        var lastValue = arr.Last();
                        var comment = string.Empty;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                        }
                        var name = string.Empty;
                        var value = string.Empty;
                        if(len > 1) {
                            name = arr[0];
                            value = arr[1];
                        } else {
                            name = arr[0];
                        }
                        if(current.Metadatas is null) {
                            current.Metadatas = new List<(string name, string value, string comment)>();
                        }
                        current.Metadatas.Add((name: name, value: value, comment: comment));
                    }
                },

                {
                    "Library",
                    (current, arr) => {
                        if(current.Libraries is null) {
                            current.Libraries = new List<(string path, List<string> args, string alias, string comment)>();
                        }
                        var path = arr[0];
                        var args = new List<string>();
                        var alias = string.Empty;
                        var comment = string.Empty;

                        var lastValue = arr.Last();
                        var len = arr.Count -1;
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                            len -= 1;
                        }

                        
                        var ret = false; // 标识是否切换存储数据

                        foreach(var i in Enumerable.Range(1, len)) {
                            if(arr[i] == "WITH NAME") {
                                ret = true;
                                continue;
                            }

                            if (ret) {
                                alias = arr[i];
                            } else {
                                args.Add(arr[i]);
                            }
                        }

                        current.Libraries.Add((path, args, alias, comment));
                    }
                },

                {
                    "Resource",
                    (current, arr) => {
                        if(current.Resources is null) {
                            current.Resources = new List<(string path, string comment)>();
                        }
                        var path = arr[0];
                        var comment = string.Empty;
                        if (arr.Last().StartsWith("#")) {
                            comment = arr.Last();
                        }
                        current.Resources.Add((path: path, comment: comment));
                    }
                },

                {
                    "Variables",
                    (current, arr) => {
                        if(current.Variables is null) {
                            current.Variables = new List<(string path, List<string> args, string comment)>();
                        }
                        var path = arr[0];
                        var comment = string.Empty;
                        var args = new List<string>();

                        var len = arr.Count - 1;
                        var lastValue = arr.Last();
                        if (lastValue.StartsWith("#")) {
                            comment = lastValue;
                            len -= 1;
                        }

                        foreach(var i in Enumerable.Range(1, len)) {
                            args.Add(arr[i]);
                        }

                        current.Variables.Add((path, args, comment));
                    }
                }
            };

            if (dic.TryGetValue(token, out var func)) {
                func(suite, data.Where(item => !string.IsNullOrWhiteSpace(item)).ToList());
            }
        }

    }
}
