using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Util.Services.Configuration;

namespace PocketMoney.Util.Services
{
    public abstract partial class Service : ServiceBase
    {
        protected readonly IServiceConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        protected Service(IServiceConfiguration configuration)
        {
            InitializeComponent();
            this.configuration = configuration;
            ServiceName = configuration.ServiceName;
        }


        /// <summary>
        /// Gets the execuptablepath.
        /// </summary>
        /// <value>The application dir path.</value>
        public static string ExecutablePath
        {
            get { return Type.GetType("PocketMoney.Service.Host.Program, PocketMoney.Service.Host", true, true).Assembly.Location; }
        }

        /// <summary>
        /// Gets the application dir path.
        /// </summary>
        /// <value>The application dir path.</value>
        public static string ApplicationDirPath
        {
            get { return Path.GetDirectoryName(ExecutablePath); }
        }

        /// <summary>
        /// Gets the log path.
        /// </summary>
        /// <value>The log path.</value>
        public string LogPath
        {
            get { return Path.Combine(ApplicationDirPath, configuration.DisplayName + ".log"); }
        }


        public void Start()
        {
            OnStart(new string[0]);
        }


        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override sealed void OnStart(string[] args)
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
                string version =
                    ((AssemblyFileVersionAttribute)
                     (typeof (Service).Assembly.GetCustomAttributes(typeof (AssemblyFileVersionAttribute), true)[0]))
                        .Version;

#if TRACE
                Trace.WriteLine("Starting service '" + configuration.DisplayName + "' version " + version + ".");
#endif
                base.OnStart(args);
                DoStart(args);
                Trace.WriteLine("Ready.");
            }
            catch (Exception e)
            {
                e.LogError();
                throw;
            }
        }

        protected abstract void DoStart(string[] args);

        // readonly List<IServiceProcess> PluginList = new List<IServiceProcess>();

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override sealed void OnStop()
        {
            Trace.WriteLine("Stopping host");
            DoStop();
            Trace.WriteLine("Stopped");
        }

        protected abstract void DoStop();


        /// <summary>
        /// Called when unhandled exception is occured.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Trace.WriteLine("============Unhandled exception:=========\r\n" + e);
        }

        public abstract IBuildManager GetBuildManager();
    }
}