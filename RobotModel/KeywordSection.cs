using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RobotModel
{
    public class KeywordSection
    {
        private string name;
        private DataTable code;
        private string documentation;
        private ValuesComment arguments = new ValuesComment();
        private ValuesComment teardown = new ValuesComment();
        private ValuesComment reutrnValue = new ValuesComment();
        private Timeout timeout = new Timeout();
        private List<string> tags = new List<string>();
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

        public ValuesComment Arguments {
            get {
                return this.arguments;
            }

            set {
                this.arguments = value;
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

        public ValuesComment ReutrnValue {
            get {
                return this.reutrnValue;
            }

            set {
                this.reutrnValue = value;
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
