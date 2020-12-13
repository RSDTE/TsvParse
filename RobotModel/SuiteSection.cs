using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotModel
{
    public class SuiteSection
    {
        private string documentation;
        private ValuesComment suiteSetup = new ValuesComment();
        private ValuesComment suiteTeardown = new ValuesComment();
        private ValuesComment testSetup = new ValuesComment();
        private ValuesComment testTeardown = new ValuesComment();
        private Timeout testTimeout = new Timeout();
        private ValueComment testTemplate = new ValueComment();
        private List<string> forceTags = new List<string>();
        private List<string> defaultTags = new List<string>();
        private List<Metadata> metadatas = new List<Metadata>();
        private List<Library> libraries = new List<Library>();
        private List<ValueComment> resources = new List<ValueComment>();
        private List<Variable> variables = new List<Variable>();

        private List<KeywordSection> keywordSections = new List<KeywordSection>();
        private List<TestCaseSection> testCaseSections = new List<TestCaseSection>();
        private List<VariableSection> variableSections = new List<VariableSection>();

        public string Documentation {
            get {
                return this.documentation;
            }

            set {
                this.documentation = value;
            }
        }

        public ValuesComment SuiteSetup {
            get {
                return this.suiteSetup;
            }

            set {
                this.suiteSetup = value;
            }
        }

        public ValuesComment SuiteTeardown {
            get {
                return this.suiteTeardown;
            }

            set {
                this.suiteTeardown = value;
            }
        }

        public ValuesComment TestSetup {
            get {
                return this.testSetup;
            }

            set {
                this.testSetup = value;
            }
        }

        public ValuesComment TestTeardown {
            get {
                return this.testTeardown;
            }

            set {
                this.testTeardown = value;
            }
        }

        public Timeout TestTimeout {
            get {
                return this.testTimeout;
            }

            set {
                this.testTimeout = value;
            }
        }

        public ValueComment TestTemplate {
            get {
                return this.testTemplate;
            }

            set {
                this.testTemplate = value;
            }
        }

        public List<string> ForceTags {
            get {
                return this.forceTags;
            }

            set {
                this.forceTags = value;
            }
        }

        public List<string> DefaultTags {
            get {
                return this.defaultTags;
            }

            set {
                this.defaultTags = value;
            }
        }

        public List<Metadata> Metadatas {
            get {
                return this.metadatas;
            }

            set {
                this.metadatas = value;
            }
        }

        public List<Library> Libraries {
            get {
                return this.libraries;
            }

            set {
                this.libraries = value;
            }
        }

        public List<ValueComment> Resources {
            get {
                return this.resources;
            }

            set {
                this.resources = value;
            }
        }

        public List<Variable> Variables {
            get {
                return this.variables;
            }

            set {
                this.variables = value;
            }
        }

        public List<KeywordSection> KeywordSections {
            get {
                return this.keywordSections;
            }

            set {
                this.keywordSections = value;
            }
        }

        public List<TestCaseSection> TestCaseSections {
            get {
                return this.testCaseSections;
            }

            set {
                this.testCaseSections = value;
            }
        }

        public List<VariableSection> VariableSections {
            get {
                return this.variableSections;
            }

            set {
                this.variableSections = value;
            }
        }
    }
}
