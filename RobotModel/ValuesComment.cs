using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotModel
{
    public class ValuesComment
    {
        private List<string> values = new List<string>();
        private string comment;

        public List<string> Values {
            get {
                return this.values;
            }

            set {
                this.values = value;
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
