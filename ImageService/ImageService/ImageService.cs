using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Configuration;

public enum ServiceState
{
    SERVICE_STOPPED = 0x00000001,
    SERVICE_START_PENDING = 0x00000002,
    SERVICE_STOP_PENDING = 0x00000003,
    SERVICE_RUNNING = 0x00000004,
    SERVICE_CONTINUE_PENDING = 0x00000005,
    SERVICE_PAUSE_PENDING = 0x00000006,
    SERVICE_PAUSED = 0x00000007,
}

[StructLayout(LayoutKind.Sequential)]
public struct ServiceStatus
{
    public int dwServiceType;
    public ServiceState dwCurrentState;
    public int dwControlsAccepted;
    public int dwWin32ExitCode;
    public int dwServiceSpecificExitCode;
    public int dwCheckPoint;
    public int dwWaitHint;
};

namespace ImageService
{
    public partial class ImageService : ServiceBase
    {

        private int eventId = 1;
        private ILoggingModel m_logging;
        private IImageController c_controller;
        private IImageModel m_imageService;
        private ImageServer server;

        public ImageService(string[] args)
        {
                InitializeComponent();

                string eventSourceName = ConfigurationManager.AppSettings.Get("SourceName");
                string logName = ConfigurationManager.AppSettings.Get("LogName");

                eventLog1 = new System.Diagnostics.EventLog();
                if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
                {
                    System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
                }
                eventLog1.Source = eventSourceName;
                eventLog1.Log = logName;

                eventLog1.WriteEntry("start constructors of all members");
                m_logging = new LoggingModel();
                m_logging.MessageRecieved += OnMsg;
                m_imageService = new ImageModel();
                c_controller = new ImageController(m_imageService);
                server = new ImageServer(c_controller, m_logging);
        }

        protected override void OnStart(string[] args)
        {

            eventLog1.WriteEntry("on start begin");


            // Update the service state to Start Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog1.WriteEntry("In OnStart");

            // Set up a timer to trigger every minute.  
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds  
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();

            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }
        private void OnMsg(object src, MessageRecievedEventArgs mra)
        {
            if(mra.Status == MessageTypeEnum.FAIL)
            {
                eventLog1.WriteEntry(mra.Message,EventLogEntryType.Error);
            } else if (mra.Status == MessageTypeEnum.INFO)
            {
                eventLog1.WriteEntry(mra.Message, EventLogEntryType.Information);
            } else
            {
                eventLog1.WriteEntry(mra.Message, EventLogEntryType.Warning);
            }

            Console.WriteLine("write to log");

        }

        protected override void OnStop()
        {
            server.OnCloseServer();
            eventLog1.WriteEntry("In OnStop");
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.  
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);

        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
    }
}
