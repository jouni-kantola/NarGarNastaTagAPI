using System;
using Nancy;

namespace NarGarNastaTag.API.Bootstrapper
{
    public class CommuterRootPathProvider : IRootPathProvider
    {
        public virtual string GetRootPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}