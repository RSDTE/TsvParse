using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HtmlParse
{
    public class TestCaseSection
    {
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

    }
}
