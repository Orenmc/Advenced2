using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication2.Communication;

namespace WebApplication2.Models
{
    public class Configuration
    {
        public delegate void SomeThingWasChanged();
        public event SomeThingWasChanged Changed;
        private static Communication.Client configClient;
        public Configuration()
        {
            configClient = Client.GetInstance;
            configClient.CommandRecieved += this.OnUpdate;

            OutputDir = string.Empty;
            SourceName = string.Empty;
            LogName = string.Empty;
            ThumbS = 0;

            HandlerList = new ObservableCollection<string>();
            CommandRecievedEventArgs commandToSent = new CommandRecievedEventArgs((int)CommandStateEnum.GET_APP_CONFIG, new string[5], "");
            configClient.SendCommandToServer(commandToSent);
        }

        private void OnUpdate(object src, CommandRecievedEventArgs e)
        {
            if (e.CommandID == (int)CommandStateEnum.GET_APP_CONFIG)
            {
                NewConfiguration(e);
            }
            else if (e.CommandID == (int)CommandStateEnum.CLOSE_HANDLER)
            {
                HandlerList.Remove(e.Args[0]);
            }
            Changed?.Invoke();

        }

        private void NewConfiguration(CommandRecievedEventArgs e)
        {
            OutputDir = e.Args[0];
            SourceName = e.Args[1];
            LogName = e.Args[2];
            ThumbS = Int32.Parse(e.Args[3]);
            string[] handler = e.Args[4].Split(';');
            if (handler[0] != "")
            {
                foreach (string handle in handler)
                {
                    if (!(HandlerList.Contains(handle)))
                        HandlerList.Add(handle);
                }
            }
        }

        [Required]
        [Display(Name = "Output Directory")]
        public String OutputDir { get; set; }

        [Required]
        [Display(Name = "Log Name")]
        public String LogName { get; set; }

        [Required]
        [Display(Name = "Source Name")]
        public String SourceName { get; set; }

        [Required]
        [Display(Name = "Thumbnail Size")]
        public int ThumbS { get; set; }

        [Required]
        [Display(Name = "Source Name")]
        public ObservableCollection<string> HandlerList { get; set; }
    }
}