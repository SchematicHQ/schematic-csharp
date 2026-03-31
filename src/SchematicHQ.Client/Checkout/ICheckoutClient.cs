namespace SchematicHQ.Client;

public partial interface ICheckoutClient
{
    WithRawResponseTask<CheckoutInternalResponse> InternalAsync(
        ChangeSubscriptionInternalRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetCheckoutDataResponse> GetCheckoutDataAsync(
        CheckoutDataRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<PreviewCheckoutInternalResponse> PreviewCheckoutInternalAsync(
        ChangeSubscriptionInternalRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ManagePlanResponse> ManagePlanAsync(
        ManagePlanRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<PreviewManagePlanResponse> PreviewManagePlanAsync(
        ManagePlanRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CancelSubscriptionResponse> CancelSubscriptionAsync(
        CancelSubscriptionRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateCustomerSubscriptionTrialEndResponse> UpdateCustomerSubscriptionTrialEndAsync(
        string subscriptionId,
        UpdateTrialEndRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
