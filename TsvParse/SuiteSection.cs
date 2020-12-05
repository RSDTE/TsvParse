using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TsvParse
{
    public class SuiteSection
    {
        private readonly string separator = "\t";

        private string documentation;
        private (List<string> value, string comment) suiteSetup;
        private (List<string> value, string comment) suiteTeardown;
        private (List<string> value, string comment) testSetup;
        private (List<string> value, string comment) testTeardown;
        private (string value, string message, string comment) testTimeout;
        private (string value, string comment) testTemplate;
        private List<string> forceTags;
        private List<string> defaultTags;
        private List<(string name, string value, string comment)> metadatas;
        private List<(string path, List<string> args, string alias, string comment)> libraries;
        private List<(string path, string comment)> resources;
        private List<(string path, List<string> args, string comment)> variables;

        private List<KeywordSection> keywordSections;
        private List<TestCaseSection> testCaseSections;
        private List<VariableSection> variableSections;

        #region 属性
        public string Documentation {
            get {
                return this.documentation;
            }

            set {
                this.documentation = value;
            }
        }

        public (List<string> value, string comment) SuiteSetup {
            get {
                return this.suiteSetup;
            }

            set {
                this.suiteSetup = value;
            }
        }

        public (List<string> value, string comment) SuiteTeardown {
            get {
                return this.suiteTeardown;
            }

            set {
                this.suiteTeardown = value;
            }
        }

        public (List<string> value, string comment) TestSetup {
            get {
                return this.testSetup;
            }

            set {
                this.testSetup = value;
            }
        }

        public (List<string> value, string comment) TestTeardown {
            get {
                return this.testTeardown;
            }

            set {
                this.testTeardown = value;
            }
        }

        public (string value, string message, string comment) TestTimeout {
            get {
                return this.testTimeout;
            }

            set {
                this.testTimeout = value;
            }
        }

        public (string value, string comment) TestTemplate {
            get {
                return this.testTemplate;
            }

            set {
                this.testTemplate = value;
            }
        }

        public List<string> ForceTags {
            get {
                return this.forceTags;
            }

            set {
                this.forceTags = value;
            }
        }

        public List<string> DefaultTags {
            get {
                return this.defaultTags;
            }

            set {
                this.defaultTags = value;
            }
        }

        public List<(string name, string value, string comment)> Metadatas {
            get {
                return this.metadatas;
            }

            set {
                this.metadatas = value;
            }
        }

        public List<(string path, List<string> args, string alias, string comment)> Libraries {
            get {
                return this.libraries;
            }

            set {
                this.libraries = value;
            }
        }

        public List<(string path, string comment)> Resources {
            get {
                return this.resources;
            }

            set {
                this.resources = value;
            }
        }

        public List<(string path, List<string> args, string comment)> Variables {
            get {
                return this.variables;
            }

            set {
                this.variables = value;
            }
        }

        public List<KeywordSection> KeywordSections {
            get {
                return this.keywordSections;
            }

            set {
                this.keywordSections = value;
            }
        }

        public List<TestCaseSection> TestCaseSections {
            get {
                return this.testCaseSections;
            }

            set {
                this.testCaseSections = value;
            }
        }

        public List<VariableSection> VariableSections {
            get {
                return this.variableSections;
            }

            set {
                this.variableSections = value;
            }
        }
        #endregion

        public override string ToString() {
            var res = new StringBuilder();
            var data = new string[8];

            data[0] = "*Settings*";
            res.Append(WriteRow(data));
            Array.Clear(data, 0, data.Length);

            // 写出 documentation 内容
            if (!string.IsNullOrWhiteSpace(this.Documentation)) {
                data[0] = nameof(this.Documentation);
                foreach (var item in this.Documentation.Split(Environment.NewLine)) {
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }

                    data[1] = item;
                    res.Append(WriteRow(data));
                    Array.Clear(data, 0, data.Length);
                }
            }

            Array.Clear(data, 0, data.Length);
            // 写出 suite setup 的内容
            if (this.SuiteSetup.value != null && this.SuiteSetup.value.Any()) {
                data[0] = "Suite Setup";
                var index = 1;
                foreach(var item in this.SuiteSetup.value) {
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }
                    data[index] = item;
                    index += 1;
                    if (index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 1;
                    }
                }

                if (string.IsNullOrWhiteSpace(data[0])) {
                    data[0] = "...";
                }

                if (!string.IsNullOrWhiteSpace(this.SuiteSetup.comment)) {
                    data[index] = this.SuiteSetup.comment;
                    index += 1;
                }
                if(index > 1) {
                    res.Append(WriteRow(data));
                    Array.Clear(data, 0, data.Length);
                }
            }

            if (this.SuiteTeardown.value != null && this.SuiteTeardown.value.Any()) {
                data[0] = "Suite Teardown";
                var index = 1;
                foreach (var item in this.SuiteTeardown.value) {
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }
                    data[index] = item;
                    index += 1;
                    if (index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 1;
                    }
                }

                if (string.IsNullOrWhiteSpace(data[0])) {
                    data[0] = "...";
                }

                if (!string.IsNullOrWhiteSpace(this.SuiteTeardown.comment)) {
                    data[index] = this.SuiteTeardown.comment;
                    index += 1;
                }
                if (index > 1) {
                    res.Append(WriteRow(data));
                    Array.Clear(data, 0, data.Length);
                }
            }

            if (this.TestSetup.value != null && this.TestSetup.value.Any()) {
                data[0] = "Test Setup";
                var index = 1;
                foreach (var item in this.TestSetup.value) {
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }
                    data[index] = item;
                    index += 1;
                    if (index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 1;
                    }
                }

                if (string.IsNullOrWhiteSpace(data[0])) {
                    data[0] = "...";
                }

                if (!string.IsNullOrWhiteSpace(this.TestSetup.comment)) {
                    data[index] = this.TestSetup.comment;
                    index += 1;
                }
                if (index > 1) {
                    res.Append(WriteRow(data));
                    Array.Clear(data, 0, data.Length);
                }
            }

            if (this.TestTeardown.value != null && this.TestTeardown.value.Any()) {
                data[0] = "Test Teardown";
                var index = 1;
                foreach (var item in this.TestTeardown.value) {
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }
                    data[index] = item;
                    index += 1;
                    if (index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 1;
                    }
                }

                if (string.IsNullOrWhiteSpace(data[0])) {
                    data[0] = "...";
                }

                if (!string.IsNullOrWhiteSpace(this.TestTeardown.comment)) {
                    data[index] = this.TestTeardown.comment;
                    index += 1;
                }
                if (index > 1) {
                    res.Append(WriteRow(data));
                    Array.Clear(data, 0, data.Length);
                }
            }

            if (!string.IsNullOrWhiteSpace(this.TestTimeout.value)) {
                data[0] = "Test Timeout";
                data[1] = this.TestTimeout.value;
                var index = 2;
                if (!string.IsNullOrWhiteSpace(this.TestTimeout.message)) {
                    data[index] = this.TestTimeout.message;
                    index += 1;
                }

                if (!string.IsNullOrWhiteSpace(this.TestTimeout.comment)) {
                    data[index] = this.TestTimeout.comment;
                }
                res.Append(WriteRow(data));
                Array.Clear(data, 0, data.Length);
            }

            if (!string.IsNullOrWhiteSpace(this.TestTemplate.value)) {
                data[0] = "Test Template";
                if (!string.IsNullOrWhiteSpace(this.TestTemplate.comment)) {
                    data[1] = this.TestTemplate.comment;
                }
                res.Append(WriteRow(data));
                Array.Clear(data, 0, data.Length);
            }

            if (this.ForceTags != null && this.ForceTags.Any()) {
                data[0] = "Force Tags";
                var index = 1;
                foreach(var item in ForceTags) {
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }
                    data[index] = item;
                    index += 1;
                    if(index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 1;
                    }
                }
                if (index > 1) {
                    res.Append(WriteRow(data));
                    Array.Clear(data, 0, data.Length);
                }
            }

            if (this.DefaultTags != null && this.DefaultTags.Any()) {
                data[0] = "Default Tags";
                var index = 1;
                foreach (var item in DefaultTags) {
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }
                    data[index] = item;
                    index += 1;
                    if (index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 1;
                    }
                }
                if (index > 1) {
                    res.Append(WriteRow(data));
                    Array.Clear(data, 0, data.Length);
                }
            }

            foreach(var metadata in Metadatas) {
                if(string.IsNullOrWhiteSpace(metadata.name) || string.IsNullOrWhiteSpace(metadata.value)) {
                    continue;
                }
                data[0] = "Metadata";
                data[1] = metadata.name;
                data[2] = metadata.value;
                if (!string.IsNullOrWhiteSpace(metadata.comment)) {
                    data[3] = metadata.comment;
                }
                res.Append(WriteRow(data));
                Array.Clear(data, 0, data.Length);
            }

            foreach (var library in Libraries) {
                if (string.IsNullOrWhiteSpace(library.path))
                    continue;
                data[0] = "Library";
                data[1] = library.path;
                var index = 2;
                foreach(var arg in library.args) {
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }
                    data[index++] = arg;
                    if (index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 1;
                    }
                }
                if (!string.IsNullOrWhiteSpace(library.alias)) {
                    data[index++] = "WITH NAME";
                    if (index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 1;
                    }
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }
                    data[index++] = library.alias;
                    if (index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 1;
                    }
                }

                if (!string.IsNullOrWhiteSpace(library.comment)) {
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }
                    data[index] = library.comment;
                }
                res.Append(WriteRow(data));
                Array.Clear(data, 0, data.Length);
            }

            foreach(var resource in Resources) {
                if (string.IsNullOrWhiteSpace(resource.path)) {
                    continue;
                }
                data[0] = "Resource";
                data[1] = resource.path;
                if (!string.IsNullOrWhiteSpace(resource.comment)) {
                    data[2] = resource.comment;
                }
                res.Append(WriteRow(data));
                Array.Clear(data, 0, data.Length);
            }

            foreach (var variable in Variables) {
                if (string.IsNullOrWhiteSpace(variable.path))
                    continue;
                data[0] = "Variables";
                data[1] = variable.path;
                var index = 2;
                foreach (var arg in variable.args) {
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }
                    data[index++] = arg;
                    if (index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 1;
                    }
                }

                if (!string.IsNullOrWhiteSpace(variable.comment)) {
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }
                    data[index] = variable.comment;
                }
                res.Append(WriteRow(data));
                Array.Clear(data, 0, data.Length);
            }

            if(this.VariableSections != null && this.VariableSections.Any()) {
                res.Append(WriteRow(data));
                data[0] = "*Variables*";
                res.Append(WriteRow(data));
                Array.Clear(data, 0, data.Length);
                foreach (var variable in VariableSections) {
                    data[0] = variable.Name;
                    var index = 1;
                    foreach (var item in variable.Value.Split("|")) {
                        if (string.IsNullOrWhiteSpace(data[0])) {
                            data[0] = "...";
                        }
                        data[index++] = item;
                        if (index > 7) {
                            res.Append(WriteRow(data));
                            Array.Clear(data, 0, data.Length);
                            index = 1;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[0] = "...";
                    }
                    if (!string.IsNullOrWhiteSpace(variable.Comment)) {
                        data[index] = variable.Comment;
                    }
                    res.Append(WriteRow(data));
                    Array.Clear(data, 0, data.Length);
                }
            }

            if(this.TestCaseSections != null && this.TestCaseSections.Any()) {
                res.Append(WriteRow(data));
                data[0] = "*Test Cases*";
                res.Append(WriteRow(data));
                Array.Clear(data, 0, data.Length);
                foreach(var item in this.TestCaseSections) {
                    res.Append(item);
                }
            }

            
            if (this.KeywordSections != null && this.KeywordSections.Any()) {
                res.Append(WriteRow(data));
                data[0] = "*Keywords*";
                res.Append(WriteRow(data));
                Array.Clear(data, 0, data.Length);
                foreach (var item in this.KeywordSections) {
                    res.Append(item);
                }
            }

            return res.ToString();
        }

        private string WriteRow(IList<string> data) {
            var strBuilder = new StringBuilder();
            foreach (var i in Enumerable.Range(0, 8)) {
                if(i< data.Count()) {
                    strBuilder.Append(data[i]);
                } else {
                    strBuilder.Append(string.Empty);
                }
                if (i < 7)
                    strBuilder.Append(this.separator);
            }
            strBuilder.Append(Environment.NewLine);
            return strBuilder.ToString();
        }


    }
}
