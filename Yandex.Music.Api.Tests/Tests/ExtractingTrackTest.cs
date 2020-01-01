using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using Yandex.Music.Api.Models;
using Yandex.Music.Api.Tests.Enviromental;
using Yandex.Music.Api.Tests.Traits;

namespace Yandex.Music.Api.Tests.Tests
{
  [Collection("Yandex Test Harness")]
  public class ExtractingTrackTest : YandexTest
  {
    public const string FolderData = "data";
    public readonly string PathFile;
    public YandexTrack Track { get; set; }
    
    public ExtractingTrackTest(YandexTestHarness fixture, ITestOutputHelper output) : base(fixture, output)
    {
      if (!Directory.Exists(FolderData))
        Directory.CreateDirectory(FolderData);

      Track = Api.GetListFavorites().FirstOrDefault();
      PathFile = $"{FolderData}/{Track.Title}.mp3";
    }

    [Fact, YandexTrait(TraitGroup.ExtractTrack)]
    public void ExtractingTrack_DownloadFile_OneTrack()
    {
      var isDownloaded = Api.ExtractTrackToFile(Track, FolderData);
      isDownloaded.Should().BeTrue();

      var isFileExist = File.Exists(PathFile);
      isFileExist.Should().BeTrue();

      int? fileSize = 0;
      using (var stream = File.Open(PathFile, FileMode.Open))
      {
        var stLength = stream.Length;
        var buffer = new byte[stLength];
        stream.Read(buffer, 0, (int) stLength);
        fileSize = buffer.Length;
      }

      fileSize.Should().BeGreaterOrEqualTo(Track.FileSize.Value);
      
      File.Delete(PathFile);
    }

    [Fact, YandexTrait(TraitGroup.ExtractTrack)]
    public void ExtractingTrack_ExtractByteData_ReturnByteTrack()
    {
      var byteData = Api.ExtractDataTrack(Track);
      byteData.Length.Should().BeGreaterOrEqualTo(Track.FileSize.Value);
    }

    [Fact, YandexTrait(TraitGroup.ExtractTrack, TraitGroup.SearchTracks)]
    public void SearchTrack_ExtractByteData_ReturnByteTrack()
    {
      var track = Api.SearchTrack("master of puppets");
      var byteData = Api.ExtractDataTrack(track.First());
      byteData.Length.Should().BeGreaterOrEqualTo(Track.FileSize.Value);
    }

        [Fact, YandexTrait(TraitGroup.ExtractTrack)]
    public void ExtractingTrack_ExtractStream_ReturnStream()
    {
      var stream = Api.ExtractStreamTrack(Track);
      var isComplated = false;
      var fileSizeFromStream = 0;

      stream.Complated += (sender, track) =>
      {
        var stLength = stream.Length;
        var buffer = new byte[stLength];
        stream.Read(buffer, 0, (int) stLength);
        fileSizeFromStream = buffer.Length;


        isComplated = true;
      };

      Task.WaitAll(stream.Task);
      stream.Length.Should().NotBe(0);
      isComplated.Should().BeTrue();
      fileSizeFromStream.Should().BeGreaterOrEqualTo(Track.FileSize.Value);
    }
  }
}