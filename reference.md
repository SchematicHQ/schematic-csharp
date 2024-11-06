# Reference
<details><summary><code>client.<a href="/src/SchematicHQ.Client/SchematicApi.cs">GetCompanyPlansAsync</a>()</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.GetCompanyPlansAsync();
```
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## accounts
<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">ListApiKeysAsync</a>(ListApiKeysRequest { ... }) -> ListApiKeysResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.ListApiKeysAsync(new ListApiKeysRequest { RequireEnvironment = true });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListApiKeysRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">CreateApiKeyAsync</a>(CreateApiKeyRequestBody { ... }) -> CreateApiKeyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.CreateApiKeyAsync(new CreateApiKeyRequestBody { Name = "name" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateApiKeyRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">GetApiKeyAsync</a>(apiKeyId) -> GetApiKeyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.GetApiKeyAsync("api_key_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**apiKeyId:** `string` — api_key_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">UpdateApiKeyAsync</a>(apiKeyId, UpdateApiKeyRequestBody { ... }) -> UpdateApiKeyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.UpdateApiKeyAsync("api_key_id", new UpdateApiKeyRequestBody());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**apiKeyId:** `string` — api_key_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateApiKeyRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">DeleteApiKeyAsync</a>(apiKeyId) -> DeleteApiKeyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.DeleteApiKeyAsync("api_key_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**apiKeyId:** `string` — api_key_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">CountApiKeysAsync</a>(CountApiKeysRequest { ... }) -> CountApiKeysResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.CountApiKeysAsync(new CountApiKeysRequest { RequireEnvironment = true });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountApiKeysRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">ListApiRequestsAsync</a>(ListApiRequestsRequest { ... }) -> ListApiRequestsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.ListApiRequestsAsync(new ListApiRequestsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListApiRequestsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">GetApiRequestAsync</a>(apiRequestId) -> GetApiRequestResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.GetApiRequestAsync("api_request_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**apiRequestId:** `string` — api_request_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">CountApiRequestsAsync</a>(CountApiRequestsRequest { ... }) -> CountApiRequestsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.CountApiRequestsAsync(new CountApiRequestsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountApiRequestsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">ListEnvironmentsAsync</a>(ListEnvironmentsRequest { ... }) -> ListEnvironmentsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.ListEnvironmentsAsync(new ListEnvironmentsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListEnvironmentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">CreateEnvironmentAsync</a>(CreateEnvironmentRequestBody { ... }) -> CreateEnvironmentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.CreateEnvironmentAsync(
    new CreateEnvironmentRequestBody
    {
        EnvironmentType = CreateEnvironmentRequestBodyEnvironmentType.Development,
        Name = "name",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateEnvironmentRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">GetEnvironmentAsync</a>(environmentId) -> GetEnvironmentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.GetEnvironmentAsync("environment_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**environmentId:** `string` — environment_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">UpdateEnvironmentAsync</a>(environmentId, UpdateEnvironmentRequestBody { ... }) -> UpdateEnvironmentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.UpdateEnvironmentAsync("environment_id", new UpdateEnvironmentRequestBody());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**environmentId:** `string` — environment_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateEnvironmentRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">DeleteEnvironmentAsync</a>(environmentId) -> DeleteEnvironmentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.DeleteEnvironmentAsync("environment_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**environmentId:** `string` — environment_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## features
<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CountAudienceCompaniesAsync</a>(AudienceRequestBody { ... }) -> CountAudienceCompaniesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.CountAudienceCompaniesAsync(
    new AudienceRequestBody
    {
        ConditionGroups = new List<CreateOrUpdateConditionGroupRequestBody>()
        {
            new CreateOrUpdateConditionGroupRequestBody
            {
                Conditions = new List<CreateOrUpdateConditionRequestBody>()
                {
                    new CreateOrUpdateConditionRequestBody
                    {
                        ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
                        Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                        ResourceIds = new List<string>() { "resource_ids" },
                    },
                },
            },
        },
        Conditions = new List<CreateOrUpdateConditionRequestBody>()
        {
            new CreateOrUpdateConditionRequestBody
            {
                ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
                Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                ResourceIds = new List<string>() { "resource_ids" },
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `AudienceRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CountAudienceUsersAsync</a>(AudienceRequestBody { ... }) -> CountAudienceUsersResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.CountAudienceUsersAsync(
    new AudienceRequestBody
    {
        ConditionGroups = new List<CreateOrUpdateConditionGroupRequestBody>()
        {
            new CreateOrUpdateConditionGroupRequestBody
            {
                Conditions = new List<CreateOrUpdateConditionRequestBody>()
                {
                    new CreateOrUpdateConditionRequestBody
                    {
                        ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
                        Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                        ResourceIds = new List<string>() { "resource_ids" },
                    },
                },
            },
        },
        Conditions = new List<CreateOrUpdateConditionRequestBody>()
        {
            new CreateOrUpdateConditionRequestBody
            {
                ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
                Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                ResourceIds = new List<string>() { "resource_ids" },
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `AudienceRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">ListAudienceCompaniesAsync</a>(AudienceRequestBody { ... }) -> ListAudienceCompaniesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.ListAudienceCompaniesAsync(
    new AudienceRequestBody
    {
        ConditionGroups = new List<CreateOrUpdateConditionGroupRequestBody>()
        {
            new CreateOrUpdateConditionGroupRequestBody
            {
                Conditions = new List<CreateOrUpdateConditionRequestBody>()
                {
                    new CreateOrUpdateConditionRequestBody
                    {
                        ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
                        Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                        ResourceIds = new List<string>() { "resource_ids" },
                    },
                },
            },
        },
        Conditions = new List<CreateOrUpdateConditionRequestBody>()
        {
            new CreateOrUpdateConditionRequestBody
            {
                ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
                Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                ResourceIds = new List<string>() { "resource_ids" },
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `AudienceRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">ListAudienceUsersAsync</a>(AudienceRequestBody { ... }) -> ListAudienceUsersResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.ListAudienceUsersAsync(
    new AudienceRequestBody
    {
        ConditionGroups = new List<CreateOrUpdateConditionGroupRequestBody>()
        {
            new CreateOrUpdateConditionGroupRequestBody
            {
                Conditions = new List<CreateOrUpdateConditionRequestBody>()
                {
                    new CreateOrUpdateConditionRequestBody
                    {
                        ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
                        Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                        ResourceIds = new List<string>() { "resource_ids" },
                    },
                },
            },
        },
        Conditions = new List<CreateOrUpdateConditionRequestBody>()
        {
            new CreateOrUpdateConditionRequestBody
            {
                ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
                Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                ResourceIds = new List<string>() { "resource_ids" },
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `AudienceRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">ListFeaturesAsync</a>(ListFeaturesRequest { ... }) -> ListFeaturesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.ListFeaturesAsync(new ListFeaturesRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListFeaturesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CreateFeatureAsync</a>(CreateFeatureRequestBody { ... }) -> CreateFeatureResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.CreateFeatureAsync(
    new CreateFeatureRequestBody
    {
        Description = "description",
        FeatureType = CreateFeatureRequestBodyFeatureType.Boolean,
        Name = "name",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateFeatureRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">GetFeatureAsync</a>(featureId) -> GetFeatureResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.GetFeatureAsync("feature_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**featureId:** `string` — feature_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">UpdateFeatureAsync</a>(featureId, UpdateFeatureRequestBody { ... }) -> UpdateFeatureResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.UpdateFeatureAsync("feature_id", new UpdateFeatureRequestBody());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**featureId:** `string` — feature_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateFeatureRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">DeleteFeatureAsync</a>(featureId) -> DeleteFeatureResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.DeleteFeatureAsync("feature_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**featureId:** `string` — feature_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CountFeaturesAsync</a>(CountFeaturesRequest { ... }) -> CountFeaturesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.CountFeaturesAsync(new CountFeaturesRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountFeaturesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">ListFlagsAsync</a>(ListFlagsRequest { ... }) -> ListFlagsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.ListFlagsAsync(new ListFlagsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListFlagsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CreateFlagAsync</a>(CreateFlagRequestBody { ... }) -> CreateFlagResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.CreateFlagAsync(
    new CreateFlagRequestBody
    {
        DefaultValue = true,
        Description = "description",
        FlagType = "flag_type",
        Key = "key",
        Name = "name",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateFlagRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">GetFlagAsync</a>(flagId) -> GetFlagResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.GetFlagAsync("flag_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**flagId:** `string` — flag_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">UpdateFlagAsync</a>(flagId, CreateFlagRequestBody { ... }) -> UpdateFlagResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.UpdateFlagAsync(
    "flag_id",
    new CreateFlagRequestBody
    {
        DefaultValue = true,
        Description = "description",
        FlagType = "flag_type",
        Key = "key",
        Name = "name",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**flagId:** `string` — flag_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `CreateFlagRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">DeleteFlagAsync</a>(flagId) -> DeleteFlagResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.DeleteFlagAsync("flag_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**flagId:** `string` — flag_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">UpdateFlagRulesAsync</a>(flagId, UpdateFlagRulesRequestBody { ... }) -> UpdateFlagRulesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.UpdateFlagRulesAsync(
    "flag_id",
    new UpdateFlagRulesRequestBody
    {
        Rules = new List<CreateOrUpdateRuleRequestBody>()
        {
            new CreateOrUpdateRuleRequestBody
            {
                ConditionGroups = new List<CreateOrUpdateConditionGroupRequestBody>()
                {
                    new CreateOrUpdateConditionGroupRequestBody
                    {
                        Conditions = new List<CreateOrUpdateConditionRequestBody>()
                        {
                            new CreateOrUpdateConditionRequestBody
                            {
                                ConditionType =
                                    CreateOrUpdateConditionRequestBodyConditionType.Company,
                                Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                                ResourceIds = new List<string>() { "resource_ids" },
                            },
                        },
                    },
                },
                Conditions = new List<CreateOrUpdateConditionRequestBody>()
                {
                    new CreateOrUpdateConditionRequestBody
                    {
                        ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
                        Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                        ResourceIds = new List<string>() { "resource_ids" },
                    },
                },
                Name = "name",
                Priority = 1,
                Value = true,
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**flagId:** `string` — flag_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateFlagRulesRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CheckFlagAsync</a>(key, CheckFlagRequestBody { ... }) -> CheckFlagResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.CheckFlagAsync("key", new CheckFlagRequestBody());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**key:** `string` — key
    
</dd>
</dl>

<dl>
<dd>

**request:** `CheckFlagRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CheckFlagsAsync</a>(CheckFlagRequestBody { ... }) -> CheckFlagsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.CheckFlagsAsync(new CheckFlagRequestBody());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CheckFlagRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CountFlagsAsync</a>(CountFlagsRequest { ... }) -> CountFlagsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.CountFlagsAsync(new CountFlagsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountFlagsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## billing
<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingCustomerAsync</a>(CreateBillingCustomerRequestBody { ... }) -> UpsertBillingCustomerResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.UpsertBillingCustomerAsync(
    new CreateBillingCustomerRequestBody
    {
        Email = "email",
        ExternalId = "external_id",
        FailedToImport = true,
        Meta = new Dictionary<string, string>() { { "key", "value" } },
        Name = "name",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateBillingCustomerRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListCustomersAsync</a>(ListCustomersRequest { ... }) -> ListCustomersResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListCustomersAsync(new ListCustomersRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListCustomersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">CountCustomersAsync</a>(CountCustomersRequest { ... }) -> CountCustomersResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.CountCustomersAsync(new CountCustomersRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountCustomersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListInvoicesAsync</a>(ListInvoicesRequest { ... }) -> ListInvoicesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListInvoicesAsync(
    new ListInvoicesRequest { CustomerExternalId = "customer_external_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListInvoicesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertInvoiceAsync</a>(CreateInvoiceRequestBody { ... }) -> UpsertInvoiceResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.UpsertInvoiceAsync(
    new CreateInvoiceRequestBody
    {
        AmountDue = 1,
        AmountPaid = 1,
        AmountRemaining = 1,
        CollectionMethod = "collection_method",
        Currency = "currency",
        CustomerExternalId = "customer_external_id",
        Subtotal = 1,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateInvoiceRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListMetersAsync</a>(ListMetersRequest { ... }) -> ListMetersResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListMetersAsync(new ListMetersRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListMetersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingMeterAsync</a>(CreateMeterRequestBody { ... }) -> UpsertBillingMeterResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.UpsertBillingMeterAsync(
    new CreateMeterRequestBody
    {
        DisplayName = "display_name",
        EventName = "event_name",
        EventPayloadKey = "event_payload_key",
        ExternalId = "external_id",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateMeterRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListPaymentMethodsAsync</a>(ListPaymentMethodsRequest { ... }) -> ListPaymentMethodsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListPaymentMethodsAsync(
    new ListPaymentMethodsRequest { CustomerExternalId = "customer_external_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListPaymentMethodsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertPaymentMethodAsync</a>(CreatePaymentMethodRequestBody { ... }) -> UpsertPaymentMethodResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.UpsertPaymentMethodAsync(
    new CreatePaymentMethodRequestBody
    {
        CustomerExternalId = "customer_external_id",
        ExternalId = "external_id",
        PaymentMethodType = "payment_method_type",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreatePaymentMethodRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingPriceAsync</a>(CreateBillingPriceRequestBody { ... }) -> UpsertBillingPriceResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.UpsertBillingPriceAsync(
    new CreateBillingPriceRequestBody
    {
        Currency = "currency",
        Interval = "interval",
        Price = 1,
        PriceExternalId = "price_external_id",
        ProductExternalId = "product_external_id",
        UsageType = "usage_type",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateBillingPriceRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListProductPricesAsync</a>(ListProductPricesRequest { ... }) -> ListProductPricesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListProductPricesAsync(new ListProductPricesRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListProductPricesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">DeleteProductPriceAsync</a>(billingId) -> DeleteProductPriceResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.DeleteProductPriceAsync("billing_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**billingId:** `string` — billing_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingProductAsync</a>(CreateBillingProductRequestBody { ... }) -> UpsertBillingProductResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.UpsertBillingProductAsync(
    new CreateBillingProductRequestBody
    {
        Currency = "currency",
        ExternalId = "external_id",
        Name = "name",
        Price = 1.1,
        Quantity = 1,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateBillingProductRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListBillingProductsAsync</a>(ListBillingProductsRequest { ... }) -> ListBillingProductsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListBillingProductsAsync(new ListBillingProductsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListBillingProductsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">CountBillingProductsAsync</a>(CountBillingProductsRequest { ... }) -> CountBillingProductsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.CountBillingProductsAsync(new CountBillingProductsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountBillingProductsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingSubscriptionAsync</a>(CreateBillingSubscriptionsRequestBody { ... }) -> UpsertBillingSubscriptionResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.UpsertBillingSubscriptionAsync(
    new CreateBillingSubscriptionsRequestBody
    {
        Currency = "currency",
        CustomerExternalId = "customer_external_id",
        ExpiredAt = new DateTime(2024, 01, 15, 09, 30, 00, 000),
        ProductExternalIds = new List<BillingProductPricing>()
        {
            new BillingProductPricing
            {
                Currency = "currency",
                Interval = "interval",
                Price = 1,
                PriceExternalId = "price_external_id",
                ProductExternalId = "product_external_id",
                Quantity = 1,
                UsageType = "usage_type",
            },
        },
        SubscriptionExternalId = "subscription_external_id",
        TotalPrice = 1,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateBillingSubscriptionsRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## companies
<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListCompaniesAsync</a>(ListCompaniesRequest { ... }) -> ListCompaniesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListCompaniesAsync(new ListCompaniesRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListCompaniesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpsertCompanyAsync</a>(UpsertCompanyRequestBody { ... }) -> UpsertCompanyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.UpsertCompanyAsync(
    new UpsertCompanyRequestBody { Keys = new Dictionary<string, string>() { { "key", "value" } } }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpsertCompanyRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetCompanyAsync</a>(companyId) -> GetCompanyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetCompanyAsync("company_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**companyId:** `string` — company_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteCompanyAsync</a>(companyId) -> DeleteCompanyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.DeleteCompanyAsync("company_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**companyId:** `string` — company_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountCompaniesAsync</a>(CountCompaniesRequest { ... }) -> CountCompaniesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CountCompaniesAsync(new CountCompaniesRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountCompaniesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CreateCompanyAsync</a>(UpsertCompanyRequestBody { ... }) -> CreateCompanyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CreateCompanyAsync(
    new UpsertCompanyRequestBody { Keys = new Dictionary<string, string>() { { "key", "value" } } }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpsertCompanyRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteCompanyByKeysAsync</a>(KeysRequestBody { ... }) -> DeleteCompanyByKeysResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.DeleteCompanyByKeysAsync(
    new KeysRequestBody { Keys = new Dictionary<string, string>() { { "key", "value" } } }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `KeysRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">LookupCompanyAsync</a>(LookupCompanyRequest { ... }) -> LookupCompanyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.LookupCompanyAsync(
    new LookupCompanyRequest
    {
        Keys = new Dictionary<string, object>()
        {
            {
                "string",
                new Dictionary<object, object?>() { { "key", "value" } }
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `LookupCompanyRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetActiveDealsAsync</a>(GetActiveDealsRequest { ... }) -> GetActiveDealsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetActiveDealsAsync(
    new GetActiveDealsRequest { CompanyId = "company_id", DealStage = "deal_stage" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetActiveDealsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListCompanyMembershipsAsync</a>(ListCompanyMembershipsRequest { ... }) -> ListCompanyMembershipsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListCompanyMembershipsAsync(new ListCompanyMembershipsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListCompanyMembershipsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetOrCreateCompanyMembershipAsync</a>(GetOrCreateCompanyMembershipRequestBody { ... }) -> GetOrCreateCompanyMembershipResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetOrCreateCompanyMembershipAsync(
    new GetOrCreateCompanyMembershipRequestBody { CompanyId = "company_id", UserId = "user_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetOrCreateCompanyMembershipRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteCompanyMembershipAsync</a>(companyMembershipId) -> DeleteCompanyMembershipResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.DeleteCompanyMembershipAsync("company_membership_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**companyMembershipId:** `string` — company_membership_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetActiveCompanySubscriptionAsync</a>(GetActiveCompanySubscriptionRequest { ... }) -> GetActiveCompanySubscriptionResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetActiveCompanySubscriptionAsync(new GetActiveCompanySubscriptionRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetActiveCompanySubscriptionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpsertCompanyTraitAsync</a>(UpsertTraitRequestBody { ... }) -> UpsertCompanyTraitResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.UpsertCompanyTraitAsync(
    new UpsertTraitRequestBody
    {
        Keys = new Dictionary<string, string>() { { "key", "value" } },
        Trait = "trait",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpsertTraitRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListEntityKeyDefinitionsAsync</a>(ListEntityKeyDefinitionsRequest { ... }) -> ListEntityKeyDefinitionsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListEntityKeyDefinitionsAsync(new ListEntityKeyDefinitionsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListEntityKeyDefinitionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountEntityKeyDefinitionsAsync</a>(CountEntityKeyDefinitionsRequest { ... }) -> CountEntityKeyDefinitionsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CountEntityKeyDefinitionsAsync(new CountEntityKeyDefinitionsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountEntityKeyDefinitionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListEntityTraitDefinitionsAsync</a>(ListEntityTraitDefinitionsRequest { ... }) -> ListEntityTraitDefinitionsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListEntityTraitDefinitionsAsync(new ListEntityTraitDefinitionsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListEntityTraitDefinitionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetOrCreateEntityTraitDefinitionAsync</a>(CreateEntityTraitDefinitionRequestBody { ... }) -> GetOrCreateEntityTraitDefinitionResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetOrCreateEntityTraitDefinitionAsync(
    new CreateEntityTraitDefinitionRequestBody
    {
        EntityType = CreateEntityTraitDefinitionRequestBodyEntityType.Company,
        Hierarchy = new List<string>() { "hierarchy" },
        TraitType = CreateEntityTraitDefinitionRequestBodyTraitType.Boolean,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateEntityTraitDefinitionRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetEntityTraitDefinitionAsync</a>(entityTraitDefinitionId) -> GetEntityTraitDefinitionResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetEntityTraitDefinitionAsync("entity_trait_definition_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**entityTraitDefinitionId:** `string` — entity_trait_definition_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpdateEntityTraitDefinitionAsync</a>(entityTraitDefinitionId, UpdateEntityTraitDefinitionRequestBody { ... }) -> UpdateEntityTraitDefinitionResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.UpdateEntityTraitDefinitionAsync(
    "entity_trait_definition_id",
    new UpdateEntityTraitDefinitionRequestBody
    {
        TraitType = UpdateEntityTraitDefinitionRequestBodyTraitType.Boolean,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**entityTraitDefinitionId:** `string` — entity_trait_definition_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateEntityTraitDefinitionRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountEntityTraitDefinitionsAsync</a>(CountEntityTraitDefinitionsRequest { ... }) -> CountEntityTraitDefinitionsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CountEntityTraitDefinitionsAsync(new CountEntityTraitDefinitionsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountEntityTraitDefinitionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetEntityTraitValuesAsync</a>(GetEntityTraitValuesRequest { ... }) -> GetEntityTraitValuesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetEntityTraitValuesAsync(
    new GetEntityTraitValuesRequest { DefinitionId = "definition_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetEntityTraitValuesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpsertUserTraitAsync</a>(UpsertTraitRequestBody { ... }) -> UpsertUserTraitResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.UpsertUserTraitAsync(
    new UpsertTraitRequestBody
    {
        Keys = new Dictionary<string, string>() { { "key", "value" } },
        Trait = "trait",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpsertTraitRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListUsersAsync</a>(ListUsersRequest { ... }) -> ListUsersResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListUsersAsync(new ListUsersRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListUsersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpsertUserAsync</a>(UpsertUserRequestBody { ... }) -> UpsertUserResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.UpsertUserAsync(
    new UpsertUserRequestBody
    {
        Company = new Dictionary<string, string>() { { "key", "value" } },
        Keys = new Dictionary<string, string>() { { "key", "value" } },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpsertUserRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetUserAsync</a>(userId) -> GetUserResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetUserAsync("user_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**userId:** `string` — user_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteUserAsync</a>(userId) -> DeleteUserResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.DeleteUserAsync("user_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**userId:** `string` — user_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountUsersAsync</a>(CountUsersRequest { ... }) -> CountUsersResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CountUsersAsync(new CountUsersRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountUsersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CreateUserAsync</a>(UpsertUserRequestBody { ... }) -> CreateUserResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CreateUserAsync(
    new UpsertUserRequestBody
    {
        Company = new Dictionary<string, string>() { { "key", "value" } },
        Keys = new Dictionary<string, string>() { { "key", "value" } },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpsertUserRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteUserByKeysAsync</a>(KeysRequestBody { ... }) -> DeleteUserByKeysResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.DeleteUserByKeysAsync(
    new KeysRequestBody { Keys = new Dictionary<string, string>() { { "key", "value" } } }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `KeysRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">LookupUserAsync</a>(LookupUserRequest { ... }) -> LookupUserResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.LookupUserAsync(
    new LookupUserRequest
    {
        Keys = new Dictionary<string, object>()
        {
            {
                "string",
                new Dictionary<object, object?>() { { "key", "value" } }
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `LookupUserRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## entitlements
<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListCompanyOverridesAsync</a>(ListCompanyOverridesRequest { ... }) -> ListCompanyOverridesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.ListCompanyOverridesAsync(new ListCompanyOverridesRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListCompanyOverridesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CreateCompanyOverrideAsync</a>(CreateCompanyOverrideRequestBody { ... }) -> CreateCompanyOverrideResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.CreateCompanyOverrideAsync(
    new CreateCompanyOverrideRequestBody
    {
        CompanyId = "company_id",
        FeatureId = "feature_id",
        ValueType = CreateCompanyOverrideRequestBodyValueType.Boolean,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateCompanyOverrideRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">GetCompanyOverrideAsync</a>(companyOverrideId) -> GetCompanyOverrideResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.GetCompanyOverrideAsync("company_override_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**companyOverrideId:** `string` — company_override_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">UpdateCompanyOverrideAsync</a>(companyOverrideId, UpdateCompanyOverrideRequestBody { ... }) -> UpdateCompanyOverrideResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.UpdateCompanyOverrideAsync(
    "company_override_id",
    new UpdateCompanyOverrideRequestBody
    {
        ValueType = UpdateCompanyOverrideRequestBodyValueType.Boolean,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**companyOverrideId:** `string` — company_override_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateCompanyOverrideRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">DeleteCompanyOverrideAsync</a>(companyOverrideId) -> DeleteCompanyOverrideResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.DeleteCompanyOverrideAsync("company_override_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**companyOverrideId:** `string` — company_override_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CountCompanyOverridesAsync</a>(CountCompanyOverridesRequest { ... }) -> CountCompanyOverridesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.CountCompanyOverridesAsync(new CountCompanyOverridesRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountCompanyOverridesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListFeatureCompaniesAsync</a>(ListFeatureCompaniesRequest { ... }) -> ListFeatureCompaniesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.ListFeatureCompaniesAsync(
    new ListFeatureCompaniesRequest { FeatureId = "feature_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListFeatureCompaniesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CountFeatureCompaniesAsync</a>(CountFeatureCompaniesRequest { ... }) -> CountFeatureCompaniesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.CountFeatureCompaniesAsync(
    new CountFeatureCompaniesRequest { FeatureId = "feature_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountFeatureCompaniesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListFeatureUsageAsync</a>(ListFeatureUsageRequest { ... }) -> ListFeatureUsageResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.ListFeatureUsageAsync(new ListFeatureUsageRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListFeatureUsageRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CountFeatureUsageAsync</a>(CountFeatureUsageRequest { ... }) -> CountFeatureUsageResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.CountFeatureUsageAsync(new CountFeatureUsageRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountFeatureUsageRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListFeatureUsersAsync</a>(ListFeatureUsersRequest { ... }) -> ListFeatureUsersResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.ListFeatureUsersAsync(
    new ListFeatureUsersRequest { FeatureId = "feature_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListFeatureUsersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CountFeatureUsersAsync</a>(CountFeatureUsersRequest { ... }) -> CountFeatureUsersResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.CountFeatureUsersAsync(
    new CountFeatureUsersRequest { FeatureId = "feature_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountFeatureUsersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListPlanEntitlementsAsync</a>(ListPlanEntitlementsRequest { ... }) -> ListPlanEntitlementsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.ListPlanEntitlementsAsync(new ListPlanEntitlementsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListPlanEntitlementsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CreatePlanEntitlementAsync</a>(CreatePlanEntitlementRequestBody { ... }) -> CreatePlanEntitlementResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.CreatePlanEntitlementAsync(
    new CreatePlanEntitlementRequestBody
    {
        FeatureId = "feature_id",
        PlanId = "plan_id",
        ValueType = CreatePlanEntitlementRequestBodyValueType.Boolean,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreatePlanEntitlementRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">GetPlanEntitlementAsync</a>(planEntitlementId) -> GetPlanEntitlementResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.GetPlanEntitlementAsync("plan_entitlement_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**planEntitlementId:** `string` — plan_entitlement_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">UpdatePlanEntitlementAsync</a>(planEntitlementId, UpdatePlanEntitlementRequestBody { ... }) -> UpdatePlanEntitlementResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.UpdatePlanEntitlementAsync(
    "plan_entitlement_id",
    new UpdatePlanEntitlementRequestBody
    {
        ValueType = UpdatePlanEntitlementRequestBodyValueType.Boolean,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**planEntitlementId:** `string` — plan_entitlement_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdatePlanEntitlementRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">DeletePlanEntitlementAsync</a>(planEntitlementId) -> DeletePlanEntitlementResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.DeletePlanEntitlementAsync("plan_entitlement_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**planEntitlementId:** `string` — plan_entitlement_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CountPlanEntitlementsAsync</a>(CountPlanEntitlementsRequest { ... }) -> CountPlanEntitlementsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.CountPlanEntitlementsAsync(new CountPlanEntitlementsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountPlanEntitlementsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">GetFeatureUsageByCompanyAsync</a>(GetFeatureUsageByCompanyRequest { ... }) -> GetFeatureUsageByCompanyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.GetFeatureUsageByCompanyAsync(
    new GetFeatureUsageByCompanyRequest
    {
        Keys = new Dictionary<string, object>()
        {
            {
                "string",
                new Dictionary<object, object?>() { { "key", "value" } }
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetFeatureUsageByCompanyRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## components
<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">ListComponentsAsync</a>(ListComponentsRequest { ... }) -> ListComponentsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.ListComponentsAsync(new ListComponentsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListComponentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">CreateComponentAsync</a>(CreateComponentRequestBody { ... }) -> CreateComponentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.CreateComponentAsync(
    new CreateComponentRequestBody
    {
        EntityType = CreateComponentRequestBodyEntityType.Entitlement,
        Name = "name",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateComponentRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">GetComponentAsync</a>(componentId) -> GetComponentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.GetComponentAsync("component_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**componentId:** `string` — component_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">UpdateComponentAsync</a>(componentId, UpdateComponentRequestBody { ... }) -> UpdateComponentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.UpdateComponentAsync("component_id", new UpdateComponentRequestBody());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**componentId:** `string` — component_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateComponentRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">DeleteComponentAsync</a>(componentId) -> DeleteComponentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.DeleteComponentAsync("component_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**componentId:** `string` — component_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">CountComponentsAsync</a>(CountComponentsRequest { ... }) -> CountComponentsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.CountComponentsAsync(new CountComponentsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountComponentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">PreviewComponentDataAsync</a>(PreviewComponentDataRequest { ... }) -> PreviewComponentDataResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.PreviewComponentDataAsync(new PreviewComponentDataRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PreviewComponentDataRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## crm
<details><summary><code>client.Crm.<a href="/src/SchematicHQ.Client/Crm/CrmClient.cs">UpsertDealLineItemAssociationAsync</a>(CreateCrmDealLineItemAssociationRequestBody { ... }) -> UpsertDealLineItemAssociationResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Crm.UpsertDealLineItemAssociationAsync(
    new CreateCrmDealLineItemAssociationRequestBody
    {
        DealExternalId = "deal_external_id",
        LineItemExternalId = "line_item_external_id",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateCrmDealLineItemAssociationRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Crm.<a href="/src/SchematicHQ.Client/Crm/CrmClient.cs">UpsertLineItemAsync</a>(CreateCrmLineItemRequestBody { ... }) -> UpsertLineItemResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Crm.UpsertLineItemAsync(
    new CreateCrmLineItemRequestBody
    {
        Amount = "amount",
        Interval = "interval",
        LineItemExternalId = "line_item_external_id",
        ProductExternalId = "product_external_id",
        Quantity = 1,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateCrmLineItemRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Crm.<a href="/src/SchematicHQ.Client/Crm/CrmClient.cs">UpsertCrmDealAsync</a>(CreateCrmDealRequestBody { ... }) -> UpsertCrmDealResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Crm.UpsertCrmDealAsync(
    new CreateCrmDealRequestBody
    {
        CrmCompanyKey = "crm_company_key",
        CrmType = "crm_type",
        DealExternalId = "deal_external_id",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateCrmDealRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Crm.<a href="/src/SchematicHQ.Client/Crm/CrmClient.cs">ListCrmProductsAsync</a>(ListCrmProductsRequest { ... }) -> ListCrmProductsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Crm.ListCrmProductsAsync(new ListCrmProductsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListCrmProductsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Crm.<a href="/src/SchematicHQ.Client/Crm/CrmClient.cs">UpsertCrmProductAsync</a>(CreateCrmProductRequestBody { ... }) -> UpsertCrmProductResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Crm.UpsertCrmProductAsync(
    new CreateCrmProductRequestBody
    {
        Currency = "currency",
        Description = "description",
        ExternalId = "external_id",
        Interval = "interval",
        Name = "name",
        Price = "price",
        Quantity = 1,
        Sku = "sku",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateCrmProductRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## events
<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">CreateEventBatchAsync</a>(CreateEventBatchRequestBody { ... }) -> CreateEventBatchResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Events.CreateEventBatchAsync(
    new CreateEventBatchRequestBody
    {
        Events = new List<CreateEventRequestBody>()
        {
            new CreateEventRequestBody { EventType = CreateEventRequestBodyEventType.Identify },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateEventBatchRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">GetEventSummariesAsync</a>(GetEventSummariesRequest { ... }) -> GetEventSummariesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Events.GetEventSummariesAsync(new GetEventSummariesRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetEventSummariesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">GetEventSummaryBySubtypeAsync</a>(key) -> GetEventSummaryBySubtypeResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Events.GetEventSummaryBySubtypeAsync("key");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**key:** `string` — key
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">ListEventsAsync</a>(ListEventsRequest { ... }) -> ListEventsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Events.ListEventsAsync(new ListEventsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListEventsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">CreateEventAsync</a>(CreateEventRequestBody { ... }) -> CreateEventResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Events.CreateEventAsync(
    new CreateEventRequestBody { EventType = CreateEventRequestBodyEventType.Identify }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateEventRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">GetEventAsync</a>(eventId) -> GetEventResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Events.GetEventAsync("event_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**eventId:** `string` — event_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">GetSegmentIntegrationStatusAsync</a>() -> GetSegmentIntegrationStatusResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Events.GetSegmentIntegrationStatusAsync();
```
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## plans
<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">GetAudienceAsync</a>(planAudienceId) -> GetAudienceResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.GetAudienceAsync("plan_audience_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**planAudienceId:** `string` — plan_audience_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">UpdateAudienceAsync</a>(planAudienceId, UpdateAudienceRequestBody { ... }) -> UpdateAudienceResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.UpdateAudienceAsync(
    "plan_audience_id",
    new UpdateAudienceRequestBody
    {
        ConditionGroups = new List<CreateOrUpdateConditionGroupRequestBody>()
        {
            new CreateOrUpdateConditionGroupRequestBody
            {
                Conditions = new List<CreateOrUpdateConditionRequestBody>()
                {
                    new CreateOrUpdateConditionRequestBody
                    {
                        ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
                        Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                        ResourceIds = new List<string>() { "resource_ids" },
                    },
                },
            },
        },
        Conditions = new List<CreateOrUpdateConditionRequestBody>()
        {
            new CreateOrUpdateConditionRequestBody
            {
                ConditionType = CreateOrUpdateConditionRequestBodyConditionType.Company,
                Operator = CreateOrUpdateConditionRequestBodyOperator.Eq,
                ResourceIds = new List<string>() { "resource_ids" },
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**planAudienceId:** `string` — plan_audience_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateAudienceRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">DeleteAudienceAsync</a>(planAudienceId) -> DeleteAudienceResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.DeleteAudienceAsync("plan_audience_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**planAudienceId:** `string` — plan_audience_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">ListPlansAsync</a>(ListPlansRequest { ... }) -> ListPlansResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.ListPlansAsync(new ListPlansRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListPlansRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">CreatePlanAsync</a>(CreatePlanRequestBody { ... }) -> CreatePlanResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.CreatePlanAsync(
    new CreatePlanRequestBody
    {
        Description = "description",
        Name = "name",
        PlanType = CreatePlanRequestBodyPlanType.Plan,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreatePlanRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">GetPlanAsync</a>(planId) -> GetPlanResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.GetPlanAsync("plan_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**planId:** `string` — plan_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">UpdatePlanAsync</a>(planId, UpdatePlanRequestBody { ... }) -> UpdatePlanResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.UpdatePlanAsync("plan_id", new UpdatePlanRequestBody { Name = "name" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**planId:** `string` — plan_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdatePlanRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">DeletePlanAsync</a>(planId) -> DeletePlanResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.DeletePlanAsync("plan_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**planId:** `string` — plan_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">UpsertBillingProductPlanAsync</a>(planId, UpsertBillingProductRequestBody { ... }) -> UpsertBillingProductPlanResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.UpsertBillingProductPlanAsync("plan_id", new UpsertBillingProductRequestBody());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**planId:** `string` — plan_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpsertBillingProductRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">CountPlansAsync</a>(CountPlansRequest { ... }) -> CountPlansResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.CountPlansAsync(new CountPlansRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountPlansRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## plangroups
<details><summary><code>client.Plangroups.<a href="/src/SchematicHQ.Client/Plangroups/PlangroupsClient.cs">GetPlanGroupAsync</a>() -> GetPlanGroupResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plangroups.GetPlanGroupAsync();
```
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plangroups.<a href="/src/SchematicHQ.Client/Plangroups/PlangroupsClient.cs">CreatePlanGroupAsync</a>(CreatePlanGroupRequestBody { ... }) -> CreatePlanGroupResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plangroups.CreatePlanGroupAsync(
    new CreatePlanGroupRequestBody
    {
        AddOnIds = new List<string>() { "add_on_ids" },
        PlanIds = new List<string>() { "plan_ids" },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreatePlanGroupRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plangroups.<a href="/src/SchematicHQ.Client/Plangroups/PlangroupsClient.cs">UpdatePlanGroupAsync</a>(planGroupId, UpdatePlanGroupRequestBody { ... }) -> UpdatePlanGroupResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plangroups.UpdatePlanGroupAsync(
    "plan_group_id",
    new UpdatePlanGroupRequestBody
    {
        AddOnIds = new List<string>() { "add_on_ids" },
        PlanIds = new List<string>() { "plan_ids" },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**planGroupId:** `string` — plan_group_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdatePlanGroupRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## accesstokens
<details><summary><code>client.Accesstokens.<a href="/src/SchematicHQ.Client/Accesstokens/AccesstokensClient.cs">IssueTemporaryAccessTokenAsync</a>(IssueTemporaryAccessTokenRequestBody { ... }) -> IssueTemporaryAccessTokenResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accesstokens.IssueTemporaryAccessTokenAsync(
    new IssueTemporaryAccessTokenRequestBody
    {
        Lookup = new Dictionary<string, string>() { { "key", "value" } },
        ResourceType = "resource_type",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `IssueTemporaryAccessTokenRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## webhooks
<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">ListWebhookEventsAsync</a>(ListWebhookEventsRequest { ... }) -> ListWebhookEventsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.ListWebhookEventsAsync(new ListWebhookEventsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListWebhookEventsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">GetWebhookEventAsync</a>(webhookEventId) -> GetWebhookEventResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.GetWebhookEventAsync("webhook_event_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**webhookEventId:** `string` — webhook_event_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">CountWebhookEventsAsync</a>(CountWebhookEventsRequest { ... }) -> CountWebhookEventsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.CountWebhookEventsAsync(new CountWebhookEventsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountWebhookEventsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">ListWebhooksAsync</a>(ListWebhooksRequest { ... }) -> ListWebhooksResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.ListWebhooksAsync(new ListWebhooksRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListWebhooksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">CreateWebhookAsync</a>(CreateWebhookRequestBody { ... }) -> CreateWebhookResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.CreateWebhookAsync(
    new CreateWebhookRequestBody
    {
        Name = "name",
        RequestTypes = new List<CreateWebhookRequestBodyRequestTypesItem>()
        {
            CreateWebhookRequestBodyRequestTypesItem.CompanyUpdated,
        },
        Url = "url",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateWebhookRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">GetWebhookAsync</a>(webhookId) -> GetWebhookResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.GetWebhookAsync("webhook_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**webhookId:** `string` — webhook_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">UpdateWebhookAsync</a>(webhookId, UpdateWebhookRequestBody { ... }) -> UpdateWebhookResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.UpdateWebhookAsync("webhook_id", new UpdateWebhookRequestBody());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**webhookId:** `string` — webhook_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateWebhookRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">DeleteWebhookAsync</a>(webhookId) -> DeleteWebhookResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.DeleteWebhookAsync("webhook_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**webhookId:** `string` — webhook_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">CountWebhooksAsync</a>(CountWebhooksRequest { ... }) -> CountWebhooksResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.CountWebhooksAsync(new CountWebhooksRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CountWebhooksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>
