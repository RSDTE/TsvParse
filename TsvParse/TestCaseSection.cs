using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TsvParse
{
    public class TestCaseSection
    {
        private readonly string separator = "\t";

        private string name;
        private string documentation;
        private (List<string> value, string comment) setup;
        private (List<string> value, string comment) teardown;
        private (string value, string comment) template;
        private (string value, string msg, string comment) timeout;
        private List<string> tags;
        private DataTable code;

        #region 属性
        public string Documentation {
            get {
                return this.documentation;
            }

            set {
                this.documentation = value;
            }
        }

        public (List<string> value, string comment) Setup {
            get {
                return this.setup;
            }

            set {
                this.setup = value;
            }
        }

        public (List<string> value, string comment) Teardown {
            get {
                return this.teardown;
            }

            set {
                this.teardown = value;
            }
        }

        public (string value, string comment) Template {
            get {
                return this.template;
            }

            set {
                this.template = value;
            }
        }

        public (string value, string msg, string comment) Timeout {
            get {
                return this.timeout;
            }

            set {
                this.timeout = value;
            }
        }

        public List<string> Tags {
            get {
                return this.tags;
            }

            set {
                this.tags = value;
            }
        }

        public DataTable Code {
            get {
                return this.code;
            }

            set {
                this.code = value;
            }
        }

        public string Name {
            get {
                return this.name;
            }

            set {
                this.name = value;
            }
        }
        #endregion

        /// <summary>
        /// 格式显示
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            var res = new StringBuilder();
            var data = new string[8];
            data[0] = this.Name;
            
            if (!string.IsNullOrWhiteSpace(Documentation)) {
                data[1] = $"[{nameof(this.Documentation)}]";
                foreach(var item in this.Documentation.Split(Environment.NewLine)) {
                    if (string.IsNullOrWhiteSpace(data[1])) {
                        data[1] = "...";
                    }
                    data[2] = item;
                    res.Append(WriteRow(data));
                    Array.Clear(data, 0, data.Length);
                }
            }
            
            // 写出 suite setup 的内容
            if (this.Setup.value != null && this.Setup.value.Any()) {
                data[1] = $"[{nameof(this.Setup)}]";
                var index = 2;
                foreach (var item in this.Setup.value) {
                    if (string.IsNullOrWhiteSpace(data[1])) {
                        data[1] = "...";
                    }
                    data[index] = item;
                    index += 1;
                    if (index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 2;
                    }
                }

                if (string.IsNullOrWhiteSpace(data[1])) {
                    data[1] = "...";
                }

                if (!string.IsNullOrWhiteSpace(this.Setup.comment)) {
                    data[index] = this.Setup.comment;
                    index += 1;
                }
                if (index > 1) {
                    res.Append(WriteRow(data));
                    Array.Clear(data, 0, data.Length);
                }
            }

            if (this.Teardown.value != null && this.Teardown.value.Any()) {
                data[1] = $"[{nameof(this.Teardown)}]";
                var index = 2;
                foreach (var item in this.Teardown.value) {
                    if (string.IsNullOrWhiteSpace(data[1])) {
                        data[1] = "...";
                    }
                    data[index] = item;
                    index += 1;
                    if (index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 2;
                    }
                }

                if (string.IsNullOrWhiteSpace(data[1])) {
                    data[1] = "...";
                }

                if (!string.IsNullOrWhiteSpace(this.Teardown.comment)) {
                    data[index] = this.Teardown.comment;
                    index += 1;
                }
                if (index > 1) {
                    res.Append(WriteRow(data));
                    Array.Clear(data, 0, data.Length);
                }
            }

            if (!string.IsNullOrWhiteSpace(this.Template.value)) {
                data[1] = $"[{nameof(this.Template)}]";
                if (!string.IsNullOrWhiteSpace(this.Template.comment)) {
                    data[2] = this.Template.comment;
                }
                res.Append(WriteRow(data));
                Array.Clear(data, 0, data.Length);
            }

            if (!string.IsNullOrWhiteSpace(this.Timeout.value)) {
                data[1] = $"[{nameof(this.Timeout)}]";
                data[2] = this.Timeout.value;
                var index = 3;
                if (!string.IsNullOrWhiteSpace(this.Timeout.msg)) {
                    data[index] = this.Timeout.msg;
                    index += 1;
                }

                if (!string.IsNullOrWhiteSpace(this.Timeout.comment)) {
                    data[index] = this.Timeout.comment;
                }
                res.Append(WriteRow(data));
                Array.Clear(data, 0, data.Length);
            }

            if (this.Tags != null && this.Tags.Any()) {
                data[1] = $"[{nameof(this.Tags)}]";
                var index = 2;
                foreach (var item in Tags) {
                    if (string.IsNullOrWhiteSpace(data[0])) {
                        data[1] = "...";
                    }
                    data[index] = item;
                    index += 1;
                    if (index > 7) {
                        res.Append(WriteRow(data));
                        Array.Clear(data, 0, data.Length);
                        index = 2;
                    }
                }
                if (index > 1) {
                    res.Append(WriteRow(data));
                    Array.Clear(data, 0, data.Length);
                }
            }

            if(this.Code != null) {
                int index = 1;
                var ret = false;
                foreach(DataRow row in this.Code.Rows) {
                    foreach(DataColumn col in this.Code.Columns) {
                        if(ret && data[1] != "...") {
                            data[1] = "...";
                            index = 2;
                        }
                        var value = row[col].ToString();
                        if (string.IsNullOrWhiteSpace(value)) {
                            break;
                        }

                        data[index++] = value;
                        if(index > 7) {
                            res.Append(WriteRow(data));
                            Array.Clear(data, 0, data.Length);
                            index = 1;
                            ret = true;
                        }
                    }
                    res.Append(WriteRow(data));
                    ret = false;
                    Array.Clear(data, 0, data.Length);
                    index = 1;
                }
            }

            return res.ToString();
        }



        private string WriteRow(IList<string> data) {
            var strBuilder = new StringBuilder();
            foreach (var i in Enumerable.Range(0, 8)) {
                if (i < data.Count()) {
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
