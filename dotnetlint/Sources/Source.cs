using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetlint.Sources
{
    public interface Source
    {
        Task<IEnumerable<TextAndPath>> Get();
    }
}
