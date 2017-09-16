using System;
using System.Data;
using System.Data.Common;

namespace Lski.Data.Profiler
{
    public class SimpleProfiler : IDbProfiler
    {
        private IRenderer _renderer;
        private Action<string> _log;

        public SimpleProfiler(Action<string> log)
        {
            _log = log;
            _renderer = new Renderer();
        }

        public bool IsActive { get; set; } = true;

        public void ExecuteFinish(IDbCommand profiledDbCommand, SqlExecuteType executeType, DbDataReader reader)
        {
            if (IsActive)
            {
                _log("Finished " + _renderer.Render(profiledDbCommand));
            }
        }

        public void OnError(IDbCommand profiledDbCommand, SqlExecuteType executeType, Exception exception)
        {
            if (IsActive)
            {
                _log("ERROR: " + _renderer.Render(profiledDbCommand));
            }
        }

        public void ExecuteStart(IDbCommand profiledDbCommand, SqlExecuteType executeType)
        {
            // TODO: Add timings
        }

        public void ReaderFinish(IDataReader reader)
        {
            // TODO: Implement
        }
    }
}