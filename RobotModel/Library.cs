using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotModel
{
    public class Library
    {
        private string pathValue;
        private List<string> args = new List<string>();
        private string alias;
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

        public string Alias {
            get {
                return this.alias;
            }

            set {
                this.alias = value;
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
