namespace HtmlParse
{
    public class VariableSection
    {

        private string name;
        private string value;
        private string comment;

        #region 属性
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
        #endregion
    }
}
