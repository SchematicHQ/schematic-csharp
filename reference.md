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
<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">ListApiKeysAsync</a>(ListApiKeysRequest { ... }) -> ListApiKeysResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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
await client.Accounts.CountApiKeysAsync(
    new CountApiKeysRequest
    {
        EnvironmentId = "environment_id",
        RequireEnvironment = true,
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">ListApiRequestsAsync</a>(ListApiRequestsRequest { ... }) -> ListApiRequestsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.ListApiRequestsAsync(
    new ListApiRequestsRequest
    {
        Q = "q",
        RequestType = "request_type",
        EnvironmentId = "environment_id",
        Limit = 1,
        Offset = 1,
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
await client.Accounts.CountApiRequestsAsync(
    new CountApiRequestsRequest
    {
        Q = "q",
        RequestType = "request_type",
        EnvironmentId = "environment_id",
        Limit = 1,
        Offset = 1,
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
await client.Accounts.ListEnvironmentsAsync(new ListEnvironmentsRequest { Limit = 1, Offset = 1 });
```
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">QuickstartAsync</a>() -> QuickstartResponse</code></summary>
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

## billing
<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListCouponsAsync</a>(ListCouponsRequest { ... }) -> ListCouponsResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingCouponAsync</a>(CreateCouponRequestBody { ... }) -> UpsertBillingCouponResponse</code></summary>
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
        AmountOff = 1,
        Duration = "duration",
        DurationInMonths = 1,
        ExternalId = "external_id",
        MaxRedemptions = 1,
        Name = "name",
        PercentOff = 1.1,
        TimesRedeemed = 1,
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListCustomersWithSubscriptionsAsync</a>(ListCustomersWithSubscriptionsRequest { ... }) -> ListCustomersWithSubscriptionsResponse</code></summary>
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
        FailedToImport = true,
        Q = "q",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">CountCustomersAsync</a>(CountCustomersRequest { ... }) -> CountCustomersResponse</code></summary>
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
        FailedToImport = true,
        Q = "q",
        Limit = 1,
        Offset = 1,
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
    new ListInvoicesRequest
    {
        CompanyId = "company_id",
        CustomerExternalId = "customer_external_id",
        SubscriptionExternalId = "subscription_external_id",
        Limit = 1,
        Offset = 1,
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
await client.Billing.ListMetersAsync(
    new ListMetersRequest
    {
        DisplayName = "display_name",
        Limit = 1,
        Offset = 1,
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
    new ListPaymentMethodsRequest
    {
        CompanyId = "company_id",
        CustomerExternalId = "customer_external_id",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">SearchBillingPricesAsync</a>(SearchBillingPricesRequest { ... }) -> SearchBillingPricesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.SearchBillingPricesAsync(
    new SearchBillingPricesRequest
    {
        ForInitialPlan = true,
        ForTrialExpiryPlan = true,
        Interval = "interval",
        Price = 1,
        ProductId = "product_id",
        Q = "q",
        TiersMode = SearchBillingPricesRequestTiersMode.Volume,
        UsageType = SearchBillingPricesRequestUsageType.Licensed,
        Limit = 1,
        Offset = 1,
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

**request:** `SearchBillingPricesRequest` 
    
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
        BillingScheme = CreateBillingPriceRequestBodyBillingScheme.PerUnit,
        Currency = "currency",
        ExternalAccountId = "external_account_id",
        Interval = "interval",
        IsActive = true,
        Price = 1,
        PriceExternalId = "price_external_id",
        PriceTiers = new List<CreateBillingPriceTierRequestBody>()
        {
            new CreateBillingPriceTierRequestBody { PriceExternalId = "price_external_id" },
        },
        ProductExternalId = "product_external_id",
        UsageType = CreateBillingPriceRequestBodyUsageType.Licensed,
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">DeleteBillingProductAsync</a>(billingId) -> DeleteBillingProductResponse</code></summary>
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListProductPricesAsync</a>(ListProductPricesRequest { ... }) -> ListProductPricesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListProductPricesAsync(
    new ListProductPricesRequest
    {
        Name = "name",
        Q = "q",
        PriceUsageType = ListProductPricesRequestPriceUsageType.Licensed,
        WithoutLinkedToPlan = true,
        WithOneTimeCharges = true,
        WithZeroPrice = true,
        WithPricesOnly = true,
        IsActive = true,
        Limit = 1,
        Offset = 1,
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
        ExternalId = "external_id",
        Name = "name",
        Price = 1.1,
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
await client.Billing.ListBillingProductsAsync(
    new ListBillingProductsRequest
    {
        Name = "name",
        Q = "q",
        PriceUsageType = ListBillingProductsRequestPriceUsageType.Licensed,
        WithoutLinkedToPlan = true,
        WithOneTimeCharges = true,
        WithZeroPrice = true,
        WithPricesOnly = true,
        IsActive = true,
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">CountBillingProductsAsync</a>(CountBillingProductsRequest { ... }) -> CountBillingProductsResponse</code></summary>
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
        Name = "name",
        Q = "q",
        PriceUsageType = CountBillingProductsRequestPriceUsageType.Licensed,
        WithoutLinkedToPlan = true,
        WithOneTimeCharges = true,
        WithZeroPrice = true,
        WithPricesOnly = true,
        IsActive = true,
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">UpsertBillingSubscriptionAsync</a>(CreateBillingSubscriptionRequestBody { ... }) -> UpsertBillingSubscriptionResponse</code></summary>
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
                Price = 1,
                PriceExternalId = "price_external_id",
                ProductExternalId = "product_external_id",
                Quantity = 1,
                UsageType = BillingProductPricingUsageType.Licensed,
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

**request:** `CreateBillingSubscriptionRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## credits
<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ListBillingCreditsAsync</a>(ListBillingCreditsRequest { ... }) -> ListBillingCreditsResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CreateBillingCreditAsync</a>(CreateBillingCreditRequestBody { ... }) -> CreateBillingCreditResponse</code></summary>
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">GetSingleBillingCreditAsync</a>(creditId) -> GetSingleBillingCreditResponse</code></summary>
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">UpdateBillingCreditAsync</a>(creditId, UpdateBillingCreditRequestBody { ... }) -> UpdateBillingCreditResponse</code></summary>
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">SoftDeleteBillingCreditAsync</a>(creditId) -> SoftDeleteBillingCreditResponse</code></summary>
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ListCreditBundlesAsync</a>(ListCreditBundlesRequest { ... }) -> ListCreditBundlesResponse</code></summary>
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
        Status = ListCreditBundlesRequestStatus.Active,
        BundleType = "fixed",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CreateCreditBundleAsync</a>(CreateCreditBundleRequestBody { ... }) -> CreateCreditBundleResponse</code></summary>
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
        PricePerUnit = 1,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">GetCreditBundleAsync</a>(bundleId) -> GetCreditBundleResponse</code></summary>
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">UpdateCreditBundleDetailsAsync</a>(bundleId, UpdateCreditBundleDetailsRequestBody { ... }) -> UpdateCreditBundleDetailsResponse</code></summary>
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
    new UpdateCreditBundleDetailsRequestBody { BundleName = "bundle_name", PricePerUnit = 1 }
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">DeleteCreditBundleAsync</a>(bundleId) -> DeleteCreditBundleResponse</code></summary>
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CountCreditBundlesAsync</a>(CountCreditBundlesRequest { ... }) -> CountCreditBundlesResponse</code></summary>
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
        Status = CountCreditBundlesRequestStatus.Active,
        BundleType = "fixed",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CountBillingCreditsAsync</a>(CountBillingCreditsRequest { ... }) -> CountBillingCreditsResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ZeroOutGrantAsync</a>(grantId, ZeroOutGrantRequestBody { ... }) -> ZeroOutGrantResponse</code></summary>
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">GrantBillingCreditsToCompanyAsync</a>(CreateCompanyCreditGrant { ... }) -> GrantBillingCreditsToCompanyResponse</code></summary>
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
        Quantity = 1,
        Reason = "reason",
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ListCompanyGrantsAsync</a>(ListCompanyGrantsRequest { ... }) -> ListCompanyGrantsResponse</code></summary>
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
        Order = ListCompanyGrantsRequestOrder.CreatedAt,
        Dir = ListCompanyGrantsRequestDir.Asc,
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CountBillingCreditsGrantsAsync</a>(CountBillingCreditsGrantsRequest { ... }) -> CountBillingCreditsGrantsResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ListGrantsForCreditAsync</a>(ListGrantsForCreditRequest { ... }) -> ListGrantsForCreditResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">GetEnrichedCreditLedgerAsync</a>(GetEnrichedCreditLedgerRequest { ... }) -> GetEnrichedCreditLedgerResponse</code></summary>
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
        Period = GetEnrichedCreditLedgerRequestPeriod.Daily,
        StartTime = "start_time",
        EndTime = "end_time",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CountCreditLedgerAsync</a>(CountCreditLedgerRequest { ... }) -> CountCreditLedgerResponse</code></summary>
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
        Period = CountCreditLedgerRequestPeriod.Daily,
        StartTime = "start_time",
        EndTime = "end_time",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ListBillingPlanCreditGrantsAsync</a>(ListBillingPlanCreditGrantsRequest { ... }) -> ListBillingPlanCreditGrantsResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CreateBillingPlanCreditGrantAsync</a>(CreateBillingPlanCreditGrantRequestBody { ... }) -> CreateBillingPlanCreditGrantResponse</code></summary>
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
        CreditAmount = 1,
        CreditId = "credit_id",
        PlanId = "plan_id",
        ResetCadence = CreateBillingPlanCreditGrantRequestBodyResetCadence.Monthly,
        ResetStart = CreateBillingPlanCreditGrantRequestBodyResetStart.BillingPeriod,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">UpdateBillingPlanCreditGrantAsync</a>(planGrantId, UpdateBillingPlanCreditGrantRequestBody { ... }) -> UpdateBillingPlanCreditGrantResponse</code></summary>
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
        ResetCadence = UpdateBillingPlanCreditGrantRequestBodyResetCadence.Monthly,
        ResetStart = UpdateBillingPlanCreditGrantRequestBodyResetStart.BillingPeriod,
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">DeleteBillingPlanCreditGrantAsync</a>(planGrantId, DeleteBillingPlanCreditGrantRequest { ... }) -> DeleteBillingPlanCreditGrantResponse</code></summary>
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">CountBillingPlanCreditGrantsAsync</a>(CountBillingPlanCreditGrantsRequest { ... }) -> CountBillingPlanCreditGrantsResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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

## checkout
<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">InternalAsync</a>(ChangeSubscriptionInternalRequestBody { ... }) -> CheckoutInternalResponse</code></summary>
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
            new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1 },
        },
        NewPlanId = "new_plan_id",
        NewPriceId = "new_price_id",
        PayInAdvance = new List<UpdatePayInAdvanceRequestBody>()
        {
            new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1 },
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

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">GetCheckoutDataAsync</a>(CheckoutDataRequestBody { ... }) -> GetCheckoutDataResponse</code></summary>
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

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">PreviewCheckoutInternalAsync</a>(ChangeSubscriptionInternalRequestBody { ... }) -> PreviewCheckoutInternalResponse</code></summary>
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
            new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1 },
        },
        NewPlanId = "new_plan_id",
        NewPriceId = "new_price_id",
        PayInAdvance = new List<UpdatePayInAdvanceRequestBody>()
        {
            new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1 },
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

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">ManagePlanAsync</a>(ManagePlanRequest { ... }) -> ManagePlanResponse</code></summary>
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
            new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1 },
        },
        PayInAdvanceEntitlements = new List<UpdatePayInAdvanceRequestBody>()
        {
            new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1 },
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

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">PreviewManagePlanAsync</a>(ManagePlanRequest { ... }) -> PreviewManagePlanResponse</code></summary>
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
            new UpdateCreditBundleRequestBody { BundleId = "bundle_id", Quantity = 1 },
        },
        PayInAdvanceEntitlements = new List<UpdatePayInAdvanceRequestBody>()
        {
            new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1 },
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

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">UpdateCustomerSubscriptionTrialEndAsync</a>(subscriptionId, UpdateTrialEndRequestBody { ... }) -> UpdateCustomerSubscriptionTrialEndResponse</code></summary>
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
<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListCompaniesAsync</a>(ListCompaniesRequest { ... }) -> ListCompaniesResponse</code></summary>
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
        PlanId = "plan_id",
        Q = "q",
        WithoutFeatureOverrideFor = "without_feature_override_for",
        WithoutPlan = true,
        WithSubscription = true,
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteCompanyAsync</a>(companyId, DeleteCompanyRequest { ... }) -> DeleteCompanyResponse</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountCompaniesAsync</a>(CountCompaniesRequest { ... }) -> CountCompaniesResponse</code></summary>
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
        PlanId = "plan_id",
        Q = "q",
        WithoutFeatureOverrideFor = "without_feature_override_for",
        WithoutPlan = true,
        WithSubscription = true,
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountCompaniesForAdvancedFilterAsync</a>(CountCompaniesForAdvancedFilterRequest { ... }) -> CountCompaniesForAdvancedFilterResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.CountCompaniesForAdvancedFilterAsync(
    new CountCompaniesForAdvancedFilterRequest
    {
        MonetizedSubscriptions = true,
        Q = "q",
        WithoutPlan = true,
        WithoutSubscription = true,
        SortOrderColumn = "sort_order_column",
        SortOrderDirection = CountCompaniesForAdvancedFilterRequestSortOrderDirection.Asc,
        Limit = 1,
        Offset = 1,
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

**request:** `CountCompaniesForAdvancedFilterRequest` 
    
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListCompaniesForAdvancedFilterAsync</a>(ListCompaniesForAdvancedFilterRequest { ... }) -> ListCompaniesForAdvancedFilterResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListCompaniesForAdvancedFilterAsync(
    new ListCompaniesForAdvancedFilterRequest
    {
        MonetizedSubscriptions = true,
        Q = "q",
        WithoutPlan = true,
        WithoutSubscription = true,
        SortOrderColumn = "sort_order_column",
        SortOrderDirection = ListCompaniesForAdvancedFilterRequestSortOrderDirection.Asc,
        Limit = 1,
        Offset = 1,
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

**request:** `ListCompaniesForAdvancedFilterRequest` 
    
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
    new GetActiveDealsRequest
    {
        CompanyId = "company_id",
        DealStage = "deal_stage",
        Limit = 1,
        Offset = 1,
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
await client.Companies.ListCompanyMembershipsAsync(
    new ListCompanyMembershipsRequest
    {
        CompanyId = "company_id",
        UserId = "user_id",
        Limit = 1,
        Offset = 1,
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
await client.Companies.GetActiveCompanySubscriptionAsync(
    new GetActiveCompanySubscriptionRequest
    {
        CompanyId = "company_id",
        Limit = 1,
        Offset = 1,
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
await client.Companies.ListEntityKeyDefinitionsAsync(
    new ListEntityKeyDefinitionsRequest
    {
        EntityType = ListEntityKeyDefinitionsRequestEntityType.Company,
        Q = "q",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountEntityKeyDefinitionsAsync</a>(CountEntityKeyDefinitionsRequest { ... }) -> CountEntityKeyDefinitionsResponse</code></summary>
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
        EntityType = CountEntityKeyDefinitionsRequestEntityType.Company,
        Q = "q",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListEntityTraitDefinitionsAsync</a>(ListEntityTraitDefinitionsRequest { ... }) -> ListEntityTraitDefinitionsResponse</code></summary>
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
        EntityType = ListEntityTraitDefinitionsRequestEntityType.Company,
        Q = "q",
        TraitType = ListEntityTraitDefinitionsRequestTraitType.Boolean,
        Limit = 1,
        Offset = 1,
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
await client.Companies.CountEntityTraitDefinitionsAsync(
    new CountEntityTraitDefinitionsRequest
    {
        EntityType = CountEntityTraitDefinitionsRequestEntityType.Company,
        Q = "q",
        TraitType = CountEntityTraitDefinitionsRequestTraitType.Boolean,
        Limit = 1,
        Offset = 1,
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
    new GetEntityTraitValuesRequest
    {
        DefinitionId = "definition_id",
        Q = "q",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListPlanChangesAsync</a>(ListPlanChangesRequest { ... }) -> ListPlanChangesResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetPlanChangeAsync</a>(planChangeId) -> GetPlanChangeResponse</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListPlanTraitsAsync</a>(ListPlanTraitsRequest { ... }) -> ListPlanTraitsResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CreatePlanTraitAsync</a>(CreatePlanTraitRequestBody { ... }) -> CreatePlanTraitResponse</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetPlanTraitAsync</a>(planTraitId) -> GetPlanTraitResponse</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpdatePlanTraitAsync</a>(planTraitId, UpdatePlanTraitRequestBody { ... }) -> UpdatePlanTraitResponse</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeletePlanTraitAsync</a>(planTraitId) -> DeletePlanTraitResponse</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpdatePlanTraitsBulkAsync</a>(UpdatePlanTraitBulkRequestBody { ... }) -> UpdatePlanTraitsBulkResponse</code></summary>
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">CountPlanTraitsAsync</a>(CountPlanTraitsRequest { ... }) -> CountPlanTraitsResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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
await client.Companies.ListUsersAsync(
    new ListUsersRequest
    {
        CompanyId = "company_id",
        PlanId = "plan_id",
        Q = "q",
        Limit = 1,
        Offset = 1,
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
await client.Companies.CountUsersAsync(
    new CountUsersRequest
    {
        CompanyId = "company_id",
        PlanId = "plan_id",
        Q = "q",
        Limit = 1,
        Offset = 1,
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
<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListCompanyOverridesAsync</a>(ListCompanyOverridesRequest { ... }) -> ListCompanyOverridesResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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
await client.Entitlements.CountCompanyOverridesAsync(
    new CountCompanyOverridesRequest
    {
        CompanyId = "company_id",
        FeatureId = "feature_id",
        WithoutExpired = true,
        Q = "q",
        Limit = 1,
        Offset = 1,
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
    new ListFeatureCompaniesRequest
    {
        FeatureId = "feature_id",
        Q = "q",
        Limit = 1,
        Offset = 1,
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
    new CountFeatureCompaniesRequest
    {
        FeatureId = "feature_id",
        Q = "q",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListFeatureUsageAsync</a>(ListFeatureUsageRequest { ... }) -> ListFeatureUsageResponse</code></summary>
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
        Q = "q",
        WithoutNegativeEntitlements = true,
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">CountFeatureUsageAsync</a>(CountFeatureUsageRequest { ... }) -> CountFeatureUsageResponse</code></summary>
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
        Q = "q",
        WithoutNegativeEntitlements = true,
        Limit = 1,
        Offset = 1,
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
    new ListFeatureUsersRequest
    {
        FeatureId = "feature_id",
        Q = "q",
        Limit = 1,
        Offset = 1,
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
    new CountFeatureUsersRequest
    {
        FeatureId = "feature_id",
        Q = "q",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">ListPlanEntitlementsAsync</a>(ListPlanEntitlementsRequest { ... }) -> ListPlanEntitlementsResponse</code></summary>
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
        Q = "q",
        WithMeteredProducts = true,
        Limit = 1,
        Offset = 1,
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
await client.Entitlements.CountPlanEntitlementsAsync(
    new CountPlanEntitlementsRequest
    {
        FeatureId = "feature_id",
        PlanId = "plan_id",
        Q = "q",
        WithMeteredProducts = true,
        Limit = 1,
        Offset = 1,
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
<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">UpdateCompanyPlansAsync</a>(companyPlanId, UpdateCompanyPlansRequestBody { ... }) -> UpdateCompanyPlansResponse</code></summary>
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

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">ListPlansAsync</a>(ListPlansRequest { ... }) -> ListPlansResponse</code></summary>
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
        PlanType = ListPlansRequestPlanType.Plan,
        Q = "q",
        WithoutEntitlementFor = "without_entitlement_for",
        WithoutPaidProductId = true,
        Limit = 1,
        Offset = 1,
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
await client.Plans.UpsertBillingProductPlanAsync(
    "plan_id",
    new UpsertBillingProductRequestBody
    {
        ChargeType = UpsertBillingProductRequestBodyChargeType.OneTime,
        IsTrialable = true,
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
await client.Plans.CountPlansAsync(
    new CountPlansRequest
    {
        CompanyId = "company_id",
        ForFallbackPlan = true,
        ForInitialPlan = true,
        ForTrialExpiryPlan = true,
        HasProductId = true,
        PlanType = CountPlansRequestPlanType.Plan,
        Q = "q",
        WithoutEntitlementFor = "without_entitlement_for",
        WithoutPaidProductId = true,
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">ListPlanIssuesAsync</a>(ListPlanIssuesRequest { ... }) -> ListPlanIssuesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.ListPlanIssuesAsync(new ListPlanIssuesRequest { PlanId = "plan_id" });
```
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
await client.Components.ListComponentsAsync(
    new ListComponentsRequest
    {
        Q = "q",
        Limit = 1,
        Offset = 1,
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
await client.Components.CountComponentsAsync(
    new CountComponentsRequest
    {
        Q = "q",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">PreviewComponentDataAsync</a>(PreviewComponentDataRequest { ... }) -> PreviewComponentDataResponse</code></summary>
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
await client.Crm.ListCrmProductsAsync(
    new ListCrmProductsRequest
    {
        Name = "name",
        Limit = 1,
        Offset = 1,
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

## dataexports
<details><summary><code>client.Dataexports.<a href="/src/SchematicHQ.Client/Dataexports/DataexportsClient.cs">CreateDataExportAsync</a>(CreateDataExportRequestBody { ... }) -> CreateDataExportResponse</code></summary>
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

<details><summary><code>client.Dataexports.<a href="/src/SchematicHQ.Client/Dataexports/DataexportsClient.cs">GetDataExportArtifactAsync</a>(dataExportId)</code></summary>
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
await client.Events.GetEventSummariesAsync(
    new GetEventSummariesRequest
    {
        Q = "q",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">ListEventsAsync</a>(ListEventsRequest { ... }) -> ListEventsResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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

## features
<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">ListFeaturesAsync</a>(ListFeaturesRequest { ... }) -> ListFeaturesResponse</code></summary>
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
        WithoutPlanEntitlementFor = "without_plan_entitlement_for",
        BooleanRequireEvent = true,
        Limit = 1,
        Offset = 1,
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
await client.Features.CountFeaturesAsync(
    new CountFeaturesRequest
    {
        Q = "q",
        WithoutCompanyOverrideFor = "without_company_override_for",
        WithoutPlanEntitlementFor = "without_plan_entitlement_for",
        BooleanRequireEvent = true,
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">ListFlagsAsync</a>(ListFlagsRequest { ... }) -> ListFlagsResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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
await client.Features.CountFlagsAsync(
    new CountFlagsRequest
    {
        FeatureId = "feature_id",
        Q = "q",
        Limit = 1,
        Offset = 1,
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
        ShowCredits = true,
        ShowPeriodToggle = true,
        ShowZeroPriceAsFree = true,
        SyncCustomerBillingDetailsForTax = true,
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
        ShowCredits = true,
        ShowPeriodToggle = true,
        ShowZeroPriceAsFree = true,
        SyncCustomerBillingDetailsForTax = true,
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
<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">ListWebhookEventsAsync</a>(ListWebhookEventsRequest { ... }) -> ListWebhookEventsResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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
await client.Webhooks.CountWebhookEventsAsync(
    new CountWebhookEventsRequest
    {
        Q = "q",
        WebhookId = "webhook_id",
        Limit = 1,
        Offset = 1,
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

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">ListWebhooksAsync</a>(ListWebhooksRequest { ... }) -> ListWebhooksResponse</code></summary>
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
        Limit = 1,
        Offset = 1,
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
await client.Webhooks.CountWebhooksAsync(
    new CountWebhooksRequest
    {
        Q = "q",
        Limit = 1,
        Offset = 1,
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
