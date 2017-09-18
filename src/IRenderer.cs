using System;
using System.Data;

namespace Lski.Data.Profiler
{
    /// <summary>
    /// Renders sql to a string
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Renders the sql query to a string
        /// </summary>
        string Render(IDbCommand command);

        /// <summary>
        /// Renders a sql parameter to a string, with its type
        /// </summary>
        string Render(IDbDataParameter param);

        /// <summary>
        /// Renders an exception in a slighly easier to read format
        /// </summary>
        string Render(Exception ex);
    }
}