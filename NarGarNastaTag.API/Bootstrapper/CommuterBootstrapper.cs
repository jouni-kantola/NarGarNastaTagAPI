using System.Collections.Generic;
using System.Reflection;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using NarGarNastaTag.API.Contract;

namespace NarGarNastaTag.API.Bootstrapper
{
    public class CommuterBootstrapper : DefaultNancyBootstrapper
    {
        private readonly IRootPathProvider _rootPathProvider;

        public CommuterBootstrapper()
            : this(new CommuterRootPathProvider())
        { }

        public CommuterBootstrapper(IRootPathProvider rootPathProvider)
        {
            _rootPathProvider = rootPathProvider;
        }

        protected override void ConfigureRequestContainer(Nancy.TinyIoc.TinyIoCContainer container, NancyContext context)
        {
            container.AutoRegister(new List<Assembly>
                {
                    typeof (ITrainRoute).Assembly
                }, false);
            base.ConfigureRequestContainer(container, context);
        }

        protected override IRootPathProvider RootPathProvider
        {
            get { return _rootPathProvider; }
        }
        internal new NancyConventions Conventions
        {
            get
            {
                return base.Conventions;
            }
        }

        internal new Nancy.TinyIoc.TinyIoCContainer GetApplicationContainer()
        {
            return base.GetApplicationContainer();
        }

        internal new NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return base.InternalConfiguration;
            }
        }
    }
}