namespace SchematicHQ.Client;

public partial interface IBillingClient
{
    WithRawResponseTask<ListCouponsResponse> ListCouponsAsync(
        ListCouponsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertBillingCouponResponse> UpsertBillingCouponAsync(
        CreateCouponRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertBillingCustomerResponse> UpsertBillingCustomerAsync(
        CreateBillingCustomerRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListCustomersWithSubscriptionsResponse> ListCustomersWithSubscriptionsAsync(
        ListCustomersWithSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountCustomersResponse> CountCustomersAsync(
        CountCustomersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListInvoicesResponse> ListInvoicesAsync(
        ListInvoicesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertInvoiceResponse> UpsertInvoiceAsync(
        CreateInvoiceRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListMetersResponse> ListMetersAsync(
        ListMetersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertBillingMeterResponse> UpsertBillingMeterAsync(
        CreateMeterRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListPaymentMethodsResponse> ListPaymentMethodsAsync(
        ListPaymentMethodsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertPaymentMethodResponse> UpsertPaymentMethodAsync(
        CreatePaymentMethodRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListBillingPricesResponse> ListBillingPricesAsync(
        ListBillingPricesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertBillingPriceResponse> UpsertBillingPriceAsync(
        CreateBillingPriceRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteBillingProductResponse> DeleteBillingProductAsync(
        string billingId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListBillingProductPricesResponse> ListBillingProductPricesAsync(
        ListBillingProductPricesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteProductPriceResponse> DeleteProductPriceAsync(
        string billingId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertBillingProductResponse> UpsertBillingProductAsync(
        CreateBillingProductRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListBillingProductsResponse> ListBillingProductsAsync(
        ListBillingProductsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountBillingProductsResponse> CountBillingProductsAsync(
        CountBillingProductsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertBillingSubscriptionResponse> UpsertBillingSubscriptionAsync(
        CreateBillingSubscriptionRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
