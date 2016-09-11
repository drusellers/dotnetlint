using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnetlint
{
    public interface Source
    {
        Task<IEnumerable<TextAndPath>> Get();
    }
}