﻿// Generated by xsd.exe
namespace ConnectFour.Tests.Automation
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class ConsoleTestScenarios
    {

        private ConsoleTestScenariosConsoleTestScenario[] consoleTestScenarioField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ConsoleTestScenario")]
        public ConsoleTestScenariosConsoleTestScenario[] ConsoleTestScenario
        {
            get
            {
                return consoleTestScenarioField;
            }
            set
            {
                consoleTestScenarioField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ConsoleTestScenariosConsoleTestScenario
    {

        private object[] itemsField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ExpectStandardOutToPrint", typeof(ConsoleTestScenariosConsoleTestScenarioExpectStandardOutToPrint))]
        [System.Xml.Serialization.XmlElementAttribute("WriteLineToStandardInput", typeof(ConsoleTestScenariosConsoleTestScenarioWriteLineToStandardInput))]
        public object[] Items
        {
            get
            {
                return itemsField;
            }
            set
            {
                itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        public override string ToString()
        {
            return nameField;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ConsoleTestScenariosConsoleTestScenarioExpectStandardOutToPrint
    {

        private bool stdInLineField;

        private string valueField;
  
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool StdInLine
        {
            get
            {
                return stdInLineField;
            }
            set
            {
                stdInLineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ConsoleTestScenariosConsoleTestScenarioWriteLineToStandardInput
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }
}