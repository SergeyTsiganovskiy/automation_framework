using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using aautomation_framework.Configuration.V2;
using aautomation_framework.Utility;
using NUnit.Framework;

namespace aautomation_framework.Configuration.V3
{
    public class PlatformTestConfiguration
    {
        #region singleton
        private static PlatformTestConfiguration instance;

        public static PlatformTestConfiguration Instance
        {
            get
            {
                if (instance == null)
                    instance = new PlatformTestConfiguration();
                return instance;
            }
        }

        private PlatformTestConfiguration()
        {
            SetSystemConfig();
            SetExecHost();
        }
        #endregion singleton

        #region choose_system

        public SystemConfiguration SysConfig
        {
            get
            {
                return CommonTestingConfiguration.Instance.commonVariables.SystemConfigurationsObj[SysConfigIdx];
            }
        }

        public ExecutionEnvironment ExecConfig
        {
            get
            {
                return CommonTestingConfiguration.Instance.commonVariables.ExecutionEnvironmentsObj[ExecConfigIdx];
            }
        }

        #region set_config
        private int SysConfigIdx = 0;
        public void SetSystemConfig()
        {
            LogUtil.log.Info("Enter SetConfig()");

            bool ignoreCase = true;

            string testStack = Environment.GetEnvironmentVariable("TestStack");
            if (testStack == null)
            {
                testStack = PlatformJSONConfig.Instance.PLTConfigData.Configurations.TestStack;
            }

            LogUtil.log.Info("Environment['TestStack'] = " + testStack);
            bool find = false;
            for (int j = 0; j < CommonTestingConfiguration.Instance.commonVariables.SystemConfigurationsObj.Count; j++)
            {
                LogUtil.log.Info("Matching with Test Stack Name " + CommonTestingConfiguration.Instance.commonVariables.SystemConfigurationsObj[j].Name);
                if (string.Compare(CommonTestingConfiguration.Instance.commonVariables.SystemConfigurationsObj[j].Name, testStack, ignoreCase) == 0)
                {
                    LogUtil.log.Info("Find Test Stack (Name = " + testStack + " ) matched");
                    SysConfigIdx = j;
                    find = true;
                    break;
                }
            }
            if (!find)
                LogUtil.log.Error("Can't Find Test Stack named with " + testStack + ". Use Test Stack " + CommonTestingConfiguration.Instance.commonVariables.SystemConfigurationsObj[0].Name + " as default");

        }

        private int ExecConfigIdx = 0;
        public void SetExecHost()
        {
            LogUtil.log.Info("Enter SetExecHost()");

            bool ignoreCase = true;

            string execHost = Environment.GetEnvironmentVariable("ExecutionHost");

            if (execHost == null)
            {
                execHost = PlatformJSONConfig.Instance.PLTConfigData.Configurations.ExecutionHost;
            }

            LogUtil.log.Info("Environment set ExecutionHost " + execHost);
            bool find = false;
            for (int j = 0; j < CommonTestingConfiguration.Instance.commonVariables.ExecutionEnvironmentsObj.Count; j++)
            {
                if (string.Compare(CommonTestingConfiguration.Instance.commonVariables.ExecutionEnvironmentsObj[j].Name, execHost, ignoreCase) == 0)
                {
                    LogUtil.log.Info("Find Envirnonment Configuration for host " + execHost);
                    ExecConfigIdx = j;
                    find = true;
                    break;
                }
            }
            if (!find)
                LogUtil.log.Error("Can't Find System Configuration for Test Stack " + execHost + ". Use Host " + CommonTestingConfiguration.Instance.commonVariables.ExecutionEnvironmentsObj[0].Name + " as default");
        }
        #endregion set_config

        #endregion choose_system

        /*******************************************************
         * Not-configurable Data
         * *****************************************************/
        public string baseFilePath = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\.."));
        public string jmxScriptPath
        {
            get { return Path.Combine(this.baseFilePath, @"TestCases\Performance\jmeter_scripts"); }
        }
        public string dataFilePath
        {
            get { return Path.Combine(this.baseFilePath, @"LfsUploadFiles"); }
        }
        /*******************************************************
         * Execution Environment Data
         * *****************************************************/
        public string jMeterBinPath
        {
            get { return this.ExecConfig.JMeterConfigObj.JMeterExecutablePath; }
        }
        public string tmpFilePath
        {
            get { return this.ExecConfig.TempPath; }
        }
        public string baseResultPath
        {
            get { return this.ExecConfig.LogFilePath; }
        }
        private string testResultPath;
        public string ResultPath
        {
            get { return this.testResultPath; }
            set { testResultPath = value; }
        }
        public List<string> Client_list
        {
            get { return PlatformJSONConfig.Instance.PLTConfigData.Configurations.Clients; }
        }
        public List<string> Users_list
        {
            get { return PlatformJSONConfig.Instance.PLTConfigData.Configurations.Internal_Users; }
        }
        public List<string> External_Users_list
        {
            get { return PlatformJSONConfig.Instance.PLTConfigData.Configurations.External_Users; }
        }
        /*******************************************************
        * System Configuration Data
        * *****************************************************/
        public string URL
        {
            get { return this.SysConfig.Url; }
        }
        public string Protocol
        {
            get { char[] seperator = { ':' }; return (URL.Split(seperator))[0]; }
        }
        public string ApiPort
        {
            get { return this.SysConfig.ApiPort; }
        }
        /*******************************************************
        * VM hosts.
        * *****************************************************/
        public string SshUser
        {
            get { return this.SysConfig.SshUser; }
        }
        public string SshPassword
        {
            get { return this.SysConfig.SshPassword; }
        }
        public string Gateway1
        {
            get { return this.SysConfig.Gateway1; }
        }
        public string CassandraCluster1
        {
            get { return this.SysConfig.CassCluster1; }
        }
        public string CassandraCluster2
        {
            get { return this.SysConfig.CassCluster2; }
        }
        public string CassandraCluster3
        {
            get { return this.SysConfig.CassCluster3; }
        }
    }
}

