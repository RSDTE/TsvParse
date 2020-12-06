using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HtmlParse
{
    public class KeywordSection
    {
        private string name;
        private DataTable code;
        private string documentation;
        private (List<string> value, string comment) arguments;
        private (List<string> value, string comment) teardown;
        private (List<string> value, string comment) reutrnValue;
        private (string value, string msg, string comment) timeout;
        private List<string> tags;

        #region 属性
        public DataTable Code {
            get {
                return this.code;
            }

            set {
                this.code = value;
            }
        }

        public string Documentation {
            get {
                return this.documentation;
            }

            set {
                this.documentation = value;
            }
        }

        public (List<string> value, string comment) Arguments {
            get {
                return this.arguments;
            }

            set {
                this.arguments = value;
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

        public (List<string> value, string comment) ReutrnValue {
            get {
                return this.reutrnValue;
            }

            set {
                this.reutrnValue = value;
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

        public string Name {
            get {
                return this.name;
            }

            set {
                this.name = value;
            }
        }
        #endregion
    }
}
