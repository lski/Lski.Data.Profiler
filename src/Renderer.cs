using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Lski.Data.Profiler
{
    public class Renderer : IRenderer
    {
        const string _indent = "    ";

        public string Render(IDbCommand command)
        {
            var sb = new StringBuilder("Query:")
                .AppendLine()
                .Append(_indent)
                .AppendLine(command.CommandText);

            if (command.Parameters.Count > 0) {

                sb.AppendLine()
                  .AppendLine("Parameters:");

                foreach (IDbDataParameter p in command.Parameters)
                {

                    sb.Append(_indent).AppendLine(Render(p));
                }
            }

            return sb.ToString();
        }

        public string Render(IDbDataParameter param)
        {
            return $"@{param.ParameterName} ({param.DbType}): {param.Value}";
        }
    }
}