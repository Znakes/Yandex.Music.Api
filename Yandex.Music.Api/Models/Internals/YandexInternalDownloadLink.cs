using System;

namespace Yandex.Music.Api.Models.Internals
{
    public class YandexInternalDownloadLink
    {
        public string Codec { get; set; }
        public int Bitrate { get; set; }
        public Uri Src { get; set; }
        public bool Gain { get; set; }
        public bool Preview { get; set; }
    }
}
