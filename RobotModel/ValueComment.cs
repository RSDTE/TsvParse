using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotModel
{
    public class ValueComment
    {
        private string value;
        private string comment;

        public string Comment {
            get {
                return this.comment;
            }

            set {
                this.comment = value;
            }
        }

        public string Value {
            get {
                return this.value;
            }

            set {
                this.value = value;
            }
        }
    }
}
