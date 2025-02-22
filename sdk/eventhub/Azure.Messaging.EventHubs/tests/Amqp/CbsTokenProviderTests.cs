﻿using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Messaging.EventHubs.Amqp;
using Azure.Messaging.EventHubs.Authorization;
using Moq;
using NUnit.Framework;

namespace Azure.Messaging.EventHubs.Tests.Amqp
{
    /// <summary>
    ///   The suite of tests for the <see cref="CbsTokenProvider" />
    ///   class.
    /// </summary>
    ///
    [TestFixture]
    public class CbsTokenProviderTests
    {
        /// <summary>
        ///   Verifies functionality of the constructor.
        /// </summary>
        ///
        [Test]
        public void ConstructorValidatesCredential()
        {
            Assert.That(() => new CbsTokenProvider(null, CancellationToken.None), Throws.ArgumentNullException);
        }

        /// <summary>
        ///   Verifies functionality of the <see cref="CbsTokenProvider.GetTokenAsync" />
        ///   method.
        /// </summary>
        ///
        [Test]
        public async Task GetTokenAsyncPassesAlongTheClaims()
        {
            var requiredClaims = new[] { "one", "two" };
            var mockCredential = new Mock<TokenCredential>();
            var credential = new EventHubTokenCredential(mockCredential.Object, "test");
            var provider = new CbsTokenProvider(credential, default);

            mockCredential
                .Setup(credential => credential.GetTokenAsync(It.Is<string[]>(value => value == requiredClaims), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new AccessToken("blah", DateTimeOffset.Parse("2015-10-27T00:00:00Z"))))
                .Verifiable();

            await provider.GetTokenAsync(new Uri("http://www.here.com"), "nobody", requiredClaims);
            mockCredential.Verify();
        }

        /// <summary>
        ///   Verifies functionality of the <see cref="CbsTokenProvider.GetTokenAsync" />
        ///   method.
        /// </summary>
        ///
        [Test]
        public async Task GetTokenAsyncPopulatesUsingTheCredentialAccessToken()
        {
            var tokenValue = "ValuE_oF_tHE_tokEn";
            var expires = DateTimeOffset.Parse("2015-10-27T00:00:00Z");
            var mockCredential = new Mock<TokenCredential>();
            var credential = new EventHubTokenCredential(mockCredential.Object, "test");
            var provider = new CbsTokenProvider(credential, default);

            mockCredential
                .Setup(credential => credential.GetTokenAsync(It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new AccessToken(tokenValue, expires)));

            var cbsToken = await provider.GetTokenAsync(new Uri("http://www.here.com"), "nobody", new string[0]);

            Assert.That(cbsToken, Is.Not.Null, "The token should have been produced");
            Assert.That(cbsToken.TokenValue, Is.EqualTo(tokenValue), "The token value should match");
            Assert.That(cbsToken.ExpiresAtUtc, Is.EqualTo(expires.DateTime), "The token expiration should match");
        }

        /// <summary>
        ///   Verifies functionality of the <see cref="CbsTokenProvider.GetTokenAsync" />
        ///   method.
        /// </summary>
        ///
        [Test]
        public async Task GetTokenAsyncSetsTheCorrectTypeForSharedAccessSignatureTokens()
        {
            var value = "TOkEn!";
            var signature = new SharedAccessSignature(String.Empty, "keyName", "key", value, DateTimeOffset.Parse("2015-10-27T00:00:00Z"));
            var sasCredential = new SharedAccessSignatureCredential(signature);
            var credential = new EventHubTokenCredential(sasCredential, "test");
            var provider = new CbsTokenProvider(credential, default);
            var cbsToken = await provider.GetTokenAsync(new Uri("http://www.here.com"), "nobody", new string[0]);

            Assert.That(cbsToken, Is.Not.Null, "The token should have been produced");
            Assert.That(cbsToken.TokenType, Is.EqualTo(GetSharedAccessTokenType()), "The token type should match");
        }

        /// <summary>
        ///   Verifies functionality of the <see cref="CbsTokenProvider.GetTokenAsync" />
        ///   method.
        /// </summary>
        ///
        [Test]
        public async Task GetTokenAsyncSetsTheCorrectTypeForOtherTokens()
        {
            var tokenValue = "ValuE_oF_tHE_tokEn";
            var expires = DateTimeOffset.Parse("2015-10-27T00:00:00Z");
            var mockCredential = new Mock<TokenCredential>();
            var credential = new EventHubTokenCredential(mockCredential.Object, "test");
            var provider = new CbsTokenProvider(credential, default);

            mockCredential
                .Setup(credential => credential.GetTokenAsync(It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new AccessToken(tokenValue, expires)));

            var cbsToken = await provider.GetTokenAsync(new Uri("http://www.here.com"), "nobody", new string[0]);

            Assert.That(cbsToken, Is.Not.Null, "The token should have been produced");
            Assert.That(cbsToken.TokenType, Is.EqualTo(GetGenericTokenType()), "The token type should match");
        }

        /// <summary>
        ///   Gets the token type used for SAS-based credentials, using the
        ///   private field of the provider.
        /// </summary>
        ///
        private static string GetSharedAccessTokenType() =>
                    (string)typeof(CbsTokenProvider)
                        .GetField("SharedAccessSignatureTokenType", BindingFlags.Static | BindingFlags.NonPublic)
                        .GetValue(null);

        /// <summary>
        ///   Gets the token type used for generic token-based credentials, using the
        ///   private field of the provider.
        /// </summary>
        ///
        private static string GetGenericTokenType() =>
                    (string)typeof(CbsTokenProvider)
                        .GetField("JsonWebTokenType", BindingFlags.Static | BindingFlags.NonPublic)
                        .GetValue(null);
    }
}
