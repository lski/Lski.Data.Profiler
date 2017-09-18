using System;
using System.Data;
using System.Data.Common;

namespace Lski.Data.Profiler
{
    /// <summary>
    /// Simple database profile, that accepts a func it can log queries too.
    /// </summary>
    public class SimpleProfiler : IDbProfiler
    {
        private IRenderer _renderer;
        private Action<string> _log;

        /// <summary>
        /// Create new profiler
        /// </summary>
        /// <param name="log">The logger to output the queries too</param>
        public SimpleProfiler(Action<string> log) : this(log, new Renderer())
        {
        }

        /// <summary>
        /// Create new profiler
        /// </summary>
        /// <param name="log">The logger to output the queries too</param>
        /// <param name="renderer">The renderer to display parts of the query</param>
        public SimpleProfiler(Action<string> log, IRenderer renderer)
        {
            _log = log;
            _renderer = renderer;
        }

        /// <summary>
        /// Gets a value indicating whether or not the profiler instance is active
        /// </summary>
        public bool IsActive { get; set; } = true;


        /// <summary>
        /// Called when a reader finishes executing, currently where logging occurs
        /// </summary>
        /// <param name="profiledDbCommand">The profiled DB Command.</param>
        /// <param name="executeType">The execute Type.</param>
        /// <param name="reader">The reader.</param>
        public void ExecuteFinish(IDbCommand profiledDbCommand, SqlExecuteType executeType, DbDataReader reader)
        {
            if (IsActive)
            {
                _log("Finished " + _renderer.Render(profiledDbCommand));
            }
        }

        /// <summary>
        /// Called when an error happens during execution of a command
        /// </summary>
        /// <param name="profiledDbCommand">The profiled DB Command.</param>
        /// <param name="executeType">The execute Type.</param>
        /// <param name="exception">The exception.</param>
        public void OnError(IDbCommand profiledDbCommand, SqlExecuteType executeType, Exception exception)
        {
            if (IsActive)
            {
                _log(_renderer.Render(exception));
                _log(_renderer.Render(profiledDbCommand));
            }
        }

        /// <summary>
        /// Called when a command starts executing
        /// </summary>
        /// <param name="profiledDbCommand">The profiled dB Command.</param>
        /// <param name="executeType">The execute Type.</param>

        public void ExecuteStart(IDbCommand profiledDbCommand, SqlExecuteType executeType)
        {
            // TODO: Add timings
        }

        /// <summary>
        /// Called when a reader is done iterating through the data
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void ReaderFinish(IDataReader reader)
        {
            // TODO: Implement
        }
    }
}