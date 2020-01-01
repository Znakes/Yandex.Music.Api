using Serilog;
using Serilog.Events;
using Xunit;
using Xunit.Abstractions;

namespace Yandex.Music.Api.Tests
{
  [CollectionDefinition("Yandex Test Harness")]
  public class YandexTestCollection : ICollectionFixture<YandexTestHarness>
  {
  }

  public class YandexTest 
  {
    public YandexApi Api => this.Fixture.Api;
    public YandexTestHarness Fixture { get; set; }

    public YandexTest(YandexTestHarness fixture, ITestOutputHelper output = null)
    {
      Fixture = fixture;
      
      if (output != null)
      {
        Log.Logger = new LoggerConfiguration()
          .WriteTo
          .TestOutput(output, LogEventLevel.Verbose)
          .CreateLogger();
      }
    }
  }
}
