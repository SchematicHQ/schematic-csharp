namespace SchematicHQ.Client.RulesEngine
{
    public static class Errors
    {
        public static readonly Exception ErrorUnexpected = new Exception("Unexpected error");
        public static readonly Exception ErrorFlagNotFound = new Exception("Flag not found");
    }
}
