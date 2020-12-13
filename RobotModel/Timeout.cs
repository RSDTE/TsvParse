using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotModel
{
    public class Timeout
    {
        private string value;
        private string msg;
        private string comment;

        public string Value {
            get {
                return this.value;
            }

            set {
                this.value = value;
            }
        }

        public string Msg {
            get {
                return this.msg;
            }

            set {
                this.msg = value;
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
