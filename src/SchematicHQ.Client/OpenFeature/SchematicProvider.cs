using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenFeature;
using OpenFeature.Constant;
using OpenFeature.Error;
using OpenFeature.Model;

namespace SchematicHQ.Client.OpenFeature
{
    /// <summary>
    /// OpenFeature provider for Schematic feature flags.
    /// </summary>
    public class SchematicProvider : FeatureProvider
    {
        private static readonly Metadata _metadata = new Metadata("schematic-provider");
        private readonly Schematic _schematic;
        private readonly ClientOptions _options;

        /// <summary>
        /// Creates a new instance of the SchematicProvider.
        /// </summary>
        /// <param name="apiKey">The Schematic API key.</param>
        /// <param name="options">Optional client configuration options.</param>
        public SchematicProvider(string apiKey, ClientOptions? options = null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("API key is required", nameof(apiKey));
            }

            _options = options ?? new ClientOptions();
            _schematic = new Schematic(apiKey, _options);
        }

        /// <summary>
        /// Creates a new instance of the SchematicProvider with a pre-configured Schematic client.
        /// </summary>
        /// <param name="schematic">A pre-configured Schematic client instance.</param>
        /// <param name="options">Optional client configuration options.</param>
        public SchematicProvider(Schematic schematic, ClientOptions? options = null)
        {
            _schematic = schematic ?? throw new ArgumentNullException(nameof(schematic));
            _options = options ?? new ClientOptions();
        }

        /// <inheritdoc/>
        public override Metadata GetMetadata()
        {
            return _metadata;
        }

        /// <inheritdoc/>
        public override async Task<ResolutionDetails<bool>> ResolveBooleanValueAsync(
            string flagKey, 
            bool defaultValue, 
            EvaluationContext? context = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var (company, user) = ExtractContext(context);
                
                var result = await _schematic.CheckFlag(
                    flagKey: flagKey,
                    company: company,
                    user: user
                ).ConfigureAwait(false);

                return new ResolutionDetails<bool>(
                    flagKey: flagKey,
                    value: result,
                    variant: result ? "on" : "off",
                    reason: Reason.Cached
                );
            }
            catch (Exception ex)
            {
                _options.Logger?.Error("Error evaluating boolean flag '{0}': {1}", flagKey, ex.Message);
                
                var errorType = MapExceptionToErrorType(ex);
                return new ResolutionDetails<bool>(
                    flagKey: flagKey,
                    value: defaultValue,
                    variant: defaultValue ? "on" : "off",
                    reason: Reason.Error,
                    errorType: errorType,
                    errorMessage: ex.Message
                );
            }
        }

        /// <inheritdoc/>
        public override Task<ResolutionDetails<string>> ResolveStringValueAsync(
            string flagKey, 
            string defaultValue, 
            EvaluationContext? context = null,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new ResolutionDetails<string>(
                flagKey: flagKey,
                value: defaultValue,
                reason: Reason.Error,
                errorType: ErrorType.TypeMismatch,
                errorMessage: "String flags are not supported by Schematic provider"
            ));
        }

        /// <inheritdoc/>
        public override Task<ResolutionDetails<int>> ResolveIntegerValueAsync(
            string flagKey, 
            int defaultValue, 
            EvaluationContext? context = null,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new ResolutionDetails<int>(
                flagKey: flagKey,
                value: defaultValue,
                reason: Reason.Error,
                errorType: ErrorType.TypeMismatch,
                errorMessage: "Integer flags are not supported by Schematic provider"
            ));
        }

        /// <inheritdoc/>
        public override Task<ResolutionDetails<double>> ResolveDoubleValueAsync(
            string flagKey, 
            double defaultValue, 
            EvaluationContext? context = null,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new ResolutionDetails<double>(
                flagKey: flagKey,
                value: defaultValue,
                reason: Reason.Error,
                errorType: ErrorType.TypeMismatch,
                errorMessage: "Double flags are not supported by Schematic provider"
            ));
        }

        /// <inheritdoc/>
        public override Task<ResolutionDetails<Value>> ResolveStructureValueAsync(
            string flagKey, 
            Value defaultValue, 
            EvaluationContext? context = null,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new ResolutionDetails<Value>(
                flagKey: flagKey,
                value: defaultValue,
                reason: Reason.Error,
                errorType: ErrorType.TypeMismatch,
                errorMessage: "Structure flags are not supported by Schematic provider"
            ));
        }

        /// <inheritdoc/>
        public override Task InitializeAsync(EvaluationContext? context, CancellationToken cancellationToken = default)
        {
            _options.Logger?.Info("Schematic provider initialized");
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override async Task ShutdownAsync(CancellationToken cancellationToken = default)
        {
            _options.Logger?.Info("Schematic provider shutting down");
            await _schematic.Shutdown().ConfigureAwait(false);
        }

        /// <summary>
        /// Tracks an event in Schematic.
        /// </summary>
        /// <param name="eventName">The name of the event to track.</param>
        /// <param name="context">The evaluation context containing user and company information.</param>
        /// <param name="traits">Additional event properties.</param>
        public Task TrackEventAsync(
            string eventName, 
            EvaluationContext? context = null, 
            Dictionary<string, object>? traits = null)
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentException("Event name is required", nameof(eventName));
            }

            var (company, user) = ExtractContext(context);
            var eventTraits = traits ?? new Dictionary<string, object>();

            // Add context attributes to traits if they don't conflict
            if (context != null)
            {
                foreach (var attr in context.AsDictionary())
                {
                    if (attr.Key != "company" && attr.Key != "user" && !eventTraits.ContainsKey(attr.Key))
                    {
                        eventTraits[attr.Key] = attr.Value.AsObject ?? attr.Value.ToString();
                    }
                }
            }

            _schematic.Track(
                eventName: eventName,
                company: company,
                user: user,
                traits: eventTraits
            );

            return Task.CompletedTask;
        }

        private static (Dictionary<string, string>? company, Dictionary<string, string>? user) ExtractContext(EvaluationContext? context)
        {
            if (context == null)
            {
                return (null, null);
            }

            Dictionary<string, string>? company = null;
            Dictionary<string, string>? user = null;

            // Extract company context
            if (context.TryGetValue("company", out var companyValue) && companyValue.IsStructure)
            {
                company = companyValue.AsStructure
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.AsString ?? kvp.Value.ToString());
            }

            // Extract user context
            if (context.TryGetValue("user", out var userValue) && userValue.IsStructure)
            {
                user = userValue.AsStructure
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.AsString ?? kvp.Value.ToString());
            }

            // If there's a targeting key but no user ID, set it as the user ID
            if (!string.IsNullOrEmpty(context.TargetingKey) && user?.ContainsKey("id") != true)
            {
                user ??= new Dictionary<string, string>();
                user["id"] = context.TargetingKey;
            }

            return (company, user);
        }

        private static ErrorType MapExceptionToErrorType(Exception ex)
        {
            return ex switch
            {
                ArgumentException => ErrorType.InvalidContext,
                UnauthorizedAccessException => ErrorType.General,
                NotImplementedException => ErrorType.FlagNotFound,
                _ => ErrorType.General
            };
        }
    }
}