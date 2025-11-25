using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchematicHQ.Client.RulesEngine;
using SchematicHQ.Client.RulesEngine.Utils;

namespace SchematicHQ.Client.Examples
{
    /// <summary>
    /// Example demonstrating how to use the WASM-powered rules engine
    /// for high-performance rule evaluation.
    /// </summary>
    public class WasmRulesEngineExample
    {
        public static async Task RunExample()
        {
            Console.WriteLine("=== WASM Rules Engine Example ===");
            
            // Create test data
            var company = CreateTestCompany();
            var user = CreateTestUser();
            var flag = CreateTestFlag();

            Console.WriteLine($"Testing flag: {flag.Key} for company: {company.Id}");
            
            // Test using traditional engine
            var traditionalStart = DateTime.UtcNow;
            var traditionalResult = await FlagCheckService.CheckFlag(company, user, flag);
            var traditionalDuration = DateTime.UtcNow - traditionalStart;
            
            Console.WriteLine($"Traditional Engine Result:");
            Console.WriteLine($"  Value: {traditionalResult.Value}");
            Console.WriteLine($"  Reason: {traditionalResult.Reason}");
            Console.WriteLine($"  Duration: {traditionalDuration.TotalMilliseconds:F2}ms");
            
            // Test using WASM engine
            var wasmStart = DateTime.UtcNow;
            using var wasmEngine = new WasmRulesEngine();
            var wasmResult = await wasmEngine.CheckFlagAsync(company, user, flag);
            var wasmDuration = DateTime.UtcNow - wasmStart;
            
            Console.WriteLine($"WASM Engine Result:");
            Console.WriteLine($"  Value: {wasmResult.Value}");
            Console.WriteLine($"  Reason: {wasmResult.Reason}");
            Console.WriteLine($"  Duration: {wasmDuration.TotalMilliseconds:F2}ms");
            
            // Verify results match
            var resultsMatch = traditionalResult.Value == wasmResult.Value &&
                              traditionalResult.Reason == wasmResult.Reason;
            
            Console.WriteLine($"Results Match: {resultsMatch}");
            
            if (wasmDuration < traditionalDuration)
            {
                var improvement = ((traditionalDuration - wasmDuration).TotalMilliseconds / traditionalDuration.TotalMilliseconds) * 100;
                Console.WriteLine($"WASM Performance Improvement: {improvement:F1}%");
            }
        }

        private static RulesengineCompany CreateTestCompany()
        {
            return new RulesengineCompany
            {
                Id = "test-company-123",
                AccountId = "test-account-456",
                EnvironmentId = "test-env-789",
                PlanIds = new List<string> { "premium-plan" },
                Traits = new List<RulesengineTrait>
                {
                    new RulesengineTrait { Key = "industry", Value = "technology" },
                    new RulesengineTrait { Key = "size", Value = "large" }
                }
            };
        }

        private static RulesengineUser CreateTestUser()
        {
            return new RulesengineUser
            {
                Id = "test-user-abc",
                AccountId = "test-account-456",
                EnvironmentId = "test-env-789",
                Traits = new List<RulesengineTrait>
                {
                    new RulesengineTrait { Key = "role", Value = "admin" },
                    new RulesengineTrait { Key = "experience", Value = "expert" }
                }
            };
        }

        private static RulesengineFlag CreateTestFlag()
        {
            var flag = new RulesengineFlag
            {
                Id = "test-flag-xyz",
                Key = "advanced-features",
                DefaultValue = false,
                Rules = new List<RulesengineRule>()
            };

            // Add a rule that enables the feature for premium plans
            var rule = new RulesengineRule
            {
                Id = "premium-rule-001",
                RuleType = RulesengineRuleRuleType.Standard,
                Priority = 1,
                Value = true,
                Conditions = new List<RulesengineCondition>
                {
                    new RulesengineCondition
                    {
                        Id = "condition-001",
                        ConditionType = RulesengineConditionConditionType.Company,
                        Operator = RulesengineConditionOperator.Eq,
                        ResourceIds = new List<string> { "test-company-123" }
                    }
                }
            };

            flag.Rules.Add(rule);
            return flag;
        }
    }
}