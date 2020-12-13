using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotModel
{
    public class Variable
    {
        private string pathValue;
        private List<string> args;
        private string comment;

        public string PathValue {
            get {
                return this.pathValue;
            }

            set {
                this.pathValue = value;
            }
        }

        public List<string> Args {
            get {
                return this.args;
            }

            set {
                this.args = value;
            }
        }

        public string Comment {
            get {
                return this.comment;
            }

            set {
                this.comment = value;
            }
        }
    }
}
