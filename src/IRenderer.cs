using System.Data;

namespace Lski.Data.Profiler
{
    public interface IRenderer
    {
        string Render(IDbCommand command);

        string Render(IDbDataParameter param);
    }
}