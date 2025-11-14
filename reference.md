# Reference
<details><summary><code>client.<a href="/src/SchematicHQ.Client/Schematic.cs">PutPlanAudiencesPlanAudienceIdAsync</a>(PutPlanAudiencesPlanAudienceIdRequest { ... })</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.PutPlanAudiencesPlanAudienceIdAsync(
    new PutPlanAudiencesPlanAudienceIdRequest { PlanAudienceId = "plan_audience_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `PutPlanAudiencesPlanAudienceIdRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.<a href="/src/SchematicHQ.Client/Schematic.cs">DeletePlanAudiencesPlanAudienceIdAsync</a>(DeletePlanAudiencesPlanAudienceIdRequest { ... })</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.DeletePlanAudiencesPlanAudienceIdAsync(
    new DeletePlanAudiencesPlanAudienceIdRequest { PlanAudienceId = "plan_audience_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeletePlanAudiencesPlanAudienceIdRequest` 
    
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">GetApiKeyAsync</a>(GetApiKeyRequest { ... }) -> GetApiKeyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.GetApiKeyAsync(new GetApiKeyRequest { ApiKeyId = "api_key_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetApiKeyRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">UpdateApiKeyAsync</a>(UpdateApiKeyRequestBody { ... }) -> UpdateApiKeyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.UpdateApiKeyAsync(new UpdateApiKeyRequestBody { ApiKeyId = "api_key_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">DeleteApiKeyAsync</a>(DeleteApiKeyRequest { ... }) -> DeleteApiKeyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.DeleteApiKeyAsync(new DeleteApiKeyRequest { ApiKeyId = "api_key_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteApiKeyRequest` 
    
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">GetApiRequestAsync</a>(GetApiRequestRequest { ... }) -> GetApiRequestResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.GetApiRequestAsync(
    new GetApiRequestRequest { ApiRequestId = "api_request_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetApiRequestRequest` 
    
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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">GetEnvironmentAsync</a>(GetEnvironmentRequest { ... }) -> GetEnvironmentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.GetEnvironmentAsync(
    new GetEnvironmentRequest { EnvironmentId = "environment_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetEnvironmentRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">UpdateEnvironmentAsync</a>(UpdateEnvironmentRequestBody { ... }) -> UpdateEnvironmentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.UpdateEnvironmentAsync(
    new UpdateEnvironmentRequestBody { EnvironmentId = "environment_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

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

<details><summary><code>client.Accounts.<a href="/src/SchematicHQ.Client/Accounts/AccountsClient.cs">DeleteEnvironmentAsync</a>(DeleteEnvironmentRequest { ... }) -> DeleteEnvironmentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Accounts.DeleteEnvironmentAsync(
    new DeleteEnvironmentRequest { EnvironmentId = "environment_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteEnvironmentRequest` 
    
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
await client.Billing.ListCouponsAsync(new ListCouponsRequest());
```
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
    new ListCustomersWithSubscriptionsRequest()
);
```
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
    new ListInvoicesRequest
    {
        CustomerExternalId = "customer_external_id",
        SubscriptionExternalId = "subscription_external_id",
    }
);
```
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListBillingPricesAsync</a>(ListBillingPricesRequest { ... }) -> ListBillingPricesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListBillingPricesAsync(new ListBillingPricesRequest());
```
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">DeleteBillingProductAsync</a>(DeleteBillingProductRequest { ... }) -> DeleteBillingProductResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.DeleteBillingProductAsync(
    new DeleteBillingProductRequest { BillingId = "billing_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteBillingProductRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">ListBillingProductPricesAsync</a>(ListBillingProductPricesRequest { ... }) -> ListBillingProductPricesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.ListBillingProductPricesAsync(new ListBillingProductPricesRequest());
```
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

<details><summary><code>client.Billing.<a href="/src/SchematicHQ.Client/Billing/BillingClient.cs">DeleteProductPriceAsync</a>(DeleteProductPriceRequest { ... }) -> DeleteProductPriceResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Billing.DeleteProductPriceAsync(
    new DeleteProductPriceRequest { BillingId = "billing_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteProductPriceRequest` 
    
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
await client.Credits.ListBillingCreditsAsync(new ListBillingCreditsRequest());
```
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">GetSingleBillingCreditAsync</a>(GetSingleBillingCreditRequest { ... }) -> GetSingleBillingCreditResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.GetSingleBillingCreditAsync(
    new GetSingleBillingCreditRequest { CreditId = "credit_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetSingleBillingCreditRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">UpdateBillingCreditAsync</a>(UpdateBillingCreditRequestBody { ... }) -> UpdateBillingCreditResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.UpdateBillingCreditAsync(
    new UpdateBillingCreditRequestBody
    {
        CreditId = "credit_id",
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

**request:** `UpdateBillingCreditRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">SoftDeleteBillingCreditAsync</a>(SoftDeleteBillingCreditRequest { ... }) -> SoftDeleteBillingCreditResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.SoftDeleteBillingCreditAsync(
    new SoftDeleteBillingCreditRequest { CreditId = "credit_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `SoftDeleteBillingCreditRequest` 
    
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
await client.Credits.ListCreditBundlesAsync(new ListCreditBundlesRequest());
```
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">GetCreditBundleAsync</a>(GetCreditBundleRequest { ... }) -> GetCreditBundleResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.GetCreditBundleAsync(new GetCreditBundleRequest { BundleId = "bundle_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetCreditBundleRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">UpdateCreditBundleDetailsAsync</a>(UpdateCreditBundleDetailsRequestBody { ... }) -> UpdateCreditBundleDetailsResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.UpdateCreditBundleDetailsAsync(
    new UpdateCreditBundleDetailsRequestBody
    {
        BundleId = "bundle_id",
        BundleName = "bundle_name",
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

**request:** `UpdateCreditBundleDetailsRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">DeleteCreditBundleAsync</a>(DeleteCreditBundleRequest { ... }) -> DeleteCreditBundleResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.DeleteCreditBundleAsync(
    new DeleteCreditBundleRequest { BundleId = "bundle_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteCreditBundleRequest` 
    
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
await client.Credits.CountCreditBundlesAsync(new CountCreditBundlesRequest());
```
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
await client.Credits.CountBillingCreditsAsync(new CountBillingCreditsRequest());
```
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">ZeroOutGrantAsync</a>(ZeroOutGrantRequestBody { ... }) -> ZeroOutGrantResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.ZeroOutGrantAsync(new ZeroOutGrantRequestBody { GrantId = "grant_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

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
await client.Credits.ListCompanyGrantsAsync(new ListCompanyGrantsRequest());
```
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
await client.Credits.CountBillingCreditsGrantsAsync(new CountBillingCreditsGrantsRequest());
```
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
await client.Credits.ListGrantsForCreditAsync(new ListGrantsForCreditRequest());
```
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
        Period = GetEnrichedCreditLedgerRequestPeriod.Daily,
    }
);
```
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
        Period = CountCreditLedgerRequestPeriod.Daily,
    }
);
```
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
await client.Credits.ListBillingPlanCreditGrantsAsync(new ListBillingPlanCreditGrantsRequest());
```
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

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">UpdateBillingPlanCreditGrantAsync</a>(UpdateBillingPlanCreditGrantRequestBody { ... }) -> UpdateBillingPlanCreditGrantResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.UpdateBillingPlanCreditGrantAsync(
    new UpdateBillingPlanCreditGrantRequestBody
    {
        PlanGrantId = "plan_grant_id",
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

**request:** `UpdateBillingPlanCreditGrantRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Credits.<a href="/src/SchematicHQ.Client/Credits/CreditsClient.cs">DeleteBillingPlanCreditGrantAsync</a>(DeleteBillingPlanCreditGrantRequest { ... }) -> DeleteBillingPlanCreditGrantResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Credits.DeleteBillingPlanCreditGrantAsync(
    new DeleteBillingPlanCreditGrantRequest { PlanGrantId = "plan_grant_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

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
await client.Credits.CountBillingPlanCreditGrantsAsync(new CountBillingPlanCreditGrantsRequest());
```
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

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">CancelSubscriptionAsync</a>(CancelSubscriptionRequest { ... }) -> CancelSubscriptionResponse</code></summary>
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

<details><summary><code>client.Checkout.<a href="/src/SchematicHQ.Client/Checkout/CheckoutClient.cs">UpdateCustomerSubscriptionTrialEndAsync</a>(UpdateTrialEndRequestBody { ... }) -> UpdateCustomerSubscriptionTrialEndResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Checkout.UpdateCustomerSubscriptionTrialEndAsync(
    new UpdateTrialEndRequestBody { SubscriptionId = "subscription_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetCompanyAsync</a>(GetCompanyRequest { ... }) -> GetCompanyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetCompanyAsync(new GetCompanyRequest { CompanyId = "company_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetCompanyRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteCompanyAsync</a>(DeleteCompanyRequest { ... }) -> DeleteCompanyResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.DeleteCompanyAsync(new DeleteCompanyRequest { CompanyId = "company_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

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
    new CountCompaniesForAdvancedFilterRequest()
);
```
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
    new ListCompaniesForAdvancedFilterRequest()
);
```
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteCompanyMembershipAsync</a>(DeleteCompanyMembershipRequest { ... }) -> DeleteCompanyMembershipResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.DeleteCompanyMembershipAsync(
    new DeleteCompanyMembershipRequest { CompanyMembershipId = "company_membership_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteCompanyMembershipRequest` 
    
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetEntityTraitDefinitionAsync</a>(GetEntityTraitDefinitionRequest { ... }) -> GetEntityTraitDefinitionResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetEntityTraitDefinitionAsync(
    new GetEntityTraitDefinitionRequest { EntityTraitDefinitionId = "entity_trait_definition_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetEntityTraitDefinitionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpdateEntityTraitDefinitionAsync</a>(UpdateEntityTraitDefinitionRequestBody { ... }) -> UpdateEntityTraitDefinitionResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.UpdateEntityTraitDefinitionAsync(
    new UpdateEntityTraitDefinitionRequestBody
    {
        EntityTraitDefinitionId = "entity_trait_definition_id",
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">ListPlanChangesAsync</a>(ListPlanChangesRequest { ... }) -> ListPlanChangesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.ListPlanChangesAsync(new ListPlanChangesRequest());
```
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetPlanChangeAsync</a>(GetPlanChangeRequest { ... }) -> GetPlanChangeResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetPlanChangeAsync(
    new GetPlanChangeRequest { PlanChangeId = "plan_change_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetPlanChangeRequest` 
    
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
await client.Companies.ListPlanTraitsAsync(new ListPlanTraitsRequest());
```
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetPlanTraitAsync</a>(GetPlanTraitRequest { ... }) -> GetPlanTraitResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetPlanTraitAsync(new GetPlanTraitRequest { PlanTraitId = "plan_trait_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetPlanTraitRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">UpdatePlanTraitAsync</a>(UpdatePlanTraitRequestBody { ... }) -> UpdatePlanTraitResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.UpdatePlanTraitAsync(
    new UpdatePlanTraitRequestBody
    {
        PlanTraitId = "plan_trait_id",
        PlanId = "plan_id",
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

**request:** `UpdatePlanTraitRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeletePlanTraitAsync</a>(DeletePlanTraitRequest { ... }) -> DeletePlanTraitResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.DeletePlanTraitAsync(
    new DeletePlanTraitRequest { PlanTraitId = "plan_trait_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeletePlanTraitRequest` 
    
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
await client.Companies.CountPlanTraitsAsync(new CountPlanTraitsRequest());
```
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

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">GetUserAsync</a>(GetUserRequest { ... }) -> GetUserResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.GetUserAsync(new GetUserRequest { UserId = "user_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetUserRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Companies.<a href="/src/SchematicHQ.Client/Companies/CompaniesClient.cs">DeleteUserAsync</a>(DeleteUserRequest { ... }) -> DeleteUserResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Companies.DeleteUserAsync(new DeleteUserRequest { UserId = "user_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteUserRequest` 
    
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">GetCompanyOverrideAsync</a>(GetCompanyOverrideRequest { ... }) -> GetCompanyOverrideResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.GetCompanyOverrideAsync(
    new GetCompanyOverrideRequest { CompanyOverrideId = "company_override_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetCompanyOverrideRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">UpdateCompanyOverrideAsync</a>(UpdateCompanyOverrideRequestBody { ... }) -> UpdateCompanyOverrideResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.UpdateCompanyOverrideAsync(
    new UpdateCompanyOverrideRequestBody
    {
        CompanyOverrideId = "company_override_id",
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

**request:** `UpdateCompanyOverrideRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">DeleteCompanyOverrideAsync</a>(DeleteCompanyOverrideRequest { ... }) -> DeleteCompanyOverrideResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.DeleteCompanyOverrideAsync(
    new DeleteCompanyOverrideRequest { CompanyOverrideId = "company_override_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteCompanyOverrideRequest` 
    
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">GetPlanEntitlementAsync</a>(GetPlanEntitlementRequest { ... }) -> GetPlanEntitlementResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.GetPlanEntitlementAsync(
    new GetPlanEntitlementRequest { PlanEntitlementId = "plan_entitlement_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetPlanEntitlementRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">UpdatePlanEntitlementAsync</a>(UpdatePlanEntitlementRequestBody { ... }) -> UpdatePlanEntitlementResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.UpdatePlanEntitlementAsync(
    new UpdatePlanEntitlementRequestBody
    {
        PlanEntitlementId = "plan_entitlement_id",
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

**request:** `UpdatePlanEntitlementRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">DeletePlanEntitlementAsync</a>(DeletePlanEntitlementRequest { ... }) -> DeletePlanEntitlementResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Entitlements.DeletePlanEntitlementAsync(
    new DeletePlanEntitlementRequest { PlanEntitlementId = "plan_entitlement_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeletePlanEntitlementRequest` 
    
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

<details><summary><code>client.Entitlements.<a href="/src/SchematicHQ.Client/Entitlements/EntitlementsClient.cs">DuplicatePlanEntitlementsAsync</a>(DuplicatePlanEntitlementsRequestBody { ... }) -> DuplicatePlanEntitlementsResponse</code></summary>
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
<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">UpdateCompanyPlansAsync</a>(UpdateCompanyPlansRequestBody { ... }) -> UpdateCompanyPlansResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.UpdateCompanyPlansAsync(
    new UpdateCompanyPlansRequestBody
    {
        CompanyPlanId = "company_plan_id",
        AddOnIds = new List<string>() { "add_on_ids" },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

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

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">GetPlanAsync</a>(GetPlanRequest { ... }) -> GetPlanResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.GetPlanAsync(new GetPlanRequest { PlanId = "plan_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

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

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">UpdatePlanAsync</a>(UpdatePlanRequestBody { ... }) -> UpdatePlanResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.UpdatePlanAsync(new UpdatePlanRequestBody { PlanId = "plan_id", Name = "name" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

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

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">DeletePlanAsync</a>(DeletePlanRequest { ... }) -> DeletePlanResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.DeletePlanAsync(new DeletePlanRequest { PlanId = "plan_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeletePlanRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Plans.<a href="/src/SchematicHQ.Client/Plans/PlansClient.cs">UpsertBillingProductPlanAsync</a>(UpsertBillingProductRequestBody { ... }) -> UpsertBillingProductPlanResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plans.UpsertBillingProductPlanAsync(
    new UpsertBillingProductRequestBody
    {
        PlanId = "plan_id",
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

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">GetComponentAsync</a>(GetComponentRequest { ... }) -> GetComponentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.GetComponentAsync(new GetComponentRequest { ComponentId = "component_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetComponentRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">UpdateComponentAsync</a>(UpdateComponentRequestBody { ... }) -> UpdateComponentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.UpdateComponentAsync(
    new UpdateComponentRequestBody { ComponentId = "component_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

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

<details><summary><code>client.Components.<a href="/src/SchematicHQ.Client/Components/ComponentsClient.cs">DeleteComponentAsync</a>(DeleteComponentRequest { ... }) -> DeleteComponentResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Components.DeleteComponentAsync(
    new DeleteComponentRequest { ComponentId = "component_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteComponentRequest` 
    
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

<details><summary><code>client.Dataexports.<a href="/src/SchematicHQ.Client/Dataexports/DataexportsClient.cs">GetDataExportArtifactAsync</a>(GetDataExportArtifactRequest { ... })</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Dataexports.GetDataExportArtifactAsync(
    new GetDataExportArtifactRequest { DataExportId = "data_export_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetDataExportArtifactRequest` 
    
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

<details><summary><code>client.Events.<a href="/src/SchematicHQ.Client/Events/EventsClient.cs">GetEventAsync</a>(GetEventRequest { ... }) -> GetEventResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Events.GetEventAsync(new GetEventRequest { EventId = "event_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetEventRequest` 
    
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">GetFeatureAsync</a>(GetFeatureRequest { ... }) -> GetFeatureResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.GetFeatureAsync(new GetFeatureRequest { FeatureId = "feature_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetFeatureRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">UpdateFeatureAsync</a>(UpdateFeatureRequestBody { ... }) -> UpdateFeatureResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.UpdateFeatureAsync(new UpdateFeatureRequestBody { FeatureId = "feature_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">DeleteFeatureAsync</a>(DeleteFeatureRequest { ... }) -> DeleteFeatureResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.DeleteFeatureAsync(new DeleteFeatureRequest { FeatureId = "feature_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteFeatureRequest` 
    
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

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">GetFlagAsync</a>(GetFlagRequest { ... }) -> GetFlagResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.GetFlagAsync(new GetFlagRequest { FlagId = "flag_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetFlagRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">UpdateFlagAsync</a>(UpdateFlagRequest { ... }) -> UpdateFlagResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.UpdateFlagAsync(
    new UpdateFlagRequest
    {
        FlagId = "flag_id",
        Body = new CreateFlagRequestBody
        {
            DefaultValue = true,
            Description = "description",
            FlagType = "boolean",
            Key = "key",
            Name = "name",
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

**request:** `UpdateFlagRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">DeleteFlagAsync</a>(DeleteFlagRequest { ... }) -> DeleteFlagResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.DeleteFlagAsync(new DeleteFlagRequest { FlagId = "flag_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteFlagRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">UpdateFlagRulesAsync</a>(UpdateFlagRulesRequestBody { ... }) -> UpdateFlagRulesResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.UpdateFlagRulesAsync(
    new UpdateFlagRulesRequestBody
    {
        FlagId = "flag_id",
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

**request:** `UpdateFlagRulesRequestBody` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Features.<a href="/src/SchematicHQ.Client/Features/FeaturesClient.cs">CheckFlagAsync</a>(CheckFlagRequest { ... }) -> CheckFlagResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Features.CheckFlagAsync(
    new CheckFlagRequest { Key = "key", Body = new CheckFlagRequestBody() }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CheckFlagRequest` 
    
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

<details><summary><code>client.Plangroups.<a href="/src/SchematicHQ.Client/Plangroups/PlangroupsClient.cs">UpdatePlanGroupAsync</a>(UpdatePlanGroupRequestBody { ... }) -> UpdatePlanGroupResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Plangroups.UpdatePlanGroupAsync(
    new UpdatePlanGroupRequestBody
    {
        PlanGroupId = "plan_group_id",
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

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">GetWebhookEventAsync</a>(GetWebhookEventRequest { ... }) -> GetWebhookEventResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.GetWebhookEventAsync(
    new GetWebhookEventRequest { WebhookEventId = "webhook_event_id" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetWebhookEventRequest` 
    
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

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">GetWebhookAsync</a>(GetWebhookRequest { ... }) -> GetWebhookResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.GetWebhookAsync(new GetWebhookRequest { WebhookId = "webhook_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `GetWebhookRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">UpdateWebhookAsync</a>(UpdateWebhookRequestBody { ... }) -> UpdateWebhookResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.UpdateWebhookAsync(new UpdateWebhookRequestBody { WebhookId = "webhook_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

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

<details><summary><code>client.Webhooks.<a href="/src/SchematicHQ.Client/Webhooks/WebhooksClient.cs">DeleteWebhookAsync</a>(DeleteWebhookRequest { ... }) -> DeleteWebhookResponse</code></summary>
<dl>
<dd>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Webhooks.DeleteWebhookAsync(new DeleteWebhookRequest { WebhookId = "webhook_id" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteWebhookRequest` 
    
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
