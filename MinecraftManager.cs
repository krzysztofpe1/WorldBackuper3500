using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WorldBackuper3500
{
    public class MinecraftManager
    {
        private string TmuxSessionName;
        private string MinecraftStartFile;
        public MinecraftManager(string _TmuxSessionName, string _MinecraftStartFile)
        {
            TmuxSessionName = _TmuxSessionName;
            MinecraftStartFile = _MinecraftStartFile;
        }
        public void stop()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                BashCommands.NoReturn("tmux", "send-keys -t " + TmuxSessionName + ".0 \"say Restart serwera za " + (10 - i) + "s\" ENTER");
            }
            BashCommands.NoReturn("tmux", "send-keys -t " + TmuxSessionName + ".0 \"stop\" ENTER");
        }
        public void start()
        {
            BashCommands.NoReturn("tmux", "send-keys -t "+ TmuxSessionName + ".0 \"bash "+ MinecraftStartFile + "\" ENTER");
        }
    }
}
