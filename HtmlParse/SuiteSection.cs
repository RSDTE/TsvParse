using HtmlParse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlParse
{
    public class SuiteSection
    {
        private string documentation;
        private (List<string> value, string comment) suiteSetup;
        private (List<string> value, string comment) suiteTeardown;
        private (List<string> value, string comment) testSetup;
        private (List<string> value, string comment) testTeardown;
        private (string value, string message, string comment) testTimeout;
        private (string value, string comment) testTemplate;
        private List<string> forceTags;
        private List<string> defaultTags;
        private List<(string name, string value, string comment)> metadatas;
        private List<(string path, List<string> args, string alias, string comment)> libraries;
        private List<(string path, string comment)> resources;
        private List<(string path, List<string> args, string comment)> variables;

        private List<KeywordSection> keywordSections;
        private List<TestCaseSection> testCaseSections;
        private List<VariableSection> variableSections;

        #region 属性
        public string Documentation {
            get {
                return this.documentation;
            }

            set {
                this.documentation = value;
            }
        }

        public (List<string> value, string comment) SuiteSetup {
            get {
                return this.suiteSetup;
            }

            set {
                this.suiteSetup = value;
            }
        }

        public (List<string> value, string comment) SuiteTeardown {
            get {
                return this.suiteTeardown;
            }

            set {
                this.suiteTeardown = value;
            }
        }

        public (List<string> value, string comment) TestSetup {
            get {
                return this.testSetup;
            }

            set {
                this.testSetup = value;
            }
        }

        public (List<string> value, string comment) TestTeardown {
            get {
                return this.testTeardown;
            }

            set {
                this.testTeardown = value;
            }
        }

        public (string value, string message, string comment) TestTimeout {
            get {
                return this.testTimeout;
            }

            set {
                this.testTimeout = value;
            }
        }

        public (string value, string comment) TestTemplate {
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

        public List<(string name, string value, string comment)> Metadatas {
            get {
                return this.metadatas;
            }

            set {
                this.metadatas = value;
            }
        }

        public List<(string path, List<string> args, string alias, string comment)> Libraries {
            get {
                return this.libraries;
            }

            set {
                this.libraries = value;
            }
        }

        public List<(string path, string comment)> Resources {
            get {
                return this.resources;
            }

            set {
                this.resources = value;
            }
        }

        public List<(string path, List<string> args, string comment)> Variables {
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
        #endregion

    }
}
