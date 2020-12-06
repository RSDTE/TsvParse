using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlParse
{
    public class HtmlParser
    {
        private readonly string path;

        public HtmlParser(string path) {
            this.path = path;
        }

        public SuiteSection Parse() {
            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(path, Encoding.UTF8);
            var settingTable = htmlDoc.GetElementbyId("setting");
            var variableTable = htmlDoc.GetElementbyId("variable");
            var testcaseTable = htmlDoc.GetElementbyId("testcase");
            var keywordTable = htmlDoc.GetElementbyId("keyword");
            SuiteSection suite = new SuiteSection();
            if (settingTable != null) {
                // 解析 setting table 
                suite = SettingParse(settingTable);
            }

            if (variableTable != null) {
                // 解析 variable table
                if (suite.VariableSections is null) {
                    suite.VariableSections = new List<VariableSection>();
                }
                suite.VariableSections.AddRange(VariableParse(variableTable));
            }

            if (testcaseTable != null) {
                // 解析 testcase table
                if (suite.TestCaseSections is null) {
                    suite.TestCaseSections = new List<TestCaseSection>();
                }
                suite.TestCaseSections.AddRange(TestCaseParse(testcaseTable));
            }

            if (keywordTable != null) {
                // 解析 keyword table
                if(suite.KeywordSections is null) {
                    suite.KeywordSections = new List<KeywordSection>();
                }
                suite.KeywordSections.AddRange(KeywordParse(keywordTable));
            }

            return suite;
        }

        private SuiteSection SettingParse(HtmlNode settingTable) {
            if (settingTable is null) {
                return default;
            }

            var dic = new Dictionary<string, Action<SuiteSection, List<string>, bool>>() {
                {
                    "Documentation",
                    (current, arr, isAppend) => {
                        var value = string.Join("", arr);
                        if (isAppend) {
                            current.Documentation = value;
                        } else {
                            current.Documentation = $"{current.Documentation}{Environment.NewLine}{value}";
                        }
                    }
                },

                {
                    "Suite Setup",
                    (current, arr, isAppend) => {
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

                        if(isAppend) {
                            list.AddRange(current.SuiteSetup.value);
                            comment = current.SuiteSetup.comment;
                        }

                        current.SuiteSetup = (value: list, comment: comment);
                    }
                },

                {
                    "Suite Teardown",
                    (current, arr, isAppend) => {
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

                        if(isAppend) {
                            list.AddRange(current.SuiteTeardown.value);
                            comment = current.SuiteTeardown.comment;
                        }

                        current.SuiteTeardown = (value: list, comment: comment);
                    }
                },

                {
                    "Test Setup",
                    (current, arr, isAppend) => {
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

                        if(isAppend) {
                            list.AddRange(current.TestSetup.value);
                            comment = current.TestSetup.comment;
                        }

                        current.TestSetup = (value: list, comment: comment);
                    }
                },

                {
                    "Test Teardown",
                    (current, arr, isAppend) => {
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

                        if(isAppend) {
                            list.AddRange(current.TestTeardown.value);
                            comment = current.TestTeardown.comment;
                        }

                        current.TestTeardown = (value: list, comment: comment);
                    }
                },

                {
                    "Test Timeout",
                    (current, arr, isAppend) => {
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
                    (current, arr, isAppend) => {
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
                    (current, arr, isAppend) => {
                        if(current.ForceTags is null) {
                            current.ForceTags = new List<string>();
                        }
                        current.ForceTags.AddRange(arr);
                    }
                },

                {
                    "Default Tags",
                    (current, arr, isAppend) => {
                        if(current.DefaultTags is null) {
                            current.DefaultTags = new List<string>();
                        }
                        current.DefaultTags.AddRange(arr);
                    }
                },

                {
                    "Metadata",
                    (current, arr, isAppend) => {
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
                    (current, arr, isAppend) => {
                        if(current.Libraries is null) {
                            current.Libraries = new List<(string path, List<string> args, string alias, string comment)>();
                        }
                        // 追加
                        if (isAppend) {
                            var library = current.Libraries.Last();
                            current.Libraries.RemoveAt(current.Libraries.Count -1);
                            if(library.args.Contains("WITH NAME")) {
                                if(arr.Count > 2)
                                    return;
                                var lastData = arr.Last();
                                if (lastData.StartsWith("#")) {
                                    library.comment = lastData;
                                    if(arr.Count > 1) {
                                        library.alias= arr[0];
                                    }
                                }
                                else {
                                    library.alias = arr[0];
                                }
                            } else {
                                if(arr.Contains("WITH NAME")) {
                                    // !! 一定存在 alias
                                    // 1. args .... WITH NAME, alias, #commemt;
                                    // 2. args .... WITH NAME, #comment;  X
                                    // 3. args .... WITH NAME, alias;
                                    var index = arr.IndexOf("WITH NAME");
                                    library.args.AddRange(arr.Take(index));
                                    if (arr.Last().StartsWith("#")) {
                                        library.comment = arr.Last();
                                    }
                                    library.alias = arr[index+1];
                                } else {
                                    // 1. args ..., #comment
                                    var count = arr.Count;
                                    if (arr.Last().StartsWith("#")) {
                                        library.comment = arr.Last();
                                        count -= 1;
                                    }
                                    library.args.AddRange(arr.Take(count));
                                }
                            }
                            current.Libraries.Add(library);
                        } else {
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
                            foreach(var i in Enumerable.Range(1, len)) {
                                if(args.Contains("WITH NAME")) {
                                     alias = arr[i];
                                     break;
                                }
                                args.Add(arr[i]);
                            }
                            current.Libraries.Add((path, args, alias, comment));
                        }

                    }
                },

                {
                    "Resource",
                    (current, arr, isAppend) => {
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
                    (current, arr, isAppend) => {
                        if(current.Variables is null) {
                            current.Variables = new List<(string path, List<string> args, string comment)>();
                        }
                        if (isAppend) {
                            var lastVaraible = current.Variables.Last();
                            current.Variables.RemoveAt(current.Variables.Count-1);
                            var count = arr.Count;
                            if (arr.Last().StartsWith("#")) {
                                lastVaraible.comment = arr.Last();
                                count -= 1;
                            }
                            lastVaraible.args.AddRange(arr.Take(count));
                        } else {
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
                }
            };

            var suite = new SuiteSection();

            // selectedNodes 结果有问题.
            var trNodes = settingTable.Elements("tr").ToArray();
            var token = string.Empty;
            foreach (var i in Enumerable.Range(1, trNodes.Length - 1)) {
                var trNode = trNodes[i];  // 单个 tr 元素
                var tdNodes = trNode.Elements("td").ToArray(); // td 元素集合
                var name = tdNodes[0].InnerText; // 首个 td 元素为标识符\
                var isAppend = name == "...";
                token = isAppend ? token : name;
                var nodes = tdNodes.Skip(1).Take(tdNodes.Length - 1)
                                            .Select(item => item.InnerText)
                                            .Where(item => !string.IsNullOrWhiteSpace(item))
                                            .ToList();
                if (dic.TryGetValue(token, out var func)) {
                    func(suite, nodes, isAppend);
                }
            }

            if (suite.Libraries != null && suite.Libraries.Any()) {
                suite.Libraries.Where(item => item.args.Contains("WITH NAME")).ToList().ForEach(item => item.args.Remove("WITH NAME"));
            }
            return suite;
        }

        private List<VariableSection> VariableParse(HtmlNode variableTable) {
            List<VariableSection> variableSections = new List<VariableSection>();
            if (variableTable is null) {
                return variableSections;
            }

            var trNodes = variableTable.Elements("tr").ToArray();
            if (trNodes[0].InnerText.Trim() != "Variables")
                return variableSections;
            var token = string.Empty;
            foreach (var i in Enumerable.Range(1, trNodes.Length - 1)) {
                var tdNodes = trNodes[i].Elements("td").ToArray();
                var name = tdNodes[0].InnerText;
                if (name != "...") {
                    token = name;
                }
                if (token.StartsWith("&amp;")) {
                    token = token.Replace("&amp;", "&");
                }


                var data = tdNodes.Skip(1)
                                .Take(tdNodes.Length - 1)
                                .Select(item => item.InnerText)
                                .Where(item => !string.IsNullOrWhiteSpace(item))
                                .ToArray();
                if (data.Length < 1)
                    continue;
                var isExist = true;
                var varaible = variableSections.Where(item => item.Name == token).SingleOrDefault();
                if (varaible is null) {
                    isExist = false;
                    varaible = new VariableSection() {
                        Name = token,
                    };
                }

                var count = data.Length;
                var lastValue = data.Last();
                if (lastValue.StartsWith("#")) {
                    varaible.Comment = lastValue;
                    count -= 1;
                }
                var valueString = new StringBuilder();
                foreach (var index in Enumerable.Range(0, count)) {
                    valueString.Append($"|{data[index]}");
                }
                if (!string.IsNullOrWhiteSpace(varaible.Value))
                    valueString.Insert(0, varaible.Value);
                varaible.Value = valueString.ToString().TrimStart('|');
                if (!isExist) {
                    variableSections.Add(varaible);
                }
            }
            return variableSections;
        }

        private List<TestCaseSection> TestCaseParse(HtmlNode testCaseTable) {
            var res = new List<TestCaseSection>();
            var trNodes = testCaseTable.Elements("tr").ToArray();
            if (trNodes[0].InnerText.Trim() != "Test Cases") {
                return res;
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

            TestCaseSection testCase = null;
            var token = string.Empty;
            foreach (var i in Enumerable.Range(1, trNodes.Length - 1)) {
                var tdNodes = trNodes[i].Elements("td").ToArray();
                if (!string.IsNullOrWhiteSpace(tdNodes[0].InnerText.Trim())) {
                    if (testCase != null) {
                        res.Add(testCase);
                    }
                    testCase = new TestCaseSection { Name = tdNodes[0].InnerText };
                }
                var name = tdNodes[1].InnerText.Trim();
                if (name != "...") {
                    token = name;
                }

                if (dic.TryGetValue(token, out var func)) {
                    var nodes = tdNodes.Skip(2).Take(tdNodes.Length - 2)
                                            .Select(item => item.InnerText)
                                            .Where(item => !string.IsNullOrWhiteSpace(item))
                                            .ToList();
                    if (nodes.Count < 1)
                        continue;
                    func(testCase, nodes);
                } else {
                    if (testCase.Code is null) {
                        testCase.Code = new System.Data.DataTable() { TableName = testCase.Name };
                    }
                    var nodes = tdNodes.Skip(1).Take(tdNodes.Length - 1)
                                            .Select(item => item.InnerText)
                                            .Where(item => !string.IsNullOrWhiteSpace(item))
                                            .ToList();
                    if (nodes.Count < 1)
                        continue;

                    if (nodes[0] == "...") {
                        var rowCount = testCase.Code.Rows.Count;
                        nodes = nodes.Skip(1).Take(nodes.Count - 1).ToList();
                        var colCount = testCase.Code.Columns.Count;
                        var row = testCase.Code.Rows[rowCount - 1];
                        var index = 0;
                        foreach (var k in Enumerable.Range(0, colCount).Reverse()) {
                            if (!string.IsNullOrWhiteSpace(row[k].ToString())) {
                                break;
                            }
                            index += 1;
                        }
                        var len = nodes.Count - index;
                        if (len > 0) {
                            foreach (var k in Enumerable.Range(colCount, len)) {
                                testCase.Code.Columns.Add($"column{k}", typeof(string));
                            }
                        }
                        foreach (var k in Enumerable.Range(0, nodes.Count)) {
                            row[colCount - index + k] = nodes[k];
                        }
                    } else {
                        nodes = nodes.Skip(1).Take(nodes.Count - 1).ToList();
                        var count = nodes.Count + 1;
                        var columnCount = testCase.Code.Columns.Count;
                        // 扩展列
                        if (count > columnCount) {
                            foreach (var k in Enumerable.Range(columnCount, count - columnCount)) {
                                testCase.Code.Columns.Add($"column{k}", typeof(string));
                            }
                        }

                        var row = testCase.Code.NewRow();
                        row[0] = token;
                        foreach (var k in Enumerable.Range(0, count - 1)) {
                            row[k + 1] = nodes[k];
                        }
                        testCase.Code.Rows.Add(row);
                    }

                }
            }
            if (testCase != null) {
                res.Add(testCase);
            }
            return res;
        }

        private List<KeywordSection> KeywordParse(HtmlNode keywordTable) {
            var res = new List<KeywordSection>();
            var trNodes = keywordTable.Elements("tr").ToArray();
            if(trNodes[0].InnerText.Trim() != "Keywords") {
                return res;
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
                            msg = items[1];
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

            KeywordSection keywordSection = null;
            var token = string.Empty;
            foreach(var i in Enumerable.Range(1, trNodes.Length - 1)) {
                var tdNodes = trNodes[i].Elements("td").ToArray();
                if (!string.IsNullOrWhiteSpace(tdNodes[0].InnerText.Trim())) {
                    if (keywordSection != null) {
                        res.Add(keywordSection);
                    }
                    keywordSection = new KeywordSection { Name = tdNodes[0].InnerText };
                }
                var name = tdNodes[1].InnerText.Trim();
                if (name != "...") {
                    token = name;
                }
                if (dic.TryGetValue(token, out var func)) {
                    var nodes = tdNodes.Skip(2).Take(tdNodes.Length - 2)
                                            .Select(item => item.InnerText)
                                            .Where(item => !string.IsNullOrWhiteSpace(item))
                                            .ToList();
                    if (nodes.Count < 1)
                        continue;
                    func(keywordSection, nodes);
                } else {
                    if (keywordSection.Code is null) {
                        keywordSection.Code = new System.Data.DataTable() { TableName = keywordSection.Name };
                    }
                    var nodes = tdNodes.Skip(1).Take(tdNodes.Length - 1)
                                            .Select(item => item.InnerText)
                                            .Where(item => !string.IsNullOrWhiteSpace(item))
                                            .ToList();
                    if (nodes.Count < 1)
                        continue;

                    if (nodes[0] == "...") {
                        var rowCount = keywordSection.Code.Rows.Count;
                        nodes = nodes.Skip(1).Take(nodes.Count - 1).ToList();
                        var colCount = keywordSection.Code.Columns.Count;
                        var row = keywordSection.Code.Rows[rowCount - 1];
                        var index = 0;
                        foreach (var k in Enumerable.Range(0, colCount).Reverse()) {
                            if (!string.IsNullOrWhiteSpace(row[k].ToString())) {
                                break;
                            }
                            index += 1;
                        }
                        var len = nodes.Count - index;
                        if (len > 0) {
                            foreach (var k in Enumerable.Range(colCount, len)) {
                                keywordSection.Code.Columns.Add($"column{k}", typeof(string));
                            }
                        }
                        foreach (var k in Enumerable.Range(0, nodes.Count)) {
                            row[colCount - index + k] = nodes[k];
                        }
                    } else {
                        nodes = nodes.Skip(1).Take(nodes.Count - 1).ToList();
                        var count = nodes.Count + 1;
                        var columnCount = keywordSection.Code.Columns.Count;
                        // 扩展列
                        if (count > columnCount) {
                            foreach (var k in Enumerable.Range(columnCount, count - columnCount)) {
                                keywordSection.Code.Columns.Add($"column{k}", typeof(string));
                            }
                        }

                        var row = keywordSection.Code.NewRow();
                        row[0] = token;
                        foreach (var k in Enumerable.Range(0, count - 1)) {
                            row[k + 1] = nodes[k];
                        }
                        keywordSection.Code.Rows.Add(row);
                    }

                }
            }
            if(keywordSection != null) {
                res.Add(keywordSection);
            }

            return res;
        }
    }
}
