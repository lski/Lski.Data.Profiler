using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Lski.Data.Profiler
{
    /// <summary>
    /// Renders sql to a string
    /// </summary>
    public class Renderer : IRenderer
    {
        const string _indent = "    ";

        /// <summary>
        /// Renders the sql query to a string
        /// </summary>
        public string Render(IDbCommand command)
        {
            var sb = new StringBuilder("Query:")
                .AppendLine()
                .Append(_indent)
                .AppendLine(command.CommandText);

            if (command.Parameters.Count > 0)
            {

                sb.AppendLine()
                  .AppendLine("Parameters:");

                foreach (IDbDataParameter p in command.Parameters)
                {

                    sb.Append(_indent).AppendLine(Render(p));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Renders a sql parameter to a string, with its type
        /// </summary>
        public string Render(IDbDataParameter param)
        {
            return $"@{param.ParameterName} ({param.DbType}): {param.Value}";
        }

        /// <summary>
        /// Renders an exception in a slighly easier to read format
        /// </summary>
        public string Render(Exception ex) => Render(new StringBuilder(), ex).ToString();

        private StringBuilder Render(StringBuilder sb, Exception ex, byte level = 0)
        {
            var indent = _indent.Repeat(level);

            sb.Append(indent)
                .Append(ex.GetType().Name)
                .Append(": ")
                .AppendLine(ex.Message);

            return ex.InnerException != null ? Render(sb, ex.InnerException, ++level) : sb;
        }
    }

    internal static class Extensions
    {
        internal static string Repeat(this string str, byte times = 0)
        {
            if (times < 1)
            {
                return String.Empty;
            }

            if (times < 2)
            {
                return str;
            }

            var sb = new StringBuilder();

            for (int i = 0; i < times; i++)
            {
                sb.Append(str);
            }

            return sb.ToString();
        }
    }
}