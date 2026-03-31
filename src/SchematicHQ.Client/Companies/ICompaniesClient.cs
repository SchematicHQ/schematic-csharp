namespace SchematicHQ.Client;

public partial interface ICompaniesClient
{
    WithRawResponseTask<ListCompaniesResponse> ListCompaniesAsync(
        ListCompaniesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertCompanyResponse> UpsertCompanyAsync(
        UpsertCompanyRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetCompanyResponse> GetCompanyAsync(
        string companyId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteCompanyResponse> DeleteCompanyAsync(
        string companyId,
        DeleteCompanyRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountCompaniesResponse> CountCompaniesAsync(
        CountCompaniesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateCompanyResponse> CreateCompanyAsync(
        UpsertCompanyRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteCompanyByKeysResponse> DeleteCompanyByKeysAsync(
        KeysRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Company lookup is determined to resolve a company from its keys, similar to how many of our other apis work.
    /// The following approaches will all work to resolve a company and any of them are appropriate:
    /// 1. `/companies/lookup?keys={"foo": "bar", "fizz": "buzz"}`
    /// 2. `/companies/lookup?keys[foo]=bar&keys[fizz]=buzz`
    /// 2. `/companies/lookup?foo=bar&fizz=buzz`
    /// </summary>
    WithRawResponseTask<LookupCompanyResponse> LookupCompanyAsync(
        LookupCompanyRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListCompanyMembershipsResponse> ListCompanyMembershipsAsync(
        ListCompanyMembershipsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetOrCreateCompanyMembershipResponse> GetOrCreateCompanyMembershipAsync(
        GetOrCreateCompanyMembershipRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteCompanyMembershipResponse> DeleteCompanyMembershipAsync(
        string companyMembershipId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetActiveCompanySubscriptionResponse> GetActiveCompanySubscriptionAsync(
        GetActiveCompanySubscriptionRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertCompanyTraitResponse> UpsertCompanyTraitAsync(
        UpsertTraitRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListEntityKeyDefinitionsResponse> ListEntityKeyDefinitionsAsync(
        ListEntityKeyDefinitionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountEntityKeyDefinitionsResponse> CountEntityKeyDefinitionsAsync(
        CountEntityKeyDefinitionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListEntityTraitDefinitionsResponse> ListEntityTraitDefinitionsAsync(
        ListEntityTraitDefinitionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetOrCreateEntityTraitDefinitionResponse> GetOrCreateEntityTraitDefinitionAsync(
        CreateEntityTraitDefinitionRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetEntityTraitDefinitionResponse> GetEntityTraitDefinitionAsync(
        string entityTraitDefinitionId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdateEntityTraitDefinitionResponse> UpdateEntityTraitDefinitionAsync(
        string entityTraitDefinitionId,
        UpdateEntityTraitDefinitionRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountEntityTraitDefinitionsResponse> CountEntityTraitDefinitionsAsync(
        CountEntityTraitDefinitionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetEntityTraitValuesResponse> GetEntityTraitValuesAsync(
        GetEntityTraitValuesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListPlanChangesResponse> ListPlanChangesAsync(
        ListPlanChangesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetPlanChangeResponse> GetPlanChangeAsync(
        string planChangeId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListPlanTraitsResponse> ListPlanTraitsAsync(
        ListPlanTraitsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreatePlanTraitResponse> CreatePlanTraitAsync(
        CreatePlanTraitRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetPlanTraitResponse> GetPlanTraitAsync(
        string planTraitId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdatePlanTraitResponse> UpdatePlanTraitAsync(
        string planTraitId,
        UpdatePlanTraitRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeletePlanTraitResponse> DeletePlanTraitAsync(
        string planTraitId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpdatePlanTraitsBulkResponse> UpdatePlanTraitsBulkAsync(
        UpdatePlanTraitBulkRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountPlanTraitsResponse> CountPlanTraitsAsync(
        CountPlanTraitsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertUserTraitResponse> UpsertUserTraitAsync(
        UpsertTraitRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<ListUsersResponse> ListUsersAsync(
        ListUsersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<UpsertUserResponse> UpsertUserAsync(
        UpsertUserRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<GetUserResponse> GetUserAsync(
        string userId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteUserResponse> DeleteUserAsync(
        string userId,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CountUsersResponse> CountUsersAsync(
        CountUsersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<CreateUserResponse> CreateUserAsync(
        UpsertUserRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<DeleteUserByKeysResponse> DeleteUserByKeysAsync(
        KeysRequestBody request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    WithRawResponseTask<LookupUserResponse> LookupUserAsync(
        LookupUserRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
