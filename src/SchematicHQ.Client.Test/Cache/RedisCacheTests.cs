using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using NUnit.Framework;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;
using StackExchange.Redis;

namespace SchematicHQ.Client.Test.Cache
{
    [TestFixture]
    public class RedisCacheTests
    {
        
        [Test]
        public void Constructor_WithRedisCacheConfig_ParsesCorrectly()
        {
            // Arrange
            var config = new RedisCacheConfig
            {
                Endpoints = new List<string> { "localhost:6379" },
                Password = "testPassword123",
                Database = 1,
                Ssl = true,
                KeyPrefix = "test:",
                CacheTTL = TimeSpan.FromMinutes(30)
            };
            
            // Act & Assert
            Assert.DoesNotThrow(() => 
            {
                try
                {
                    var cache = new RedisCache<string>(config);
                }
                catch (InvalidOperationException ex) when (ex.Message.Contains("Failed to connect to Redis"))
                {
                    // Expected if Redis is not running
                    Assert.That(ex.InnerException, Is.Not.Null);
                    Assert.That(ex.InnerException, Is.TypeOf<RedisConnectionException>());
                }
            });
        }
        
        [Test]
        public void Constructor_WithRedisCacheConfig_MultipleEndpoints()
        {
            // Arrange
            var config = new RedisCacheConfig
            {
                Endpoints = new List<string> 
                { 
                    "redis1.example.com:6379",
                    "redis2.example.com:6379",
                    "redis3.example.com:6379"
                },
                Password = "clusterPassword",
                Username = "default",
                ConnectTimeout = 10000,
                AbortOnConnectFail = false
            };
            
            // Act & Assert
            Assert.DoesNotThrow(() => 
            {
                try
                {
                    var cache = new RedisCache<string>(config);
                }
                catch (InvalidOperationException ex) when (ex.Message.Contains("Failed to connect to Redis"))
                {
                    // Expected if Redis is not running
                    Assert.That(ex.InnerException, Is.Not.Null);
                    Assert.That(ex.InnerException, Is.TypeOf<RedisConnectionException>());
                }
            });
        }
        
        [Test]
        public void Constructor_WithRedisCacheConfig_NullConfig_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RedisCache<string>((RedisCacheConfig)null));
        }
        
        [Test]
        public void Constructor_WithRedisCacheConfig_EmptyEndpoints_ThrowsArgumentException()
        {
            // Arrange
            var config = new RedisCacheConfig
            {
                Endpoints = new List<string>()
            };
            
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new RedisCache<string>(config));
            Assert.That(ex.Message, Contains.Substring("Redis endpoints cannot be null or empty"));
        }
        
        [Test]
        public void RedisCacheConfig_DefaultKeyPrefix_IsSchematic()
        {
            // Arrange & Act
            var config = new RedisCacheConfig();
            
            // Assert
            Assert.That(config.KeyPrefix, Is.EqualTo("schematic:"));
        }
        
        [Test]
        public void RedisCacheConfig_CustomKeyPrefix_OverridesDefault()
        {
            // Arrange & Act
            var config = new RedisCacheConfig
            {
                KeyPrefix = "myapp:"
            };
            
            // Assert
            Assert.That(config.KeyPrefix, Is.EqualTo("myapp:"));
        }
        
        [Test]
        public void RedisCacheConfig_NullKeyPrefix_IsAllowed()
        {
            // Arrange & Act
            var config = new RedisCacheConfig
            {
                KeyPrefix = null
            };
            
            // Assert
            Assert.That(config.KeyPrefix, Is.Null);
        }
    }
}