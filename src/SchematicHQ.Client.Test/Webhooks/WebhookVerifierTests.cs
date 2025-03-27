using System;
using System.Collections.Generic;
using NUnit.Framework;
using SchematicHQ.Client.Webhooks.WebhookUtils;

namespace SchematicHQ.Client.Test.Webhooks
{
    [TestFixture]
    public class WebhookVerifierTests
    {
        private const string WebhookSecret = "test_secret";
        private const string RequestBody = "{\"event\":\"test_event\"}";
        private const string Timestamp = "1234567890";

        private string _validSignature;

        [SetUp]
        public void SetUp()
        {
            _validSignature = WebhookVerifier.ComputeHexSignature(RequestBody, Timestamp, WebhookSecret);
        }

        [Test]
        public void ComputeHexSignature_ReturnsValidSignature()
        {
            // Act
            string signature = WebhookVerifier.ComputeHexSignature(RequestBody, Timestamp, WebhookSecret);

            // Assert
            Assert.That(signature, Is.Not.Null);
            Assert.That(signature, Is.Not.Empty);
        }

        [Test]
        public void VerifySignature_WithValidSignature_DoesNotThrow()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => WebhookVerifier.VerifySignature(RequestBody, _validSignature, Timestamp, WebhookSecret));
        }

        [Test]
        public void VerifySignature_WithInvalidSignature_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<WebhookSignatureException>(
                () => WebhookVerifier.VerifySignature(RequestBody, "invalid", Timestamp, WebhookSecret),
                "Should throw for invalid signature"
            );
        }

        [Test]
        public void VerifySignature_WithNullSignature_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<WebhookSignatureException>(
                () => WebhookVerifier.VerifySignature(RequestBody, null, Timestamp, WebhookSecret),
                "Should throw for null signature"
            );
        }

        [Test]
        public void VerifySignature_WithEmptySignature_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<WebhookSignatureException>(
                () => WebhookVerifier.VerifySignature(RequestBody, "", Timestamp, WebhookSecret),
                "Should throw for empty signature"
            );
        }

        [Test]
        public void VerifySignature_WithNullTimestamp_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<WebhookSignatureException>(
                () => WebhookVerifier.VerifySignature(RequestBody, _validSignature, null, WebhookSecret),
                "Should throw for null timestamp"
            );
        }

        [Test]
        public void VerifySignature_WithEmptyTimestamp_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<WebhookSignatureException>(
                () => WebhookVerifier.VerifySignature(RequestBody, _validSignature, "", WebhookSecret),
                "Should throw for empty timestamp"
            );
        }

        [Test]
        public void VerifySignature_WithTamperedBody_ThrowsException()
        {
            // Arrange
            string tamperedBody = "{\"event\":\"tampered_event\"}";

            // Act & Assert
            Assert.Throws<WebhookSignatureException>(
                () => WebhookVerifier.VerifySignature(tamperedBody, _validSignature, Timestamp, WebhookSecret),
                "Should throw for tampered body"
            );
        }

        [Test]
        public void VerifyWebhookSignature_WithValidSignature_DoesNotThrow()
        {
            // Arrange
            var headers = new Dictionary<string, string>
            {
                { WebhookVerifier.WebhookSignatureHeader, _validSignature },
                { WebhookVerifier.WebhookTimestampHeader, Timestamp }
            };

            // Act & Assert
            Assert.DoesNotThrow(() => WebhookVerifier.VerifyWebhookSignature(RequestBody, headers, WebhookSecret));
        }

        [Test]
        public void VerifyWebhookSignature_WithInvalidSignature_ThrowsException()
        {
            // Arrange
            var headers = new Dictionary<string, string>
            {
                { WebhookVerifier.WebhookSignatureHeader, "invalid" },
                { WebhookVerifier.WebhookTimestampHeader, Timestamp }
            };

            // Act & Assert
            Assert.Throws<WebhookSignatureException>(
                () => WebhookVerifier.VerifyWebhookSignature(RequestBody, headers, WebhookSecret),
                "Should throw for invalid signature"
            );
        }

        [Test]
        public void VerifyWebhookSignature_WithCaseInsensitiveHeaders_DoesNotThrow()
        {
            // Arrange
            var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { WebhookVerifier.WebhookSignatureHeader.ToLower(), _validSignature },
                { WebhookVerifier.WebhookTimestampHeader.ToLower(), Timestamp }
            };

            // Act & Assert
            Assert.DoesNotThrow(() => WebhookVerifier.VerifyWebhookSignature(RequestBody, headers, WebhookSecret));
        }

        [Test]
        public void VerifySignature_WithWrongSecret_ThrowsException()
        {
            // Arrange
            const string wrongSecret = "wrong_secret";

            // Act & Assert
            Assert.Throws<WebhookSignatureException>(
                () => WebhookVerifier.VerifySignature(RequestBody, _validSignature, Timestamp, wrongSecret),
                "Should throw when using wrong secret"
            );
        }
    }
}