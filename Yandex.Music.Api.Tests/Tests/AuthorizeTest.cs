﻿using FluentAssertions;
using System;
using Xunit;
using Xunit.Abstractions;
using Yandex.Music.Api.Tests.Enviromental;
using Yandex.Music.Api.Tests.Traits;

namespace Yandex.Music.Api.Tests.Tests
{
  [Collection("Yandex Test Harness")]
  public class AuthorizeTest : YandexTest
  {
    public AuthorizeTest(YandexTestHarness fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact, YandexTrait(TraitGroup.Authorize)]
    public void Authorize_ValidData_GenerateTrue()
    {
      var (username, password) = Credentials.GetCredentials();
      var isAuthorized = Api.Authorize(username, password);

      isAuthorized.Should().BeTrue();
    }
    
    [Fact, YandexTrait(TraitGroup.Authorize)]
    public void Authorize_InvalidData_GenerateFalse()
    {
      var isAuthorized = Api.Authorize("login", "123");
      
      isAuthorized.Should().BeFalse();
    }
  }
}