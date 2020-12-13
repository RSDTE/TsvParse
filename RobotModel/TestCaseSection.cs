using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RobotModel
{
    public class TestCaseSection
    {
        private string name;
        private string documentation;
        private ValuesComment setup = new ValuesComment();
        private ValuesComment teardown = new ValuesComment();
        private ValueComment template = new ValueComment();
        private Timeout timeout = new Timeout();
        private List<string> tags = new List<string>();
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

        public ValuesComment Setup {
            get {
                return this.setup;
            }

            set {
                this.setup = value;
            }
        }

        public ValuesComment Teardown {
            get {
                return this.teardown;
            }

            set {
                this.teardown = value;
            }
        }

        public ValueComment Template {
            get {
                return this.template;
            }

            set {
                this.template = value;
            }
        }

        public Timeout Timeout {
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
