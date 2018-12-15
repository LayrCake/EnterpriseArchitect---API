using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailClient.Tests
{
    [TestClass]
    public abstract class Hosted_BaseActionServiceSetup
    {

        private Process _iisProcess;

        [TestInitialize]
        public void Setup()
        {
            var thread = new Thread(StartIisExpress) {IsBackground = true};

            thread.Start();
        }

        private void StartIisExpress()
        {
            var startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    ErrorDialog = true,
                    LoadUserProfile = true,
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    Arguments = string.Format("/path:\"{0}\" /port:{1}", "localhost", "5560")
                };

            var programfiles = string.IsNullOrEmpty(startInfo.EnvironmentVariables["programfiles"])
                                ? startInfo.EnvironmentVariables["programfiles(x86)"]
                                : startInfo.EnvironmentVariables["programfiles"];

            startInfo.FileName = programfiles + "\\IIS Express\\iisexpress.exe";

            try
            {
                _iisProcess = new Process { StartInfo = startInfo };

                _iisProcess.Start();
                _iisProcess.WaitForExit();
            }
            catch
            {
                _iisProcess.CloseMainWindow();
                _iisProcess.Dispose();
            }
        }

        [TestCleanup]
        public void Teardown()
        {
            _iisProcess.CloseMainWindow();
            _iisProcess.Dispose();
        }
    }
}