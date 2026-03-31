# Reference
<details><summary><code>client.<a href="/src/SchematicHQ.Client/Schematic.cs">PutPlanAudiencesPlanAudienceIdAsync</a>(planAudienceId)</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.PutPlanAudiencesPlanAudienceIdAsync("plan_audience_id");
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

**planAudienceId:** `string` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.<a href="/src/SchematicHQ.Client/Schematic.cs">DeletePlanAudiencesPlanAudienceIdAsync</a>(planAudienceId)</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.DeletePlanAudiencesPlanAudienceIdAsync("plan_audience_id");
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

**planAudienceId:** `string` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## accounts
<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">ListApiKeysAsync</a>(ListApiKeysRequest { ... }) -> WithRawResponseTask&lt;ListApiKeysResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.ListApiKeysAsync(
    new ListApiKeysRequest
    {
        EnvironmentId = "environment_id",
        RequireEnvironment = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListApiKeysRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">CreateApiKeyAsync</a>(CreateApiKeyRequestBody { ... }) -> WithRawResponseTask&lt;CreateApiKeyResponse&gt;</code></summary>
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">GetApiKeyAsync</a>(apiKeyId) -> WithRawResponseTask&lt;GetApiKeyResponse&gt;</code></summary>
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">UpdateApiKeyAsync</a>(apiKeyId, UpdateApiKeyRequestBody { ... }) -> WithRawResponseTask&lt;UpdateApiKeyResponse&gt;</code></summary>
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">DeleteApiKeyAsync</a>(apiKeyId) -> WithRawResponseTask&lt;DeleteApiKeyResponse&gt;</code></summary>
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">CountApiKeysAsync</a>(CountApiKeysRequest { ... }) -> WithRawResponseTask&lt;CountApiKeysResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.CountApiKeysAsync(
    new CountApiKeysRequest
    {
        EnvironmentId = "environment_id",
        RequireEnvironment = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountApiKeysRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">ListAuditLogsAsync</a>(ListAuditLogsRequest { ... }) -> WithRawResponseTask&lt;ListAuditLogsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.ListAuditLogsAsync(
    new ListAuditLogsRequest
    {
        ActorType = ActorType.ApiKey,
        EndTime = new DateTime(2024, 01, 15, 09, 30, 00, 000),
        EnvironmentId = "environment_id",
        Q = "q",
        StartTime = new DateTime(2024, 01, 15, 09, 30, 00, 000),
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListAuditLogsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">GetAuditLogAsync</a>(auditLogId) -> WithRawResponseTask&lt;GetAuditLogResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.GetAuditLogAsync("audit_log_id");
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

**auditLogId:** `string` — audit_log_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">CountAuditLogsAsync</a>(CountAuditLogsRequest { ... }) -> WithRawResponseTask&lt;CountAuditLogsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.CountAuditLogsAsync(
    new CountAuditLogsRequest
    {
        ActorType = ActorType.ApiKey,
        EndTime = new DateTime(2024, 01, 15, 09, 30, 00, 000),
        EnvironmentId = "environment_id",
        Q = "q",
        StartTime = new DateTime(2024, 01, 15, 09, 30, 00, 000),
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountAuditLogsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">ListEnvironmentsAsync</a>(ListEnvironmentsRequest { ... }) -> WithRawResponseTask&lt;ListEnvironmentsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.ListEnvironmentsAsync(
    new ListEnvironmentsRequest { Limit = 1000000, Offset = 1000000 }
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

**request:** `ListEnvironmentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">CreateEnvironmentAsync</a>(CreateEnvironmentRequestBody { ... }) -> WithRawResponseTask&lt;CreateEnvironmentResponse&gt;</code></summary>
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
        EnvironmentType = EnvironmentType.Development,
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">GetEnvironmentAsync</a>(environmentId) -> WithRawResponseTask&lt;GetEnvironmentResponse&gt;</code></summary>
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">UpdateEnvironmentAsync</a>(environmentId, UpdateEnvironmentRequestBody { ... }) -> WithRawResponseTask&lt;UpdateEnvironmentResponse&gt;</code></summary>
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">DeleteEnvironmentAsync</a>(environmentId) -> WithRawResponseTask&lt;DeleteEnvironmentResponse&gt;</code></summary>
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">QuickstartAsync</a>() -> WithRawResponseTask&lt;QuickstartResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.QuickstartAsync();
```
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">GetWhoAmIAsync</a>() -> WithRawResponseTask&lt;GetWhoAmIResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.GetWhoAmIAsync();
```
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## billing
<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListCouponsAsync</a>(ListCouponsRequest { ... }) -> WithRawResponseTask&lt;ListCouponsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListCouponsAsync(
    new ListCouponsRequest
    {
        IsActive = true,
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListCouponsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingCouponAsync</a>(CreateCouponRequestBody { ... }) -> WithRawResponseTask&lt;UpsertBillingCouponResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.UpsertBillingCouponAsync(
    new CreateCouponRequestBody
    {
        AmountOff = 1000000,
        Duration = "duration",
        DurationInMonths = 1000000,
        ExternalId = "external_id",
        MaxRedemptions = 1000000,
        Name = "name",
        PercentOff = 1.1,
        TimesRedeemed = 1000000,
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

**request:** `CreateCouponRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingCustomerAsync</a>(CreateBillingCustomerRequestBody { ... }) -> WithRawResponseTask&lt;UpsertBillingCustomerResponse&gt;</code></summary>
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListCustomersWithSubscriptionsAsync</a>(ListCustomersWithSubscriptionsRequest { ... }) -> WithRawResponseTask&lt;ListCustomersWithSubscriptionsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListCustomersWithSubscriptionsAsync(
    new ListCustomersWithSubscriptionsRequest
    {
        Name = "name",
        ProviderType = BillingProviderType.Schematic,
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListCustomersWithSubscriptionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">CountCustomersAsync</a>(CountCustomersRequest { ... }) -> WithRawResponseTask&lt;CountCustomersResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.CountCustomersAsync(
    new CountCustomersRequest
    {
        Name = "name",
        ProviderType = BillingProviderType.Schematic,
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountCustomersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListInvoicesAsync</a>(ListInvoicesRequest { ... }) -> WithRawResponseTask&lt;ListInvoicesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListInvoicesAsync(
    new ListInvoicesRequest
    {
        CompanyId = "company_id",
        CustomerExternalId = "customer_external_id",
        SubscriptionExternalId = "subscription_external_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListInvoicesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertInvoiceAsync</a>(CreateInvoiceRequestBody { ... }) -> WithRawResponseTask&lt;UpsertInvoiceResponse&gt;</code></summary>
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
        AmountDue = 1000000,
        AmountPaid = 1000000,
        AmountRemaining = 1000000,
        CollectionMethod = "collection_method",
        Currency = "currency",
        CustomerExternalId = "customer_external_id",
        Subtotal = 1000000,
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListMetersAsync</a>(ListMetersRequest { ... }) -> WithRawResponseTask&lt;ListMetersResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListMetersAsync(
    new ListMetersRequest
    {
        DisplayName = "display_name",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListMetersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingMeterAsync</a>(CreateMeterRequestBody { ... }) -> WithRawResponseTask&lt;UpsertBillingMeterResponse&gt;</code></summary>
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListPaymentMethodsAsync</a>(ListPaymentMethodsRequest { ... }) -> WithRawResponseTask&lt;ListPaymentMethodsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListPaymentMethodsAsync(
    new ListPaymentMethodsRequest
    {
        CompanyId = "company_id",
        CustomerExternalId = "customer_external_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListPaymentMethodsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertPaymentMethodAsync</a>(CreatePaymentMethodRequestBody { ... }) -> WithRawResponseTask&lt;UpsertPaymentMethodResponse&gt;</code></summary>
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListBillingPricesAsync</a>(ListBillingPricesRequest { ... }) -> WithRawResponseTask&lt;ListBillingPricesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListBillingPricesAsync(
    new ListBillingPricesRequest
    {
        Currency = "currency",
        ForInitialPlan = true,
        ForTrialExpiryPlan = true,
        Interval = "interval",
        IsActive = true,
        Price = 1000000,
        ProductId = "product_id",
        ProviderType = BillingProviderType.Schematic,
        Q = "q",
        TiersMode = BillingTiersMode.Graduated,
        UsageType = BillingPriceUsageType.Licensed,
        WithMeter = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListBillingPricesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingPriceAsync</a>(CreateBillingPriceRequestBody { ... }) -> WithRawResponseTask&lt;UpsertBillingPriceResponse&gt;</code></summary>
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
        BillingScheme = BillingPriceScheme.PerUnit,
        Currency = "currency",
        ExternalAccountId = "external_account_id",
        Interval = "interval",
        IsActive = true,
        Price = 1000000,
        PriceExternalId = "price_external_id",
        PriceTiers = new List<CreateBillingPriceTierRequestBody>()
        {
            new CreateBillingPriceTierRequestBody { PriceExternalId = "price_external_id" },
        },
        ProductExternalId = "product_external_id",
        UsageType = BillingPriceUsageType.Licensed,
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">DeleteBillingProductAsync</a>(billingId) -> WithRawResponseTask&lt;DeleteBillingProductResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.DeleteBillingProductAsync("billing_id");
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListBillingProductPricesAsync</a>(ListBillingProductPricesRequest { ... }) -> WithRawResponseTask&lt;ListBillingProductPricesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListBillingProductPricesAsync(
    new ListBillingProductPricesRequest
    {
        Currency = "currency",
        ForInitialPlan = true,
        ForTrialExpiryPlan = true,
        Interval = "interval",
        IsActive = true,
        Price = 1000000,
        ProductId = "product_id",
        ProviderType = BillingProviderType.Schematic,
        Q = "q",
        TiersMode = BillingTiersMode.Graduated,
        UsageType = BillingPriceUsageType.Licensed,
        WithMeter = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListBillingProductPricesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">DeleteProductPriceAsync</a>(billingId) -> WithRawResponseTask&lt;DeleteProductPriceResponse&gt;</code></summary>
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingProductAsync</a>(CreateBillingProductRequestBody { ... }) -> WithRawResponseTask&lt;UpsertBillingProductResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.UpsertBillingProductAsync(
    new CreateBillingProductRequestBody { ExternalId = "external_id", Price = 1.1 }
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListBillingProductsAsync</a>(ListBillingProductsRequest { ... }) -> WithRawResponseTask&lt;ListBillingProductsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListBillingProductsAsync(
    new ListBillingProductsRequest
    {
        IsActive = true,
        Name = "name",
        PriceUsageType = BillingPriceUsageType.Licensed,
        ProviderType = BillingProviderType.Schematic,
        Q = "q",
        WithOneTimeCharges = true,
        WithPricesOnly = true,
        WithZeroPrice = true,
        WithoutLinkedToPlan = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListBillingProductsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">CountBillingProductsAsync</a>(CountBillingProductsRequest { ... }) -> WithRawResponseTask&lt;CountBillingProductsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.CountBillingProductsAsync(
    new CountBillingProductsRequest
    {
        IsActive = true,
        Name = "name",
        PriceUsageType = BillingPriceUsageType.Licensed,
        ProviderType = BillingProviderType.Schematic,
        Q = "q",
        WithOneTimeCharges = true,
        WithPricesOnly = true,
        WithZeroPrice = true,
        WithoutLinkedToPlan = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountBillingProductsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingSubscriptionAsync</a>(CreateBillingSubscriptionRequestBody { ... }) -> WithRawResponseTask&lt;UpsertBillingSubscriptionResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.UpsertBillingSubscriptionAsync(
    new CreateBillingSubscriptionRequestBody
    {
        CancelAtPeriodEnd = true,
        Currency = "currency",
        CustomerExternalId = "customer_external_id",
        Discounts = new List<BillingSubscriptionDiscount>()
        {
            new BillingSubscriptionDiscount
            {
                CouponExternalId = "coupon_external_id",
                ExternalId = "external_id",
                IsActive = true,
                StartedAt = new DateTime(2024, 01, 15, 09, 30, 00, 000),
            },
        },
        ExpiredAt = new DateTime(2024, 01, 15, 09, 30, 00, 000),
        ProductExternalIds = new List<BillingProductPricing>()
        {
            new BillingProductPricing
            {
                Currency = "currency",
                Interval = "interval",
                Price = 1000000,
                PriceExternalId = "price_external_id",
                ProductExternalId = "product_external_id",
                Quantity = 1000000,
                UsageType = BillingPriceUsageType.Licensed,
            },
        },
        SubscriptionExternalId = "subscription_external_id",
        TotalPrice = 1000000,
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

**request:** `CreateBillingSubscriptionRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## credits
<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ListBillingCreditsAsync</a>(ListBillingCreditsRequest { ... }) -> WithRawResponseTask&lt;ListBillingCreditsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.ListBillingCreditsAsync(
    new ListBillingCreditsRequest
    {
        Name = "name",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListBillingCreditsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CreateBillingCreditAsync</a>(CreateBillingCreditRequestBody { ... }) -> WithRawResponseTask&lt;CreateBillingCreditResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.CreateBillingCreditAsync(
    new CreateBillingCreditRequestBody
    {
        Currency = "currency",
        Description = "description",
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

**request:** `CreateBillingCreditRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">GetSingleBillingCreditAsync</a>(creditId) -> WithRawResponseTask&lt;GetSingleBillingCreditResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.GetSingleBillingCreditAsync("credit_id");
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

**creditId:** `string` — credit_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">UpdateBillingCreditAsync</a>(creditId, UpdateBillingCreditRequestBody { ... }) -> WithRawResponseTask&lt;UpdateBillingCreditResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.UpdateBillingCreditAsync(
    "credit_id",
    new UpdateBillingCreditRequestBody { Description = "description", Name = "name" }
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

**creditId:** `string` — credit_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateBillingCreditRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">SoftDeleteBillingCreditAsync</a>(creditId) -> WithRawResponseTask&lt;SoftDeleteBillingCreditResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.SoftDeleteBillingCreditAsync("credit_id");
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

**creditId:** `string` — credit_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ListCreditBundlesAsync</a>(ListCreditBundlesRequest { ... }) -> WithRawResponseTask&lt;ListCreditBundlesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.ListCreditBundlesAsync(
    new ListCreditBundlesRequest
    {
        CreditId = "credit_id",
        Status = BillingCreditBundleStatus.Active,
        BundleType = "fixed",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListCreditBundlesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CreateCreditBundleAsync</a>(CreateCreditBundleRequestBody { ... }) -> WithRawResponseTask&lt;CreateCreditBundleResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.CreateCreditBundleAsync(
    new CreateCreditBundleRequestBody
    {
        BundleName = "bundle_name",
        CreditId = "credit_id",
        Currency = "currency",
        PricePerUnit = 1000000,
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

**request:** `CreateCreditBundleRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">GetCreditBundleAsync</a>(bundleId) -> WithRawResponseTask&lt;GetCreditBundleResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.GetCreditBundleAsync("bundle_id");
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

**bundleId:** `string` — bundle_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">UpdateCreditBundleDetailsAsync</a>(bundleId, UpdateCreditBundleDetailsRequestBody { ... }) -> WithRawResponseTask&lt;UpdateCreditBundleDetailsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.UpdateCreditBundleDetailsAsync(
    "bundle_id",
    new UpdateCreditBundleDetailsRequestBody { BundleName = "bundle_name", PricePerUnit = 1000000 }
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

**bundleId:** `string` — bundle_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateCreditBundleDetailsRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">DeleteCreditBundleAsync</a>(bundleId) -> WithRawResponseTask&lt;DeleteCreditBundleResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.DeleteCreditBundleAsync("bundle_id");
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

**bundleId:** `string` — bundle_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CountCreditBundlesAsync</a>(CountCreditBundlesRequest { ... }) -> WithRawResponseTask&lt;CountCreditBundlesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.CountCreditBundlesAsync(
    new CountCreditBundlesRequest
    {
        CreditId = "credit_id",
        Status = BillingCreditBundleStatus.Active,
        BundleType = "fixed",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountCreditBundlesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CountBillingCreditsAsync</a>(CountBillingCreditsRequest { ... }) -> WithRawResponseTask&lt;CountBillingCreditsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.CountBillingCreditsAsync(
    new CountBillingCreditsRequest
    {
        Name = "name",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountBillingCreditsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ZeroOutGrantAsync</a>(grantId, ZeroOutGrantRequestBody { ... }) -> WithRawResponseTask&lt;ZeroOutGrantResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.ZeroOutGrantAsync("grant_id", new ZeroOutGrantRequestBody());
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

**grantId:** `string` — grant_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `ZeroOutGrantRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">GrantBillingCreditsToCompanyAsync</a>(CreateCompanyCreditGrant { ... }) -> WithRawResponseTask&lt;GrantBillingCreditsToCompanyResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.GrantBillingCreditsToCompanyAsync(
    new CreateCompanyCreditGrant
    {
        CompanyId = "company_id",
        CreditId = "credit_id",
        Quantity = 1000000,
        Reason = BillingCreditGrantReason.BillingCreditAutoTopup,
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

**request:** `CreateCompanyCreditGrant` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CountCompanyGrantsAsync</a>(CountCompanyGrantsRequest { ... }) -> WithRawResponseTask&lt;CountCompanyGrantsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.CountCompanyGrantsAsync(
    new CountCompanyGrantsRequest
    {
        CompanyId = "company_id",
        Order = CreditGrantSortOrder.CreatedAt,
        Dir = SortDirection.Asc,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountCompanyGrantsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ListCompanyGrantsAsync</a>(ListCompanyGrantsRequest { ... }) -> WithRawResponseTask&lt;ListCompanyGrantsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.ListCompanyGrantsAsync(
    new ListCompanyGrantsRequest
    {
        CompanyId = "company_id",
        Order = CreditGrantSortOrder.CreatedAt,
        Dir = SortDirection.Asc,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListCompanyGrantsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CountBillingCreditsGrantsAsync</a>(CountBillingCreditsGrantsRequest { ... }) -> WithRawResponseTask&lt;CountBillingCreditsGrantsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.CountBillingCreditsGrantsAsync(
    new CountBillingCreditsGrantsRequest
    {
        CreditId = "credit_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountBillingCreditsGrantsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ListGrantsForCreditAsync</a>(ListGrantsForCreditRequest { ... }) -> WithRawResponseTask&lt;ListGrantsForCreditResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.ListGrantsForCreditAsync(
    new ListGrantsForCreditRequest
    {
        CreditId = "credit_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListGrantsForCreditRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">GetEnrichedCreditLedgerAsync</a>(GetEnrichedCreditLedgerRequest { ... }) -> WithRawResponseTask&lt;GetEnrichedCreditLedgerResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.GetEnrichedCreditLedgerAsync(
    new GetEnrichedCreditLedgerRequest
    {
        CompanyId = "company_id",
        BillingCreditId = "billing_credit_id",
        FeatureId = "feature_id",
        Period = CreditLedgerPeriod.Daily,
        StartTime = "start_time",
        EndTime = "end_time",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `GetEnrichedCreditLedgerRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CountCreditLedgerAsync</a>(CountCreditLedgerRequest { ... }) -> WithRawResponseTask&lt;CountCreditLedgerResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.CountCreditLedgerAsync(
    new CountCreditLedgerRequest
    {
        CompanyId = "company_id",
        BillingCreditId = "billing_credit_id",
        FeatureId = "feature_id",
        Period = CreditLedgerPeriod.Daily,
        StartTime = "start_time",
        EndTime = "end_time",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountCreditLedgerRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ListBillingPlanCreditGrantsAsync</a>(ListBillingPlanCreditGrantsRequest { ... }) -> WithRawResponseTask&lt;ListBillingPlanCreditGrantsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.ListBillingPlanCreditGrantsAsync(
    new ListBillingPlanCreditGrantsRequest
    {
        CreditId = "credit_id",
        PlanId = "plan_id",
        PlanVersionId = "plan_version_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListBillingPlanCreditGrantsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CreateBillingPlanCreditGrantAsync</a>(CreateBillingPlanCreditGrantRequestBody { ... }) -> WithRawResponseTask&lt;CreateBillingPlanCreditGrantResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.CreateBillingPlanCreditGrantAsync(
    new CreateBillingPlanCreditGrantRequestBody
    {
        CreditAmount = 1000000,
        CreditId = "credit_id",
        PlanId = "plan_id",
        ResetCadence = BillingPlanCreditGrantResetCadence.Daily,
        ResetStart = BillingPlanCreditGrantResetStart.BillingPeriod,
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

**request:** `CreateBillingPlanCreditGrantRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">UpdateBillingPlanCreditGrantAsync</a>(planGrantId, UpdateBillingPlanCreditGrantRequestBody { ... }) -> WithRawResponseTask&lt;UpdateBillingPlanCreditGrantResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.UpdateBillingPlanCreditGrantAsync(
    "plan_grant_id",
    new UpdateBillingPlanCreditGrantRequestBody
    {
        ResetCadence = BillingPlanCreditGrantResetCadence.Daily,
        ResetStart = BillingPlanCreditGrantResetStart.BillingPeriod,
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

**planGrantId:** `string` — plan_grant_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateBillingPlanCreditGrantRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">DeleteBillingPlanCreditGrantAsync</a>(planGrantId, DeleteBillingPlanCreditGrantRequest { ... }) -> WithRawResponseTask&lt;DeleteBillingPlanCreditGrantResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.DeleteBillingPlanCreditGrantAsync(
    "plan_grant_id",
    new DeleteBillingPlanCreditGrantRequest { ApplyToExisting = true }
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

**planGrantId:** `string` — plan_grant_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `DeleteBillingPlanCreditGrantRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CountBillingPlanCreditGrantsAsync</a>(CountBillingPlanCreditGrantsRequest { ... }) -> WithRawResponseTask&lt;CountBillingPlanCreditGrantsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.CountBillingPlanCreditGrantsAsync(
    new CountBillingPlanCreditGrantsRequest
    {
        CreditId = "credit_id",
        PlanId = "plan_id",
        PlanVersionId = "plan_version_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountBillingPlanCreditGrantsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ListCreditEventLedgerAsync</a>(ListCreditEventLedgerRequest { ... }) -> WithRawResponseTask&lt;ListCreditEventLedgerResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.ListCreditEventLedgerAsync(
    new ListCreditEventLedgerRequest
    {
        BillingCreditId = "billing_credit_id",
        CompanyId = "company_id",
        EndTime = "end_time",
        EventType = CreditEventType.Grant,
        FeatureId = "feature_id",
        StartTime = "start_time",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListCreditEventLedgerRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CountCreditEventLedgerAsync</a>(CountCreditEventLedgerRequest { ... }) -> WithRawResponseTask&lt;CountCreditEventLedgerResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.CountCreditEventLedgerAsync(
    new CountCreditEventLedgerRequest
    {
        BillingCreditId = "billing_credit_id",
        CompanyId = "company_id",
        EndTime = "end_time",
        EventType = CreditEventType.Grant,
        FeatureId = "feature_id",
        StartTime = "start_time",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountCreditEventLedgerRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## checkout
<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">InternalAsync</a>(ChangeSubscriptionInternalRequestBody { ... }) -> WithRawResponseTask&lt;CheckoutInternalResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Checkout.InternalAsync(
    new ChangeSubscriptionInternalRequestBody
    {
        AddOnIds = new List<UpdateAddOnRequestBody>()
        {
            new UpdateAddOnRequestBody { AddOnId = "add_on_id", PriceId = "price_id" },
        },
        CompanyId = "company_id",
        CreditBundles = new List<UpdateCreditBundleRequestBody>()
        {
            new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1000000 },
        },
        NewPlanId = "new_plan_id",
        NewPriceId = "new_price_id",
        PayInAdvance = new List<UpdatePayInAdvanceRequestBody>()
        {
            new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
        },
        SkipTrial = true,
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

**request:** `ChangeSubscriptionInternalRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">GetCheckoutDataAsync</a>(CheckoutDataRequestBody { ... }) -> WithRawResponseTask&lt;GetCheckoutDataResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Checkout.GetCheckoutDataAsync(
    new CheckoutDataRequestBody { CompanyId = "company_id" }
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

**request:** `CheckoutDataRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">PreviewCheckoutInternalAsync</a>(ChangeSubscriptionInternalRequestBody { ... }) -> WithRawResponseTask&lt;PreviewCheckoutInternalResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Checkout.PreviewCheckoutInternalAsync(
    new ChangeSubscriptionInternalRequestBody
    {
        AddOnIds = new List<UpdateAddOnRequestBody>()
        {
            new UpdateAddOnRequestBody { AddOnId = "add_on_id", PriceId = "price_id" },
        },
        CompanyId = "company_id",
        CreditBundles = new List<UpdateCreditBundleRequestBody>()
        {
            new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1000000 },
        },
        NewPlanId = "new_plan_id",
        NewPriceId = "new_price_id",
        PayInAdvance = new List<UpdatePayInAdvanceRequestBody>()
        {
            new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
        },
        SkipTrial = true,
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

**request:** `ChangeSubscriptionInternalRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">ManagePlanAsync</a>(ManagePlanRequest { ... }) -> WithRawResponseTask&lt;ManagePlanResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Checkout.ManagePlanAsync(
    new ManagePlanRequest
    {
        AddOnSelections = new List<PlanSelection>() { new PlanSelection { PlanId = "plan_id" } },
        CompanyId = "company_id",
        CreditBundles = new List<UpdateCreditBundleRequestBody>()
        {
            new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1000000 },
        },
        PayInAdvanceEntitlements = new List<UpdatePayInAdvanceRequestBody>()
        {
            new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
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

**request:** `ManagePlanRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">PreviewManagePlanAsync</a>(ManagePlanRequest { ... }) -> WithRawResponseTask&lt;PreviewManagePlanResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Checkout.PreviewManagePlanAsync(
    new ManagePlanRequest
    {
        AddOnSelections = new List<PlanSelection>() { new PlanSelection { PlanId = "plan_id" } },
        CompanyId = "company_id",
        CreditBundles = new List<UpdateCreditBundleRequestBody>()
        {
            new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1000000 },
        },
        PayInAdvanceEntitlements = new List<UpdatePayInAdvanceRequestBody>()
        {
            new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
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

**request:** `ManagePlanRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">CancelSubscriptionAsync</a>(CancelSubscriptionRequest { ... }) -> WithRawResponseTask&lt;CancelSubscriptionResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Checkout.CancelSubscriptionAsync(
    new CancelSubscriptionRequest { CompanyId = "company_id" }
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

**request:** `CancelSubscriptionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">UpdateCustomerSubscriptionTrialEndAsync</a>(subscriptionId, UpdateTrialEndRequestBody { ... }) -> WithRawResponseTask&lt;UpdateCustomerSubscriptionTrialEndResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Checkout.UpdateCustomerSubscriptionTrialEndAsync(
    "subscription_id",
    new UpdateTrialEndRequestBody()
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

**subscriptionId:** `string` — subscription_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateTrialEndRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## companies
<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListCompaniesAsync</a>(ListCompaniesRequest { ... }) -> WithRawResponseTask&lt;ListCompaniesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListCompaniesAsync(
    new ListCompaniesRequest
    {
        MonetizedSubscriptions = true,
        PlanId = "plan_id",
        PlanVersionId = "plan_version_id",
        Q = "q",
        SortOrderColumn = "sort_order_column",
        SortOrderDirection = SortDirection.Asc,
        WithEntitlementFor = "with_entitlement_for",
        WithoutFeatureOverrideFor = "without_feature_override_for",
        WithoutPlan = true,
        WithoutSubscription = true,
        WithSubscription = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListCompaniesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpsertCompanyAsync</a>(UpsertCompanyRequestBody { ... }) -> WithRawResponseTask&lt;UpsertCompanyResponse&gt;</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetCompanyAsync</a>(companyId) -> WithRawResponseTask&lt;GetCompanyResponse&gt;</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteCompanyAsync</a>(companyId, DeleteCompanyRequest { ... }) -> WithRawResponseTask&lt;DeleteCompanyResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.DeleteCompanyAsync(
    "company_id",
    new DeleteCompanyRequest { CancelSubscription = true, Prorate = true }
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

**companyId:** `string` — company_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `DeleteCompanyRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountCompaniesAsync</a>(CountCompaniesRequest { ... }) -> WithRawResponseTask&lt;CountCompaniesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CountCompaniesAsync(
    new CountCompaniesRequest
    {
        MonetizedSubscriptions = true,
        PlanId = "plan_id",
        PlanVersionId = "plan_version_id",
        Q = "q",
        SortOrderColumn = "sort_order_column",
        SortOrderDirection = SortDirection.Asc,
        WithEntitlementFor = "with_entitlement_for",
        WithoutFeatureOverrideFor = "without_feature_override_for",
        WithoutPlan = true,
        WithoutSubscription = true,
        WithSubscription = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountCompaniesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CreateCompanyAsync</a>(UpsertCompanyRequestBody { ... }) -> WithRawResponseTask&lt;CreateCompanyResponse&gt;</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteCompanyByKeysAsync</a>(KeysRequestBody { ... }) -> WithRawResponseTask&lt;DeleteCompanyByKeysResponse&gt;</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">LookupCompanyAsync</a>(LookupCompanyRequest { ... }) -> WithRawResponseTask&lt;LookupCompanyResponse&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Company lookup is determined to resolve a company from its keys, similar to how many of our other apis work. 
The following approaches will all work to resolve a company and any of them are appropriate:
1. `/companies/lookup?keys={"foo": "bar", "fizz": "buzz"}`
2. `/companies/lookup?keys[foo]=bar&keys[fizz]=buzz`
2. `/companies/lookup?foo=bar&fizz=buzz`
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.LookupCompanyAsync(
    new LookupCompanyRequest { Keys = new Dictionary<string, string>() { { "keys", "keys" } } }
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListCompanyMembershipsAsync</a>(ListCompanyMembershipsRequest { ... }) -> WithRawResponseTask&lt;ListCompanyMembershipsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListCompanyMembershipsAsync(
    new ListCompanyMembershipsRequest
    {
        CompanyId = "company_id",
        UserId = "user_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListCompanyMembershipsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetOrCreateCompanyMembershipAsync</a>(GetOrCreateCompanyMembershipRequestBody { ... }) -> WithRawResponseTask&lt;GetOrCreateCompanyMembershipResponse&gt;</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteCompanyMembershipAsync</a>(companyMembershipId) -> WithRawResponseTask&lt;DeleteCompanyMembershipResponse&gt;</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetActiveCompanySubscriptionAsync</a>(GetActiveCompanySubscriptionRequest { ... }) -> WithRawResponseTask&lt;GetActiveCompanySubscriptionResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetActiveCompanySubscriptionAsync(
    new GetActiveCompanySubscriptionRequest
    {
        CompanyId = "company_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `GetActiveCompanySubscriptionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpsertCompanyTraitAsync</a>(UpsertTraitRequestBody { ... }) -> WithRawResponseTask&lt;UpsertCompanyTraitResponse&gt;</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListEntityKeyDefinitionsAsync</a>(ListEntityKeyDefinitionsRequest { ... }) -> WithRawResponseTask&lt;ListEntityKeyDefinitionsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListEntityKeyDefinitionsAsync(
    new ListEntityKeyDefinitionsRequest
    {
        EntityType = EntityType.Company,
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListEntityKeyDefinitionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountEntityKeyDefinitionsAsync</a>(CountEntityKeyDefinitionsRequest { ... }) -> WithRawResponseTask&lt;CountEntityKeyDefinitionsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CountEntityKeyDefinitionsAsync(
    new CountEntityKeyDefinitionsRequest
    {
        EntityType = EntityType.Company,
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountEntityKeyDefinitionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListEntityTraitDefinitionsAsync</a>(ListEntityTraitDefinitionsRequest { ... }) -> WithRawResponseTask&lt;ListEntityTraitDefinitionsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListEntityTraitDefinitionsAsync(
    new ListEntityTraitDefinitionsRequest
    {
        EntityType = EntityType.Company,
        Q = "q",
        TraitType = TraitType.Boolean,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListEntityTraitDefinitionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetOrCreateEntityTraitDefinitionAsync</a>(CreateEntityTraitDefinitionRequestBody { ... }) -> WithRawResponseTask&lt;GetOrCreateEntityTraitDefinitionResponse&gt;</code></summary>
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
        EntityType = EntityType.Company,
        Hierarchy = new List<string>() { "hierarchy" },
        TraitType = TraitType.Boolean,
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetEntityTraitDefinitionAsync</a>(entityTraitDefinitionId) -> WithRawResponseTask&lt;GetEntityTraitDefinitionResponse&gt;</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpdateEntityTraitDefinitionAsync</a>(entityTraitDefinitionId, UpdateEntityTraitDefinitionRequestBody { ... }) -> WithRawResponseTask&lt;UpdateEntityTraitDefinitionResponse&gt;</code></summary>
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
    new UpdateEntityTraitDefinitionRequestBody { TraitType = TraitType.Boolean }
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountEntityTraitDefinitionsAsync</a>(CountEntityTraitDefinitionsRequest { ... }) -> WithRawResponseTask&lt;CountEntityTraitDefinitionsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CountEntityTraitDefinitionsAsync(
    new CountEntityTraitDefinitionsRequest
    {
        EntityType = EntityType.Company,
        Q = "q",
        TraitType = TraitType.Boolean,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountEntityTraitDefinitionsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetEntityTraitValuesAsync</a>(GetEntityTraitValuesRequest { ... }) -> WithRawResponseTask&lt;GetEntityTraitValuesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetEntityTraitValuesAsync(
    new GetEntityTraitValuesRequest
    {
        DefinitionId = "definition_id",
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `GetEntityTraitValuesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListPlanChangesAsync</a>(ListPlanChangesRequest { ... }) -> WithRawResponseTask&lt;ListPlanChangesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListPlanChangesAsync(
    new ListPlanChangesRequest
    {
        Action = "action",
        BasePlanAction = "base_plan_action",
        CompanyId = "company_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListPlanChangesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetPlanChangeAsync</a>(planChangeId) -> WithRawResponseTask&lt;GetPlanChangeResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetPlanChangeAsync("plan_change_id");
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

**planChangeId:** `string` — plan_change_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListPlanTraitsAsync</a>(ListPlanTraitsRequest { ... }) -> WithRawResponseTask&lt;ListPlanTraitsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListPlanTraitsAsync(
    new ListPlanTraitsRequest
    {
        PlanId = "plan_id",
        TraitId = "trait_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListPlanTraitsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CreatePlanTraitAsync</a>(CreatePlanTraitRequestBody { ... }) -> WithRawResponseTask&lt;CreatePlanTraitResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CreatePlanTraitAsync(
    new CreatePlanTraitRequestBody
    {
        PlanId = "plan_id",
        TraitId = "trait_id",
        TraitValue = "trait_value",
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

**request:** `CreatePlanTraitRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetPlanTraitAsync</a>(planTraitId) -> WithRawResponseTask&lt;GetPlanTraitResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetPlanTraitAsync("plan_trait_id");
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

**planTraitId:** `string` — plan_trait_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpdatePlanTraitAsync</a>(planTraitId, UpdatePlanTraitRequestBody { ... }) -> WithRawResponseTask&lt;UpdatePlanTraitResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.UpdatePlanTraitAsync(
    "plan_trait_id",
    new UpdatePlanTraitRequestBody { PlanId = "plan_id", TraitValue = "trait_value" }
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

**planTraitId:** `string` — plan_trait_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdatePlanTraitRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeletePlanTraitAsync</a>(planTraitId) -> WithRawResponseTask&lt;DeletePlanTraitResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.DeletePlanTraitAsync("plan_trait_id");
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

**planTraitId:** `string` — plan_trait_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpdatePlanTraitsBulkAsync</a>(UpdatePlanTraitBulkRequestBody { ... }) -> WithRawResponseTask&lt;UpdatePlanTraitsBulkResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.UpdatePlanTraitsBulkAsync(
    new UpdatePlanTraitBulkRequestBody
    {
        ApplyToExistingCompanies = true,
        PlanId = "plan_id",
        Traits = new List<UpdatePlanTraitTraitRequestBody>()
        {
            new UpdatePlanTraitTraitRequestBody
            {
                TraitId = "trait_id",
                TraitValue = "trait_value",
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

**request:** `UpdatePlanTraitBulkRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountPlanTraitsAsync</a>(CountPlanTraitsRequest { ... }) -> WithRawResponseTask&lt;CountPlanTraitsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CountPlanTraitsAsync(
    new CountPlanTraitsRequest
    {
        PlanId = "plan_id",
        TraitId = "trait_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountPlanTraitsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpsertUserTraitAsync</a>(UpsertTraitRequestBody { ... }) -> WithRawResponseTask&lt;UpsertUserTraitResponse&gt;</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListUsersAsync</a>(ListUsersRequest { ... }) -> WithRawResponseTask&lt;ListUsersResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListUsersAsync(
    new ListUsersRequest
    {
        CompanyId = "company_id",
        PlanId = "plan_id",
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListUsersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpsertUserAsync</a>(UpsertUserRequestBody { ... }) -> WithRawResponseTask&lt;UpsertUserResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.UpsertUserAsync(
    new UpsertUserRequestBody { Keys = new Dictionary<string, string>() { { "key", "value" } } }
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetUserAsync</a>(userId) -> WithRawResponseTask&lt;GetUserResponse&gt;</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteUserAsync</a>(userId) -> WithRawResponseTask&lt;DeleteUserResponse&gt;</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountUsersAsync</a>(CountUsersRequest { ... }) -> WithRawResponseTask&lt;CountUsersResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CountUsersAsync(
    new CountUsersRequest
    {
        CompanyId = "company_id",
        PlanId = "plan_id",
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountUsersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CreateUserAsync</a>(UpsertUserRequestBody { ... }) -> WithRawResponseTask&lt;CreateUserResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CreateUserAsync(
    new UpsertUserRequestBody { Keys = new Dictionary<string, string>() { { "key", "value" } } }
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteUserByKeysAsync</a>(KeysRequestBody { ... }) -> WithRawResponseTask&lt;DeleteUserByKeysResponse&gt;</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">LookupUserAsync</a>(LookupUserRequest { ... }) -> WithRawResponseTask&lt;LookupUserResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.LookupUserAsync(
    new LookupUserRequest { Keys = new Dictionary<string, string>() { { "keys", "keys" } } }
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
<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListCompanyOverridesAsync</a>(ListCompanyOverridesRequest { ... }) -> WithRawResponseTask&lt;ListCompanyOverridesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.ListCompanyOverridesAsync(
    new ListCompanyOverridesRequest
    {
        CompanyId = "company_id",
        FeatureId = "feature_id",
        WithoutExpired = true,
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListCompanyOverridesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CreateCompanyOverrideAsync</a>(CreateCompanyOverrideRequestBody { ... }) -> WithRawResponseTask&lt;CreateCompanyOverrideResponse&gt;</code></summary>
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
        ValueType = EntitlementValueType.Boolean,
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">GetCompanyOverrideAsync</a>(companyOverrideId) -> WithRawResponseTask&lt;GetCompanyOverrideResponse&gt;</code></summary>
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">UpdateCompanyOverrideAsync</a>(companyOverrideId, UpdateCompanyOverrideRequestBody { ... }) -> WithRawResponseTask&lt;UpdateCompanyOverrideResponse&gt;</code></summary>
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
    new UpdateCompanyOverrideRequestBody { ValueType = EntitlementValueType.Boolean }
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">DeleteCompanyOverrideAsync</a>(companyOverrideId) -> WithRawResponseTask&lt;DeleteCompanyOverrideResponse&gt;</code></summary>
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CountCompanyOverridesAsync</a>(CountCompanyOverridesRequest { ... }) -> WithRawResponseTask&lt;CountCompanyOverridesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.CountCompanyOverridesAsync(
    new CountCompanyOverridesRequest
    {
        CompanyId = "company_id",
        FeatureId = "feature_id",
        WithoutExpired = true,
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountCompanyOverridesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListFeatureCompaniesAsync</a>(ListFeatureCompaniesRequest { ... }) -> WithRawResponseTask&lt;ListFeatureCompaniesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.ListFeatureCompaniesAsync(
    new ListFeatureCompaniesRequest
    {
        FeatureId = "feature_id",
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListFeatureCompaniesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CountFeatureCompaniesAsync</a>(CountFeatureCompaniesRequest { ... }) -> WithRawResponseTask&lt;CountFeatureCompaniesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.CountFeatureCompaniesAsync(
    new CountFeatureCompaniesRequest
    {
        FeatureId = "feature_id",
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountFeatureCompaniesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListFeatureUsageAsync</a>(ListFeatureUsageRequest { ... }) -> WithRawResponseTask&lt;ListFeatureUsageResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.ListFeatureUsageAsync(
    new ListFeatureUsageRequest
    {
        CompanyId = "company_id",
        IncludeUsageAggregation = true,
        Q = "q",
        WithoutNegativeEntitlements = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListFeatureUsageRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">GetFeatureUsageTimeSeriesAsync</a>(GetFeatureUsageTimeSeriesRequest { ... }) -> WithRawResponseTask&lt;GetFeatureUsageTimeSeriesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.GetFeatureUsageTimeSeriesAsync(
    new GetFeatureUsageTimeSeriesRequest
    {
        CompanyId = "company_id",
        EndTime = new DateTime(2024, 01, 15, 09, 30, 00, 000),
        FeatureId = "feature_id",
        Granularity = TimeSeriesGranularity.Daily,
        StartTime = new DateTime(2024, 01, 15, 09, 30, 00, 000),
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

**request:** `GetFeatureUsageTimeSeriesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CountFeatureUsageAsync</a>(CountFeatureUsageRequest { ... }) -> WithRawResponseTask&lt;CountFeatureUsageResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.CountFeatureUsageAsync(
    new CountFeatureUsageRequest
    {
        CompanyId = "company_id",
        IncludeUsageAggregation = true,
        Q = "q",
        WithoutNegativeEntitlements = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountFeatureUsageRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListFeatureUsersAsync</a>(ListFeatureUsersRequest { ... }) -> WithRawResponseTask&lt;ListFeatureUsersResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.ListFeatureUsersAsync(
    new ListFeatureUsersRequest
    {
        FeatureId = "feature_id",
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListFeatureUsersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CountFeatureUsersAsync</a>(CountFeatureUsersRequest { ... }) -> WithRawResponseTask&lt;CountFeatureUsersResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.CountFeatureUsersAsync(
    new CountFeatureUsersRequest
    {
        FeatureId = "feature_id",
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountFeatureUsersRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListPlanEntitlementsAsync</a>(ListPlanEntitlementsRequest { ... }) -> WithRawResponseTask&lt;ListPlanEntitlementsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.ListPlanEntitlementsAsync(
    new ListPlanEntitlementsRequest
    {
        FeatureId = "feature_id",
        PlanId = "plan_id",
        PlanVersionId = "plan_version_id",
        Q = "q",
        WithMeteredProducts = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListPlanEntitlementsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CreatePlanEntitlementAsync</a>(CreatePlanEntitlementRequestBody { ... }) -> WithRawResponseTask&lt;CreatePlanEntitlementResponse&gt;</code></summary>
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
        ValueType = EntitlementValueType.Boolean,
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">GetPlanEntitlementAsync</a>(planEntitlementId) -> WithRawResponseTask&lt;GetPlanEntitlementResponse&gt;</code></summary>
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">UpdatePlanEntitlementAsync</a>(planEntitlementId, UpdatePlanEntitlementRequestBody { ... }) -> WithRawResponseTask&lt;UpdatePlanEntitlementResponse&gt;</code></summary>
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
    new UpdatePlanEntitlementRequestBody { ValueType = EntitlementValueType.Boolean }
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">DeletePlanEntitlementAsync</a>(planEntitlementId) -> WithRawResponseTask&lt;DeletePlanEntitlementResponse&gt;</code></summary>
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CountPlanEntitlementsAsync</a>(CountPlanEntitlementsRequest { ... }) -> WithRawResponseTask&lt;CountPlanEntitlementsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.CountPlanEntitlementsAsync(
    new CountPlanEntitlementsRequest
    {
        FeatureId = "feature_id",
        PlanId = "plan_id",
        PlanVersionId = "plan_version_id",
        Q = "q",
        WithMeteredProducts = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountPlanEntitlementsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">DuplicatePlanEntitlementsAsync</a>(DuplicatePlanEntitlementsRequestBody { ... }) -> WithRawResponseTask&lt;DuplicatePlanEntitlementsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.DuplicatePlanEntitlementsAsync(
    new DuplicatePlanEntitlementsRequestBody
    {
        SourcePlanId = "source_plan_id",
        TargetPlanId = "target_plan_id",
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

**request:** `DuplicatePlanEntitlementsRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">GetFeatureUsageByCompanyAsync</a>(GetFeatureUsageByCompanyRequest { ... }) -> WithRawResponseTask&lt;GetFeatureUsageByCompanyResponse&gt;</code></summary>
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
        Keys = new Dictionary<string, string>() { { "keys", "keys" } },
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

## plans
<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">UpdateCompanyPlansAsync</a>(companyPlanId, UpdateCompanyPlansRequestBody { ... }) -> WithRawResponseTask&lt;UpdateCompanyPlansResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.UpdateCompanyPlansAsync(
    "company_plan_id",
    new UpdateCompanyPlansRequestBody { AddOnIds = new List<string>() { "add_on_ids" } }
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

**companyPlanId:** `string` — company_plan_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateCompanyPlansRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">ListPlansAsync</a>(ListPlansRequest { ... }) -> WithRawResponseTask&lt;ListPlansResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.ListPlansAsync(
    new ListPlansRequest
    {
        CompanyId = "company_id",
        ForFallbackPlan = true,
        ForInitialPlan = true,
        ForTrialExpiryPlan = true,
        HasProductId = true,
        IncludeDraftVersions = true,
        PlanType = PlanType.Plan,
        Q = "q",
        WithoutEntitlementFor = "without_entitlement_for",
        WithoutPaidProductId = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListPlansRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">CreatePlanAsync</a>(CreatePlanRequestBody { ... }) -> WithRawResponseTask&lt;CreatePlanResponse&gt;</code></summary>
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
        PlanType = PlanType.Plan,
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

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">GetPlanAsync</a>(planId, GetPlanRequest { ... }) -> WithRawResponseTask&lt;GetPlanResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.GetPlanAsync(
    "plan_id",
    new GetPlanRequest { PlanVersionId = "plan_version_id" }
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

**planId:** `string` — plan_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `GetPlanRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">UpdatePlanAsync</a>(planId, UpdatePlanRequestBody { ... }) -> WithRawResponseTask&lt;UpdatePlanResponse&gt;</code></summary>
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

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">DeletePlanAsync</a>(planId) -> WithRawResponseTask&lt;DeletePlanResponse&gt;</code></summary>
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

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">UpsertBillingProductPlanAsync</a>(planId, UpsertBillingProductRequestBody { ... }) -> WithRawResponseTask&lt;UpsertBillingProductPlanResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.UpsertBillingProductPlanAsync(
    "plan_id",
    new UpsertBillingProductRequestBody { ChargeType = ChargeType.Free, IsTrialable = true }
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

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">CountPlansAsync</a>(CountPlansRequest { ... }) -> WithRawResponseTask&lt;CountPlansResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.CountPlansAsync(
    new CountPlansRequest
    {
        CompanyId = "company_id",
        ForFallbackPlan = true,
        ForInitialPlan = true,
        ForTrialExpiryPlan = true,
        HasProductId = true,
        IncludeDraftVersions = true,
        PlanType = PlanType.Plan,
        Q = "q",
        WithoutEntitlementFor = "without_entitlement_for",
        WithoutPaidProductId = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountPlansRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">ListPlanIssuesAsync</a>(ListPlanIssuesRequest { ... }) -> WithRawResponseTask&lt;ListPlanIssuesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.ListPlanIssuesAsync(
    new ListPlanIssuesRequest { PlanId = "plan_id", PlanVersionId = "plan_version_id" }
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

**request:** `ListPlanIssuesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">DeletePlanVersionAsync</a>(planId) -> WithRawResponseTask&lt;DeletePlanVersionResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.DeletePlanVersionAsync("plan_id");
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

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">PublishPlanVersionAsync</a>(planId, PublishPlanVersionRequestBody { ... }) -> WithRawResponseTask&lt;PublishPlanVersionResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.PublishPlanVersionAsync(
    "plan_id",
    new PublishPlanVersionRequestBody
    {
        ExcludedCompanyIds = new List<string>() { "excluded_company_ids" },
        MigrationStrategy = PlanVersionMigrationStrategy.Immediate,
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

**planId:** `string` — plan_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `PublishPlanVersionRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## components
<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">ListComponentsAsync</a>(ListComponentsRequest { ... }) -> WithRawResponseTask&lt;ListComponentsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.ListComponentsAsync(
    new ListComponentsRequest
    {
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListComponentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">CreateComponentAsync</a>(CreateComponentRequestBody { ... }) -> WithRawResponseTask&lt;CreateComponentResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.CreateComponentAsync(
    new CreateComponentRequestBody { EntityType = ComponentEntityType.Billing, Name = "name" }
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

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">GetComponentAsync</a>(componentId) -> WithRawResponseTask&lt;GetComponentResponse&gt;</code></summary>
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

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">UpdateComponentAsync</a>(componentId, UpdateComponentRequestBody { ... }) -> WithRawResponseTask&lt;UpdateComponentResponse&gt;</code></summary>
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

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">DeleteComponentAsync</a>(componentId) -> WithRawResponseTask&lt;DeleteComponentResponse&gt;</code></summary>
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

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">CountComponentsAsync</a>(CountComponentsRequest { ... }) -> WithRawResponseTask&lt;CountComponentsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.CountComponentsAsync(
    new CountComponentsRequest
    {
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountComponentsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">PreviewComponentDataAsync</a>(PreviewComponentDataRequest { ... }) -> WithRawResponseTask&lt;PreviewComponentDataResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.PreviewComponentDataAsync(
    new PreviewComponentDataRequest { CompanyId = "company_id", ComponentId = "component_id" }
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

**request:** `PreviewComponentDataRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## dataexports
<details><summary><code>client.Dataexports.<a href="/src/SchematicHQ.Client/Dataexports/DataexportsClient.cs">CreateDataExportAsync</a>(CreateDataExportRequestBody { ... }) -> WithRawResponseTask&lt;CreateDataExportResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Dataexports.CreateDataExportAsync(
    new CreateDataExportRequestBody
    {
        ExportType = "company-feature-usage",
        Metadata = "metadata",
        OutputFileType = "csv",
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

**request:** `CreateDataExportRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Dataexports.<a href="/src/SchematicHQ.Client/Dataexports/DataexportsClient.cs">GetDataExportArtifactAsync</a>(dataExportId) -> WithRawResponseTask&lt;Stream&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Dataexports.GetDataExportArtifactAsync("data_export_id");
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

**dataExportId:** `string` — data_export_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## events
<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">CreateEventBatchAsync</a>(CreateEventBatchRequestBody { ... }) -> WithRawResponseTask&lt;CreateEventBatchResponse&gt;</code></summary>
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
            new CreateEventRequestBody { EventType = EventType.FlagCheck },
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

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">GetEventSummariesAsync</a>(GetEventSummariesRequest { ... }) -> WithRawResponseTask&lt;GetEventSummariesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Events.GetEventSummariesAsync(
    new GetEventSummariesRequest
    {
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `GetEventSummariesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">ListEventsAsync</a>(ListEventsRequest { ... }) -> WithRawResponseTask&lt;ListEventsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Events.ListEventsAsync(
    new ListEventsRequest
    {
        CompanyId = "company_id",
        EventSubtype = "event_subtype",
        FlagId = "flag_id",
        UserId = "user_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListEventsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">CreateEventAsync</a>(CreateEventRequestBody { ... }) -> WithRawResponseTask&lt;CreateEventResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Events.CreateEventAsync(
    new CreateEventRequestBody { EventType = EventType.FlagCheck }
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

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">GetEventAsync</a>(eventId) -> WithRawResponseTask&lt;GetEventResponse&gt;</code></summary>
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

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">GetSegmentIntegrationStatusAsync</a>() -> WithRawResponseTask&lt;GetSegmentIntegrationStatusResponse&gt;</code></summary>
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

## features
<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">ListFeaturesAsync</a>(ListFeaturesRequest { ... }) -> WithRawResponseTask&lt;ListFeaturesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.ListFeaturesAsync(
    new ListFeaturesRequest
    {
        Q = "q",
        WithoutCompanyOverrideFor = "without_company_override_for",
        PlanVersionId = "plan_version_id",
        WithoutPlanEntitlementFor = "without_plan_entitlement_for",
        BooleanRequireEvent = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListFeaturesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CreateFeatureAsync</a>(CreateFeatureRequestBody { ... }) -> WithRawResponseTask&lt;CreateFeatureResponse&gt;</code></summary>
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
        FeatureType = FeatureType.Boolean,
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">GetFeatureAsync</a>(featureId) -> WithRawResponseTask&lt;GetFeatureResponse&gt;</code></summary>
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">UpdateFeatureAsync</a>(featureId, UpdateFeatureRequestBody { ... }) -> WithRawResponseTask&lt;UpdateFeatureResponse&gt;</code></summary>
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">DeleteFeatureAsync</a>(featureId) -> WithRawResponseTask&lt;DeleteFeatureResponse&gt;</code></summary>
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CountFeaturesAsync</a>(CountFeaturesRequest { ... }) -> WithRawResponseTask&lt;CountFeaturesResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.CountFeaturesAsync(
    new CountFeaturesRequest
    {
        Q = "q",
        WithoutCompanyOverrideFor = "without_company_override_for",
        PlanVersionId = "plan_version_id",
        WithoutPlanEntitlementFor = "without_plan_entitlement_for",
        BooleanRequireEvent = true,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountFeaturesRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">ListFlagsAsync</a>(ListFlagsRequest { ... }) -> WithRawResponseTask&lt;ListFlagsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.ListFlagsAsync(
    new ListFlagsRequest
    {
        FeatureId = "feature_id",
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListFlagsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CreateFlagAsync</a>(CreateFlagRequestBody { ... }) -> WithRawResponseTask&lt;CreateFlagResponse&gt;</code></summary>
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
        FlagType = "boolean",
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">GetFlagAsync</a>(flagId) -> WithRawResponseTask&lt;GetFlagResponse&gt;</code></summary>
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">UpdateFlagAsync</a>(flagId, CreateFlagRequestBody { ... }) -> WithRawResponseTask&lt;UpdateFlagResponse&gt;</code></summary>
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
        FlagType = "boolean",
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">DeleteFlagAsync</a>(flagId) -> WithRawResponseTask&lt;DeleteFlagResponse&gt;</code></summary>
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">UpdateFlagRulesAsync</a>(flagId, UpdateFlagRulesRequestBody { ... }) -> WithRawResponseTask&lt;UpdateFlagRulesResponse&gt;</code></summary>
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
                Priority = 1000000,
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CheckFlagAsync</a>(key, CheckFlagRequestBody { ... }) -> WithRawResponseTask&lt;CheckFlagResponse&gt;</code></summary>
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CheckFlagsAsync</a>(CheckFlagRequestBody { ... }) -> WithRawResponseTask&lt;CheckFlagsResponse&gt;</code></summary>
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CheckFlagsBulkAsync</a>(CheckFlagsBulkRequestBody { ... }) -> WithRawResponseTask&lt;CheckFlagsBulkResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.CheckFlagsBulkAsync(
    new CheckFlagsBulkRequestBody
    {
        Contexts = new List<CheckFlagRequestBody>() { new CheckFlagRequestBody() },
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

**request:** `CheckFlagsBulkRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CountFlagsAsync</a>(CountFlagsRequest { ... }) -> WithRawResponseTask&lt;CountFlagsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.CountFlagsAsync(
    new CountFlagsRequest
    {
        FeatureId = "feature_id",
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountFlagsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## planbundle
<details><summary><code>client.Planbundle.<a href="/src/SchematicHQ.Client/Planbundle/PlanbundleClient.cs">CreatePlanBundleAsync</a>(CreatePlanBundleRequestBody { ... }) -> WithRawResponseTask&lt;CreatePlanBundleResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Planbundle.CreatePlanBundleAsync(
    new CreatePlanBundleRequestBody
    {
        Entitlements = new List<PlanBundleEntitlementRequestBody>()
        {
            new PlanBundleEntitlementRequestBody { Action = PlanBundleAction.Create },
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

**request:** `CreatePlanBundleRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Planbundle.<a href="/src/SchematicHQ.Client/Planbundle/PlanbundleClient.cs">UpdatePlanBundleAsync</a>(planBundleId, UpdatePlanBundleRequestBody { ... }) -> WithRawResponseTask&lt;UpdatePlanBundleResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Planbundle.UpdatePlanBundleAsync(
    "plan_bundle_id",
    new UpdatePlanBundleRequestBody
    {
        Entitlements = new List<PlanBundleEntitlementRequestBody>()
        {
            new PlanBundleEntitlementRequestBody { Action = PlanBundleAction.Create },
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

**planBundleId:** `string` — plan_bundle_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdatePlanBundleRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## plangroups
<details><summary><code>client.Plangroups.<a href="/src/SchematicHQ.Client/Plangroups/PlangroupsClient.cs">GetPlanGroupAsync</a>(GetPlanGroupRequest { ... }) -> WithRawResponseTask&lt;GetPlanGroupResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plangroups.GetPlanGroupAsync(new GetPlanGroupRequest { IncludeCompanyCounts = true });
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

**request:** `GetPlanGroupRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plangroups.<a href="/src/SchematicHQ.Client/Plangroups/PlangroupsClient.cs">CreatePlanGroupAsync</a>(CreatePlanGroupRequestBody { ... }) -> WithRawResponseTask&lt;CreatePlanGroupResponse&gt;</code></summary>
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
        CheckoutCollectAddress = true,
        CheckoutCollectEmail = true,
        CheckoutCollectPhone = true,
        EnableTaxCollection = true,
        OrderedAddOns = new List<OrderedPlansInGroup>()
        {
            new OrderedPlansInGroup { PlanId = "plan_id" },
        },
        OrderedBundleList = new List<PlanGroupBundleOrder>()
        {
            new PlanGroupBundleOrder { BundleId = "bundleId" },
        },
        OrderedPlans = new List<OrderedPlansInGroup>()
        {
            new OrderedPlansInGroup { PlanId = "plan_id" },
        },
        PreventDowngradesWhenOverLimit = true,
        PreventSelfServiceDowngrade = true,
        ProrationBehavior = ProrationBehavior.CreateProrations,
        ShowAsMonthlyPrices = true,
        ShowCredits = true,
        ShowFeatureDescription = true,
        ShowHardLimit = true,
        ShowPeriodToggle = true,
        ShowZeroPriceAsFree = true,
        SyncCustomerBillingDetails = true,
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

<details><summary><code>client.Plangroups.<a href="/src/SchematicHQ.Client/Plangroups/PlangroupsClient.cs">UpdatePlanGroupAsync</a>(planGroupId, UpdatePlanGroupRequestBody { ... }) -> WithRawResponseTask&lt;UpdatePlanGroupResponse&gt;</code></summary>
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
        CheckoutCollectAddress = true,
        CheckoutCollectEmail = true,
        CheckoutCollectPhone = true,
        EnableTaxCollection = true,
        OrderedAddOns = new List<OrderedPlansInGroup>()
        {
            new OrderedPlansInGroup { PlanId = "plan_id" },
        },
        OrderedBundleList = new List<PlanGroupBundleOrder>()
        {
            new PlanGroupBundleOrder { BundleId = "bundleId" },
        },
        OrderedPlans = new List<OrderedPlansInGroup>()
        {
            new OrderedPlansInGroup { PlanId = "plan_id" },
        },
        PreventDowngradesWhenOverLimit = true,
        PreventSelfServiceDowngrade = true,
        ProrationBehavior = ProrationBehavior.CreateProrations,
        ShowAsMonthlyPrices = true,
        ShowCredits = true,
        ShowFeatureDescription = true,
        ShowHardLimit = true,
        ShowPeriodToggle = true,
        ShowZeroPriceAsFree = true,
        SyncCustomerBillingDetails = true,
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

## planmigrations
<details><summary><code>client.Planmigrations.<a href="/src/SchematicHQ.Client/Planmigrations/PlanmigrationsClient.cs">ListCompanyMigrationsAsync</a>(ListCompanyMigrationsRequest { ... }) -> WithRawResponseTask&lt;ListCompanyMigrationsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Planmigrations.ListCompanyMigrationsAsync(
    new ListCompanyMigrationsRequest
    {
        MigrationId = "migration_id",
        Q = "q",
        Status = PlanVersionCompanyMigrationStatus.Completed,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListCompanyMigrationsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Planmigrations.<a href="/src/SchematicHQ.Client/Planmigrations/PlanmigrationsClient.cs">CountCompanyMigrationsAsync</a>(CountCompanyMigrationsRequest { ... }) -> WithRawResponseTask&lt;CountCompanyMigrationsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Planmigrations.CountCompanyMigrationsAsync(
    new CountCompanyMigrationsRequest
    {
        MigrationId = "migration_id",
        Q = "q",
        Status = PlanVersionCompanyMigrationStatus.Completed,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountCompanyMigrationsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Planmigrations.<a href="/src/SchematicHQ.Client/Planmigrations/PlanmigrationsClient.cs">ListMigrationsAsync</a>(ListMigrationsRequest { ... }) -> WithRawResponseTask&lt;ListMigrationsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Planmigrations.ListMigrationsAsync(
    new ListMigrationsRequest
    {
        PlanVersionId = "plan_version_id",
        Status = PlanVersionMigrationStatus.Completed,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListMigrationsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Planmigrations.<a href="/src/SchematicHQ.Client/Planmigrations/PlanmigrationsClient.cs">GetMigrationAsync</a>(planVersionMigrationId) -> WithRawResponseTask&lt;GetMigrationResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Planmigrations.GetMigrationAsync("plan_version_migration_id");
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

**planVersionMigrationId:** `string` — plan_version_migration_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Planmigrations.<a href="/src/SchematicHQ.Client/Planmigrations/PlanmigrationsClient.cs">CountMigrationsAsync</a>(CountMigrationsRequest { ... }) -> WithRawResponseTask&lt;CountMigrationsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Planmigrations.CountMigrationsAsync(
    new CountMigrationsRequest
    {
        PlanVersionId = "plan_version_id",
        Status = PlanVersionMigrationStatus.Completed,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountMigrationsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## componentspublic
<details><summary><code>client.Componentspublic.<a href="/src/SchematicHQ.Client/Componentspublic/ComponentspublicClient.cs">GetPublicPlansAsync</a>() -> WithRawResponseTask&lt;GetPublicPlansResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Componentspublic.GetPublicPlansAsync();
```
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## scheduledcheckout
<details><summary><code>client.Scheduledcheckout.<a href="/src/SchematicHQ.Client/Scheduledcheckout/ScheduledcheckoutClient.cs">ListScheduledCheckoutsAsync</a>(ListScheduledCheckoutsRequest { ... }) -> WithRawResponseTask&lt;ListScheduledCheckoutsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Scheduledcheckout.ListScheduledCheckoutsAsync(
    new ListScheduledCheckoutsRequest
    {
        CompanyId = "company_id",
        Status = ScheduledCheckoutStatus.Cancelled,
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListScheduledCheckoutsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Scheduledcheckout.<a href="/src/SchematicHQ.Client/Scheduledcheckout/ScheduledcheckoutClient.cs">CreateScheduledCheckoutAsync</a>(CreateScheduledCheckoutRequest { ... }) -> WithRawResponseTask&lt;CreateScheduledCheckoutResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Scheduledcheckout.CreateScheduledCheckoutAsync(
    new CreateScheduledCheckoutRequest
    {
        CompanyId = "company_id",
        ExecuteAfter = new DateTime(2024, 01, 15, 09, 30, 00, 000),
        FromPlanId = "from_plan_id",
        ToPlanId = "to_plan_id",
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

**request:** `CreateScheduledCheckoutRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Scheduledcheckout.<a href="/src/SchematicHQ.Client/Scheduledcheckout/ScheduledcheckoutClient.cs">GetScheduledCheckoutAsync</a>(scheduledCheckoutId) -> WithRawResponseTask&lt;GetScheduledCheckoutResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Scheduledcheckout.GetScheduledCheckoutAsync("scheduled_checkout_id");
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

**scheduledCheckoutId:** `string` — scheduled_checkout_id
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Scheduledcheckout.<a href="/src/SchematicHQ.Client/Scheduledcheckout/ScheduledcheckoutClient.cs">UpdateScheduledCheckoutAsync</a>(scheduledCheckoutId, UpdateScheduledCheckoutRequest { ... }) -> WithRawResponseTask&lt;UpdateScheduledCheckoutResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Scheduledcheckout.UpdateScheduledCheckoutAsync(
    "scheduled_checkout_id",
    new UpdateScheduledCheckoutRequest()
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

**scheduledCheckoutId:** `string` — scheduled_checkout_id
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateScheduledCheckoutRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## accesstokens
<details><summary><code>client.Accesstokens.<a href="/src/SchematicHQ.Client/Accesstokens/AccesstokensClient.cs">IssueTemporaryAccessTokenAsync</a>(IssueTemporaryAccessTokenRequestBody { ... }) -> WithRawResponseTask&lt;IssueTemporaryAccessTokenResponse&gt;</code></summary>
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
        ResourceType = "company",
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
<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">ListWebhookEventsAsync</a>(ListWebhookEventsRequest { ... }) -> WithRawResponseTask&lt;ListWebhookEventsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.ListWebhookEventsAsync(
    new ListWebhookEventsRequest
    {
        Q = "q",
        WebhookId = "webhook_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListWebhookEventsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">GetWebhookEventAsync</a>(webhookEventId) -> WithRawResponseTask&lt;GetWebhookEventResponse&gt;</code></summary>
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

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">CountWebhookEventsAsync</a>(CountWebhookEventsRequest { ... }) -> WithRawResponseTask&lt;CountWebhookEventsResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.CountWebhookEventsAsync(
    new CountWebhookEventsRequest
    {
        Q = "q",
        WebhookId = "webhook_id",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountWebhookEventsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">ListWebhooksAsync</a>(ListWebhooksRequest { ... }) -> WithRawResponseTask&lt;ListWebhooksResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.ListWebhooksAsync(
    new ListWebhooksRequest
    {
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `ListWebhooksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">CreateWebhookAsync</a>(CreateWebhookRequestBody { ... }) -> WithRawResponseTask&lt;CreateWebhookResponse&gt;</code></summary>
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
        RequestTypes = new List<WebhookRequestType>() { WebhookRequestType.SubscriptionTrialEnded },
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

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">GetWebhookAsync</a>(webhookId) -> WithRawResponseTask&lt;GetWebhookResponse&gt;</code></summary>
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

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">UpdateWebhookAsync</a>(webhookId, UpdateWebhookRequestBody { ... }) -> WithRawResponseTask&lt;UpdateWebhookResponse&gt;</code></summary>
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

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">DeleteWebhookAsync</a>(webhookId) -> WithRawResponseTask&lt;DeleteWebhookResponse&gt;</code></summary>
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

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">CountWebhooksAsync</a>(CountWebhooksRequest { ... }) -> WithRawResponseTask&lt;CountWebhooksResponse&gt;</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.CountWebhooksAsync(
    new CountWebhooksRequest
    {
        Q = "q",
        Limit = 1000000,
        Offset = 1000000,
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

**request:** `CountWebhooksRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

