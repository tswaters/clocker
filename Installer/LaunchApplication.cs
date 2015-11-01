using System.Windows.Forms;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections;

namespace Installer
{
    [RunInstaller(true)]
    public partial class LaunchApplication : System.Configuration.Install.Installer
    {
        public LaunchApplication()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            if (Context.Parameters["Run"] == "1")
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Directory.SetCurrentDirectory(path);
                Process.Start(path + "\\Clocker.exe");
            }
        }

    }


}
