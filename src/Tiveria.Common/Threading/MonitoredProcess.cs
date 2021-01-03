using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Tiveria.Common.Threading
{
    public class MonitoredProcess
    {
        private Process _Process;
        private readonly System.Timers.Timer _Timer;
        private int _NoRespondCounter;

        public event EventHandler Crashed;
        #region OnProcessCrashed
        /// <summary>
        /// Triggers the ProcessCrashed event.
        /// </summary>
        protected virtual void OnCrashed()
        {
            Crashed?.Invoke(this, EventArgs.Empty);
        }
        #endregion
        public event EventHandler Exited;
        #region OnExited
        /// <summary>
        /// Triggers the Exited event.
        /// </summary>
        protected virtual void OnExited(EventArgs ea)
        {
            Exited?.Invoke(this, ea);
        }
        #endregion

        public string FileName { get; private set; }
        public string Arguments { get; private set; }
        public int CheckInterval { get; private set; }
        public int NoResponseTreshold { get; private set; }
        public bool HasExited { get { return (_Process != null) && _Process.HasExited; } }
        public bool IsRunning { get { return (_Process != null) && !_Process.HasExited; } }
        public int Id { get { return (_Process != null) ? _Process.Id : -1; } }

        public MonitoredProcess(string filename, string arguments, int checkInterval = 10, int noResponseTreshold = 3)
        {
            FileName = filename;
            Arguments = arguments;
            _Process = null;

            CheckInterval = (checkInterval > 0) ? checkInterval : 10;
            NoResponseTreshold = (noResponseTreshold >= 0) ? noResponseTreshold : 3;

            _Timer = new System.Timers.Timer(CheckInterval * 1000);
            _Timer.Elapsed += TimerElapsedHandler;
        }

        public bool Start()
        {
            if (!System.IO.File.Exists(FileName))
                return false;

            if ((_Process != null) && !_Process.HasExited)
                return false;

            try
            {
                _NoRespondCounter = 0;
                _Process = Process.Start(
                        new ProcessStartInfo()
                        {
                            FileName = FileName,
                            Arguments = Arguments,
                            WindowStyle = ProcessWindowStyle.Normal
                        });
                _Process.EnableRaisingEvents = true;
                _Process.Exited += ProcessExitedHandler;
                StartChecking();
                return true;
            }
            catch
            {
                return false;
            }
        }

        void ProcessExitedHandler(object sender, EventArgs e)
        {
            StopChecking();
            _Process = null;
            OnExited(e);
        }

        public void Kill()
        {
            StopChecking();
            if ((!HasExited) && (_Process != null))
                _Process.Kill();
        }

        private void StartChecking()
        {
            _Timer.Start();
        }

        private void StopChecking()
        {
            _Timer.Stop();
        }

        void TimerElapsedHandler(object sender, System.Timers.ElapsedEventArgs e)
        {
            if ((_Process != null) && (_Process.Responding))
                _NoRespondCounter = 0;
            else
                _NoRespondCounter++;

            if (_NoRespondCounter > NoResponseTreshold)
            {
                OnCrashed();
                Kill();
            }
        }
    }
}
