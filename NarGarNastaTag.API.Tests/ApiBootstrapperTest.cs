using Moq;
using Nancy;
using Nancy.Responses;
using Nancy.Serializers.Json.ServiceStack;
using Nancy.TinyIoc;
using NarGarNastaTag.API.Bootstrapper;
using System.Linq;
using NarGarNastaTag.API.Contract;
using Xunit;

namespace NarGarNastaTag.API.Tests
{
    public class ApiBootstrapperTest
    {
        [Fact]
        public void Should_use_custom_root_path_provider_when_when_initializing_bootstrapper()
        {
            var expectedHaveBeenCalled = false;
            // Given
            var rootPathProvider = new Mock<CommuterRootPathProvider>();
            rootPathProvider.Setup(r => r.GetRootPath()).Callback(() => { expectedHaveBeenCalled = true; });

            var bootstrapper = new CommuterBootstrapper(rootPathProvider.Object);

            // When
            bootstrapper.Initialise();

            // Then
            Assert.True(expectedHaveBeenCalled);
        }

        [Fact]
        public void Should_have_precedence_for_JSON_serialization_with_ServiceStackSerializer_when_initializing_bootstrapper()
        {
            // Given
            var rootPathProvider = Mock.Of<IRootPathProvider>();

            var bootstrapper = new CommuterBootstrapper(rootPathProvider);

            // When
            var serializers = bootstrapper.InternalConfiguration.Serializers.Select(s => s.UnderlyingSystemType).ToList();
            var indexOfServiceStackJsonSerializer = serializers.FindIndex(0, s => s.UnderlyingSystemType == typeof (ServiceStackJsonSerializer));
            var indexOfDefaultJsonSerializer = serializers.FindIndex(0, s => s.UnderlyingSystemType == typeof (DefaultJsonSerializer));

            // Then
            Assert.True(serializers.Count(s => s.UnderlyingSystemType == typeof (ServiceStackJsonSerializer)) > 0);
            Assert.True(indexOfServiceStackJsonSerializer < indexOfDefaultJsonSerializer);
        }

        [Fact]
        public void Should_not_register_entity_types_in_application_container_when_initializing_bootstrapper()
        {
            // Given
            var rootPathProvider = Mock.Of<IRootPathProvider>();

            var bootstrapper = new CommuterBootstrapper(rootPathProvider);

            // When
            bootstrapper.Initialise();
            
            // Then
            var applicationContainer = bootstrapper.GetApplicationContainer();
            Assert.Throws<TinyIoCResolutionException>(() => applicationContainer.Resolve<ITrainRoute>());
        }
    }
}
