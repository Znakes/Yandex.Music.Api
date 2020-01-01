using System;
using Yandex.Music.Api.Tests.Enviromental;

namespace Yandex.Music.Api.Tests
{
  public class YandexTestHarness : IDisposable
  {
        public YandexApi Api { get; set; }

        public YandexTestHarness()
        {
            if (Api == null)
            {
                Api = new YandexMusicApi();
                var (username, password) = Credentials.GetCredentials();
                Api.Authorize(username, password);
            }
        }

    public void Dispose()
    {
    }
  }
}