using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotModel
{
    public class Metadata
    {
        private string name;
        private string value;
        private string comment;

        public string Name {
            get {
                return this.name;
            }

            set {
                this.name = value;
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
